using System;
using FxFFmpeg.Tests.Stubs;
using FxFFmpeg.Web.Exceptions;
using FxFFmpeg.Web.Objects;
using Xunit;

namespace FxFFmpeg.Tests
{
	public class FFmpegTaskTest
	{
		[Fact]
		public async void WhenTheTaskIsNotStartedAnExceptionIsThrown()
		{
			var ffmpegProcess = new FFmpegProcessStub();

			var ffmpeg = new FFmpegTask(ffmpegProcess);

			await Assert.ThrowsAsync<FFmpegTaskNotStartedException>(() => ffmpeg.GetOutputAsync());
		}

		[Fact]
		public void WhenUnderlineProcessFailedToStartAnExceptionIsThrown()
		{
			var ffmpegProcess = new FFmpegProcessStub();
			ffmpegProcess.ThrownExceptionOnStart();

			var ffmpeg = new FFmpegTask(ffmpegProcess);

			Assert.Throws<FFmpegProcessStartFailedException>(() => ffmpeg.Start());
		}

		[Fact]
		public async void WhenUnderlineProcessThrowAnExceptionTheExceptionIsPropagated()
		{
			var ffmpegProcess = new FFmpegProcessStub();
			ffmpegProcess.ThrownExceptionOnReadLineAsync();

			var ffmpeg = new FFmpegTask(ffmpegProcess);
			ffmpeg.Start();

			await Assert.ThrowsAsync<Exception>(() => ffmpeg.GetOutputAsync());
		}
	}
}
