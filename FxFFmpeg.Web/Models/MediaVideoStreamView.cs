using System;
using System.Linq;
using RxFFmpegCore.Core;
using RxFFmpegCore.Models;

namespace RxFFmpegWebCore.Model
{
	public class MediaVideoStreamView
	{
		readonly MediaVideoStream _mediaVideoStream;

		public MediaVideoStreamView(MediaVideoStream mediaVideoStream) { _mediaVideoStream = mediaVideoStream; }

		public bool IsH264 => _mediaVideoStream.Attributes.Any(a => a.IndexOf(FFMpegConstants.H264, StringComparison.OrdinalIgnoreCase) != -1);
		// ReSharper disable once InconsistentNaming
		public bool IsHEVC => _mediaVideoStream.Attributes.Any(a => a.IndexOf(FFMpegConstants.HEVC, StringComparison.OrdinalIgnoreCase) != -1);
		public bool IsUnknown => !IsH264 && !IsHEVC;
		public string VideoStreamTypeRaw => _mediaVideoStream.Attributes.FirstOrDefault() ?? "N/A";
	}
}