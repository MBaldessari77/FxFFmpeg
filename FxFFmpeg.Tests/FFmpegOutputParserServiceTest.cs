using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FxFFmpeg.Tests.Stubs;
using FxFFmpeg.Web.Objects;
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

			Assert.Equal("3.0.1", version?.Version);
		}

		[Fact]
		public async void VersionLineIsProcessedCorrectlyAlsoInCaseOfMajorAndMinorOnly()
		{
			var ffmpegProcess = new FFmpegProcessStub();
			ffmpegProcess.EnqueueOutput("ffmpeg version 3.4 Copyright(c) 2000-2017 the FFmpeg developers");

			var ffmpeg = new FFmpegTask(ffmpegProcess);
			ffmpeg.Start();

			var outputs = new List<FFmpegOutput>();
			FFmpegOutput output;
			while ((output = await ffmpeg.GetOutputAsync()) != null)
				outputs.Add(output);

			var version = outputs.OfType<FFmpegVersion>().FirstOrDefault();

			Assert.Equal("3.4", version?.Version);
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

			Assert.Equal(0, stream?.Media);
			Assert.Equal(2, stream?.Number);
			Assert.Equal(7, stream?.Attributes?.Count());
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
			Assert.Equal(0M, duration?.Start);
			Assert.Equal(4800, duration?.Bitrate);
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
			Assert.Equal(0M, duration?.Start);
			Assert.Equal(0, duration?.Bitrate);
		}
	}
}
