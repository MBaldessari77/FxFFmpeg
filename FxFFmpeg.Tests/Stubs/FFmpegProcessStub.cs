using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RxFFmpegCore.Contracts;
using RxFFmpegCore.Exceptions;

namespace RxFFmpegCore.Tests.Stubs
{
	public class FFmpegProcessStub : IFFmpegProcess
	{
		bool _thrownExceptionOnStart;
		bool _thrownExceptionOnReadLineAsync;
		readonly List<string> _outputs = new List<string>();

		public void ThrownExceptionOnStart() { _thrownExceptionOnStart = true; }
		public void ThrownExceptionOnReadLineAsync() { _thrownExceptionOnReadLineAsync = true; }
		public void EnqueueOutput(string output) { _outputs.Add(output); }

		public void Close()
		{
		}

		public Task<string> ReadLineAsync()
		{
			if(_thrownExceptionOnReadLineAsync)
				throw new Exception();

			if (_outputs.Count == 0)
				return Task.FromResult((string) null);

			var line = _outputs[0];
			_outputs.RemoveAt(0);
			return Task.FromResult(line);
		}

		public void Start(string arguments)
		{
			if (_thrownExceptionOnStart)
				throw new FFmpegProcessStartFailedException();

			Started = true;
		}

		public Task WaitForExitAsync()
		{
			return Task.CompletedTask;
		}

		public bool Started { get; private set; }
	}
}
