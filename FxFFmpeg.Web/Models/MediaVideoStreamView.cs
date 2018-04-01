// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
using System;
using System.Linq;
using System.Text.RegularExpressions;
using FxFFmpeg.Core;
using FxFFmpeg.Objects;

namespace FxFFmpeg.Web.Models
{
	public class MediaVideoStreamView
	{
		readonly MediaVideoStream _mediaVideoStream;

		public MediaVideoStreamView(MediaVideoStream mediaVideoStream) { _mediaVideoStream = mediaVideoStream; }

		public bool IsH264 => _mediaVideoStream.Attributes.Any(a => a.IndexOf(FFMpegConstants.H264, StringComparison.OrdinalIgnoreCase) != -1);
		public bool IsHEVC => _mediaVideoStream.Attributes.Any(a => a.IndexOf(FFMpegConstants.HEVC, StringComparison.OrdinalIgnoreCase) != -1);
		public bool IsUnknown => !IsH264 && !IsHEVC;
		public string VideoStreamTypeRaw => _mediaVideoStream.Attributes.FirstOrDefault() ?? "N/A";

		public string Resolution => ProcessResolution();

		string ProcessResolution()
		{
			const string pattern = @"^\d+x\d+";
        
			foreach (string attribute in _mediaVideoStream.Attributes)
			{
				Match m = Regex.Match(attribute, pattern);
				
				if(!m.Success)
					continue;

				string[] resolutions = m.Value.Split('x');

				int x = int.Parse(resolutions[0]);
				int y = int.Parse(resolutions[1]);

				if (y == 720)
					return "HD";

				if (y == 1080)
					return "FULL HD";

				if (x == 2560)
					return "2K";

				if (x == 3840)
					return "4K UHD";

				return m.Value;
			}

			return "N/A";
		}
	}
}