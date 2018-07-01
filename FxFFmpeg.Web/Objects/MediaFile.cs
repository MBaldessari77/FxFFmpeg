using System.IO;
using FxFFmpeg.Web.Core;

namespace FxFFmpeg.Web.Objects
{
	public class MediaFile
	{
		readonly FileInfo _info;

		public MediaFile(FileInfo info) { _info = info; }

		// ReSharper disable UnusedMember.Global
		public string Name => _info.Name;
		public decimal SizeInGb => _info.Length/(decimal) FFMpegConstants.GByte;
		public string Path => _info.FullName;
		// ReSharper restore UnusedMember.Global
	}
}