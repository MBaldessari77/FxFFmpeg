using System.Collections.Generic;

namespace RxFFmpegCore.Models
{
	public class MediaVideoStream : MediaStream
	{
		public MediaVideoStream(int media, int number, IEnumerable<string> attributes) : base(media, number, attributes) { }
	}
}