namespace FxFFmpeg.Core
{
	// ReSharper disable once InconsistentNaming
	public static class FFMpegConstants
	{
		public const string FFmpegExecutable = "ffmpeg.exe";

		public const long KByte = 1000;
		public const long MByte = KByte* KByte;
		public const long GByte = KByte*KByte*KByte;

		public static readonly string[] SupportedMediaExtensions =
		{
			"*.mkv",
			"*.avi",
			"*.mp4"
		};

		public const string H264 = "H264";
		// ReSharper disable once InconsistentNaming
		public const string HEVC = "HEVC";
	}
}