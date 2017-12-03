using System.Collections.Generic;

namespace RxFFmpegCore.Models
{
	public abstract class MediaStream : FFmpegOutput
	{
		protected MediaStream(int media, int number, IEnumerable<string> attributes)
		{
			Media = media;
			Attributes = attributes;
			Number = number;
		}

		public int Media { get; private set; }
		public int Number { get; private set; }
		public IEnumerable<string> Attributes { get; private set; }
	}
}