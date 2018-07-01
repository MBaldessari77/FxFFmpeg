using System;

namespace FxFFmpeg.Web.Exceptions
{
	//[Serializable]
	public class FFmpegTaskNotStartedException : FFmpegException
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public FFmpegTaskNotStartedException() { }
		// ReSharper disable UnusedMember.Global
		public FFmpegTaskNotStartedException(string message) : base(message) { }
		public FFmpegTaskNotStartedException(string message, Exception inner) : base(message, inner) { }
		// ReSharper restore UnusedMember.Global

		//protected RxFFmpegProcessStartFailedException(
		//	SerializationInfo info,
		//	StreamingContext context) : base(info, context) { }
	}
}