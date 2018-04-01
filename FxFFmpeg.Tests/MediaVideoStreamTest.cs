using FxFFmpeg.Objects;
using Xunit;

namespace FxFFmpeg.Tests
{
	public class MediaVideoStreamTest
	{
		[Fact]
		public void TheCheckIsH264WorkAsExpected()
		{
			var h264Media = new MediaVideoStream(0, 0, new[] {"    Stream #0:2: Video: h264 (High), yuv420p(tv, bt709), 1280x720 [SAR 1:1 DAR 16:9], 25 fps, 25 tbr, 1k tbn, 47.95 tbc"});
			var nonH264Media = new MediaVideoStream(0, 0, new[] {"    Stream #0:2: Video: MPEG (High), yuv420p(tv, bt709), 1280x720 [SAR 1:1 DAR 16:9], 25 fps, 25 tbr, 1k tbn, 47.95 tbc"});

			Assert.True(h264Media.IsH264);
			Assert.False(nonH264Media.IsH264);
		}
		
		[Fact]
		public void TheCheckIsHevcWorkAsExpected()
		{
			var hevcMedia = new MediaVideoStream(0, 0, new[] {"    Stream #0:2: Video: HEVC (High), yuv420p(tv, bt709), 1280x720 [SAR 1:1 DAR 16:9], 25 fps, 25 tbr, 1k tbn, 47.95 tbc"});
			var nonHevcMedia = new MediaVideoStream(0, 0, new[] {"    Stream #0:2: Video: MPEG (High), yuv420p(tv, bt709), 1280x720 [SAR 1:1 DAR 16:9], 25 fps, 25 tbr, 1k tbn, 47.95 tbc"});

			Assert.True(hevcMedia.IsHEVC);
			Assert.False(nonHevcMedia.IsHEVC);
		}

		[Fact]
		public void TheCheckIsUnknownWorkAsExpected()
		{
			var unknownEncodingMedia = new MediaVideoStream(0, 0, new[] {"    Stream #0:2: Video: strange (High), yuv420p(tv, bt709), 1280x720 [SAR 1:1 DAR 16:9], 25 fps, 25 tbr, 1k tbn, 47.95 tbc"});

			Assert.True(unknownEncodingMedia.IsUnknown);
		}
	}
}