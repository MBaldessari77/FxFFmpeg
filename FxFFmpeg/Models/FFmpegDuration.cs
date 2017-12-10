using System;

namespace FxFFmpeg.Models
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