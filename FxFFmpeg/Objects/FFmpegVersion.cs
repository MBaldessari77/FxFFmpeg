namespace FxFFmpeg.Objects
{
	public class FFmpegVersion : FFmpegOutput
	{
		public FFmpegVersion(string version)
		{
			Version = version;
		}

		public string Version { get; }
	}
}