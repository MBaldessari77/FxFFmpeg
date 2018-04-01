using System;

namespace FxFFmpeg.Objects
{
	public class FFmpegDuration : FFmpegOutput
	{
		public FFmpegDuration(TimeSpan duration, decimal start, int bitrate)
		{
			Duration = duration;
			Start = start;
			Bitrate = bitrate;
		}

		public TimeSpan Duration { get; }
		public decimal Start { get; }
		public int Bitrate { get; }
	}
}