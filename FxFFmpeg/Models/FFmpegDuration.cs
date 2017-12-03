using System;

namespace RxFFmpegCore.Models
{
	public class FFmpegDuration : FFmpegOutput
	{
		public FFmpegDuration(TimeSpan duration, decimal start, int bitrate)
		{
			Duration = duration;
			Start = start;
			Bitrate = bitrate;
		}

		public TimeSpan Duration { get; private set; }
		public decimal Start { get; private set; }
		public int Bitrate { get; private set; }
	}
}