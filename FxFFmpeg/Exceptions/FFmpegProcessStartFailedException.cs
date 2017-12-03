using System;

//using System.Runtime.Serialization;

namespace RxFFmpegCore.Exceptions
{
	//[Serializable]
	public class FFmpegProcessStartFailedException : Exception
	{
		//
		// For guidelines regarding the creation of new exception types, see
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
		// and
		//    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
		//

		public FFmpegProcessStartFailedException() { }
		public FFmpegProcessStartFailedException(string message) : base(message) { }
		public FFmpegProcessStartFailedException(string message, Exception inner) : base(message, inner) { }

		//protected RxFFmpegProcessStartFailedException(
		//	SerializationInfo info,
		//	StreamingContext context) : base(info, context) { }
	}
}