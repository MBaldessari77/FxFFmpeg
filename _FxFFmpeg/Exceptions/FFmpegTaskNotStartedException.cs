using System;

namespace FxFFmpeg.Exceptions
{
	//[Serializable]
	public class FFmpegTaskNotStartedException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public FFmpegTaskNotStartedException() { }
		public FFmpegTaskNotStartedException(string message) : base(message) { }
		public FFmpegTaskNotStartedException(string message, Exception inner) : base(message, inner) { }

		//protected RxFFmpegProcessStartFailedException(
		//	SerializationInfo info,
		//	StreamingContext context) : base(info, context) { }
	}
}