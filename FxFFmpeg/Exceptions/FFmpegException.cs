using System;

//using System.Runtime.Serialization;

namespace FxFFmpeg.Exceptions
{
	//[Serializable]
	public abstract class FFmpegException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		protected FFmpegException() { }
		protected FFmpegException(string message) : base(message) { }
		protected FFmpegException(string message, Exception inner) : base(message, inner) { }

		//protected RxFFmpegException(
		//	SerializationInfo info,
		//	StreamingContext context) : base(info, context) { }
	}
}