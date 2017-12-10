using System.Collections.Generic;

namespace FxFFmpeg.Models
{
	public abstract class MediaStream : FFmpegOutput
	{
		protected MediaStream(int media, int number, IEnumerable<string> attributes)
		{
			Media = media;
			Attributes = attributes;
			Number = number;
		}

		public int Media { get; }
		public int Number { get; }
		public IEnumerable<string> Attributes { get; }
	}
}