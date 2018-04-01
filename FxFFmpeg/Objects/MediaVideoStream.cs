using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using FxFFmpeg.Core;

namespace FxFFmpeg.Objects
{
	public class MediaVideoStream : MediaStream
	{
		public MediaVideoStream(int media, int number, IEnumerable<string> attributes) : base(media, number, attributes) { }

		public bool IsH264 => Attributes.Any(a => a.IndexOf(FFMpegConstants.H264, StringComparison.OrdinalIgnoreCase) != -1);
		// ReSharper disable once InconsistentNaming
		public bool IsHEVC => Attributes.Any(a => a.IndexOf(FFMpegConstants.HEVC, StringComparison.OrdinalIgnoreCase) != -1);
		public bool IsUnknown => !IsH264 && !IsHEVC;
		public string VideoStreamTypeRaw => Attributes.FirstOrDefault() ?? "N/A";
		public string Resolution => ProcessResolution();

		string ProcessResolution()
		{
			const string pattern = @"^\d+x\d+";
        
			foreach (string attribute in Attributes)
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