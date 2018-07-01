using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using FxFFmpeg.Web.Objects;

namespace FxFFmpeg.Web.Services
{
	public class FFmpegOutputParserService
	{
		public FFmpegOutput ProcessOutput(string line)
		{
			if (string.IsNullOrWhiteSpace(line))
				return null;

			//Version info
			if (Regex.Match(line, @"^ffmpeg version").Success)
			{
				string version = Regex.Match(line, @"[0-9]\.[0-9]\.[0-9]").Value;
				if(string.IsNullOrWhiteSpace(version))
					version = Regex.Match(line, @"[0-9]\.[0-9]").Value;
				return new FFmpegVersion(version);
			}

			//Stream info
			if (Regex.Match(line, @"\s?Stream").Success)
			{
				Match videoMatch = Regex.Match(line, @"Video:");
				if (videoMatch.Success)
				{
					string ordinal = Regex.Match(line, @"#[0-9]+:[0-9]+").Value;
					string[] ordinals = ordinal.Substring(1).Split(':');
					int media = int.Parse(ordinals[0]);
					int number = int.Parse(ordinals[1]);
					IEnumerable<string> attributes = Regex.Split(line.Substring(videoMatch.Index + videoMatch.Length), @",(?![^(]*\))").Select(a => a.Trim());
					return new MediaVideoStream(media, number, attributes);
				}
			}

			//Duration info
			if (Regex.Match(line, @"\s?Duration:").Success)
			{
				Match durationMatch = Regex.Match(line, @"[0-9]{2}:[0-9]{2}:[0-9]{2}.[0-9]{2}");
				TimeSpan duration = durationMatch.Success ? TimeSpan.ParseExact(durationMatch.Value, @"hh\:mm\:ss\.ff", CultureInfo.InvariantCulture) : TimeSpan.Zero;
				Match startMatch = Regex.Match(line, @"\sstart: [0-9]+.[0-9]+");
				decimal start = decimal.Parse(startMatch.Value.Substring("start: ".Length), CultureInfo.InvariantCulture);
				Match startBitrate = Regex.Match(line, @"\sbitrate: [0-9]+");
				int bitrate = startBitrate.Success ? int.Parse(startBitrate.Value.Substring("bitrate: ".Length)) : 0;
				return new FFmpegDuration(duration, start, bitrate);
			}

			return null;
		}
	}
}