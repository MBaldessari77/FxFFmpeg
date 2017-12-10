namespace FxFFmpeg.Models
{
	public class FFmpegVersion : FFmpegOutput
	{
		public FFmpegVersion(string version)
		{
			Version = version;
		}

		public string Version { get; private set; }
	}
}