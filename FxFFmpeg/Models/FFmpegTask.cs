﻿using System;
using System.Threading.Tasks;
using RxFFmpegCore.Contracts;
using RxFFmpegCore.Core;
using RxFFmpegCore.Exceptions;
using RxFFmpegCore.Services;

namespace RxFFmpegCore.Models
{
	public class FFmpegTask
	{
		readonly IFFmpegProcess _process;
		readonly FFmpegOutputParserService _outputParserService = new FFmpegOutputParserService();

		public FFmpegTask() : this(new FFmpegLocalProcess()) { }
		public FFmpegTask(IFFmpegProcess process) { _process = process; }

		public void Start() { _process.Start(null); }
		public void Start(string arguments) { _process.Start(arguments); }

		public async Task<FFmpegOutput> GetOutputAsync()
		{
			if (!_process.Started)
				throw new FFmpegTaskNotStartedException();

			string line;
			while ((line = await _process.ReadLineAsync()) != null)
			{
				var output = _outputParserService.ProcessOutput(line);
				if (output != null)
					return output;
			}

			return null;
		}
	}
}