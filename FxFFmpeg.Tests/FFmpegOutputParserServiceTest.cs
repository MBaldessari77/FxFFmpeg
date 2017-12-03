using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RxFFmpegCore.Models;
using RxFFmpegCore.Tests.Stubs;
using Xunit;

namespace FxFFmpeg.Tests
{
	public class FFmpegOutputParserServiceTest
	{
		[Fact]
		public async Task NullOrEmptyInputIsIgnored()
		{
			var ffmpegProcess = new FFmpegProcessStub();
			ffmpegProcess.EnqueueOutput(null);
			ffmpegProcess.EnqueueOutput(string.Empty);
			ffmpegProcess.EnqueueOutput(" ");

			var ffmpeg = new FFmpegTask(ffmpegProcess);
			ffmpeg.Start();

			var outputs = new List<FFmpegOutput>();
			FFmpegOutput output;
			while ((output = await ffmpeg.GetOutputAsync()) != null)
				outputs.Add(output);

			Assert.Empty(outputs);
		}

		[Fact]
		public async Task VersionLineIsProcessedCorrectly()
		{
			var ffmpegProcess = new FFmpegProcessStub();
			ffmpegProcess.EnqueueOutput("ffmpeg version 3.0.1 Copyright (c) 2000-2016 the FFmpeg developers");

			var ffmpeg = new FFmpegTask(ffmpegProcess);
			ffmpeg.Start();

			var outputs = new List<FFmpegOutput>();
			FFmpegOutput output;
			while ((output = await ffmpeg.GetOutputAsync()) != null)
				outputs.Add(output);

			var version = outputs.OfType<FFmpegVersion>().FirstOrDefault();

			Assert.Equal(version?.Version, "3.0.1");
		}

		[Fact]
		public async Task VideoStreamIsProcessedCorrectly()
		{
			var ffmpegProcess = new FFmpegProcessStub();
			ffmpegProcess.EnqueueOutput("    Stream #0:2: Video: h264 (High), yuv420p(tv, bt709), 1280x720 [SAR 1:1 DAR 16:9], 25 fps, 25 tbr, 1k tbn, 47.95 tbc");

			var ffmpeg = new FFmpegTask(ffmpegProcess);
			ffmpeg.Start();

			var outputs = new List<FFmpegOutput>();
			FFmpegOutput output;
			while ((output = await ffmpeg.GetOutputAsync()) != null)
				outputs.Add(output);

			var stream = outputs.OfType<MediaVideoStream>().FirstOrDefault();

			Assert.Equal(stream?.Media, 0);
			Assert.Equal(stream?.Number, 2);
			Assert.Equal(stream?.Attributes?.Count(), 7);
			Assert.Contains("h264 (High)", stream?.Attributes ?? Enumerable.Empty<string>());
			Assert.Contains("yuv420p(tv, bt709)", stream?.Attributes ?? Enumerable.Empty<string>());
			Assert.Contains("1280x720 [SAR 1:1 DAR 16:9]", stream?.Attributes ?? Enumerable.Empty<string>());
			Assert.Contains("25 fps", stream?.Attributes ?? Enumerable.Empty<string>());
			Assert.Contains("25 tbr", stream?.Attributes ?? Enumerable.Empty<string>());
			Assert.Contains("1k tbn", stream?.Attributes ?? Enumerable.Empty<string>());
			Assert.Contains("47.95 tbc", stream?.Attributes ?? Enumerable.Empty<string>());
		}

		[Fact]
		public async Task DurationIsProcessedCorrectly()
		{
			var ffmpegProcess = new FFmpegProcessStub();
			ffmpegProcess.EnqueueOutput("  Duration: 00:41:33.50, start: 0.000000, bitrate: 4800 kb/s");

			var ffmpeg = new FFmpegTask(ffmpegProcess);
			ffmpeg.Start();

			var outputs = new List<FFmpegOutput>();
			FFmpegOutput output;
			while ((output = await ffmpeg.GetOutputAsync()) != null)
				outputs.Add(output);

			var duration = outputs.OfType<FFmpegDuration>().FirstOrDefault();

			Assert.Equal(duration?.Duration, new TimeSpan(0, 0, 41, 33, 500));
			Assert.Equal(duration?.Start, 0M);
			Assert.Equal(duration?.Bitrate, 4800);
		}

		[Fact]
		public async Task DurationAndBitRateNotAvaibleIsProcessedCorrectly()
		{
			var ffmpegProcess = new FFmpegProcessStub();
			ffmpegProcess.EnqueueOutput("  Duration: N/A, start: 0.000000, bitrate: N/A kb/s");

			var ffmpeg = new FFmpegTask(ffmpegProcess);
			ffmpeg.Start();

			var outputs = new List<FFmpegOutput>();
			FFmpegOutput output;
			while ((output = await ffmpeg.GetOutputAsync()) != null)
				outputs.Add(output);

			var duration = outputs.OfType<FFmpegDuration>().FirstOrDefault();

			Assert.Equal(duration?.Duration, TimeSpan.Zero);
			Assert.Equal(duration?.Start, 0M);
			Assert.Equal(duration?.Bitrate, 0);
		}
	}
}
