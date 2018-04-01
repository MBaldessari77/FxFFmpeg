using System.Collections.Generic;
using System.Text;

namespace FxFFmpeg.Models
{
	// ReSharper disable once InconsistentNaming
	public class FFMpegStartCommand : FFMpegCommand
	{
		public IEnumerable<string> Inputs { get; set; }

		public string Options
		{
			get
			{
				var options = new StringBuilder();

				if (Inputs != null)
					foreach (string input in Inputs)
					{
						if(string.IsNullOrWhiteSpace(input))
							continue;

						if (options.Length > 0)
							options.Append(" ");

						string trimmed = input.Trim();
						options.Append(trimmed.Contains(" ") ? $"-i \"{trimmed}\"" : $"-i {trimmed}");
					}

				return options.ToString();
			}
		}
	}
}