using System;
using System.Diagnostics;
using System.Threading.Tasks;
using RxFFmpegCore.Contracts;
using RxFFmpegCore.Exceptions;

namespace RxFFmpegCore.Core
{
	public class FFmpegLocalProcess : IFFmpegProcess
	{
		readonly string _executables;
		readonly Process _process = new Process();
		bool _started;

		public FFmpegLocalProcess() : this(FFMpegConstants.FFmpegExecutable) { }
		public FFmpegLocalProcess(string executables) { _executables = executables; }

		public void Start(string arguments)
		{
			_process.StartInfo.UseShellExecute = false;
			_process.StartInfo.RedirectStandardOutput = true;
			_process.StartInfo.RedirectStandardError = true;
			_process.StartInfo.RedirectStandardInput = true;
			_process.StartInfo.CreateNoWindow = true;
			_process.StartInfo.FileName = _executables;
			_process.StartInfo.Arguments = arguments;
			try
			{
				_process.Start();
				_started = true;
			}
			catch (Exception e)
			{
				throw new FFmpegProcessStartFailedException(e.Message, e);
			}
		}

		public async Task<string> ReadLineAsync()
		{
			while (!_process.StandardError.EndOfStream)
			{
				var line = await _process.StandardError.ReadLineAsync();
				if (line != null)
					return line;
			}

			return null;
		}

		public Task WaitForExitAsync() { return Task.Run(() => _process.WaitForExit()); }

		public void Close()
		{
			//_process.Close();
		}

		public bool Started => _started;
	}
}