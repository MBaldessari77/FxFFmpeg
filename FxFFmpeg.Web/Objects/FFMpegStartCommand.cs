using System.Collections.Generic;
using System.Text;

namespace FxFFmpeg.Web.Objects
{
	// ReSharper disable once InconsistentNaming
	// ReSharper disable once UnusedMember.Global
	public class FFMpegStartCommand : FFMpegCommand
	{

		// ReSharper disable once MemberCanBePrivate.Global
		// ReSharper disable once UnusedAutoPropertyAccessor.Global
		public IEnumerable<string> Inputs { get; set; }

		// ReSharper disable once UnusedMember.Global
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