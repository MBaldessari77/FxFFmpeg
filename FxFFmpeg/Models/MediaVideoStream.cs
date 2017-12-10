using System.Collections.Generic;

namespace FxFFmpeg.Models
{
	public class MediaVideoStream : MediaStream
	{
		public MediaVideoStream(int media, int number, IEnumerable<string> attributes) : base(media, number, attributes) { }
	}
}