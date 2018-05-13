using System.IO;
using FxFFmpeg.Core;

namespace FxFFmpeg.Objects
{
	public class MediaFile
	{
		readonly FileInfo _info;

		public MediaFile(FileInfo info) { _info = info; }

		public string Name => _info.Name;
		public decimal SizeInGb => _info.Length/(decimal) FFMpegConstants.GByte;
		public string Path => _info.FullName;
	}
}