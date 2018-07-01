using System.Threading.Tasks;

namespace FxFFmpeg.Web.Contracts
{
	public interface IFFmpegProcess
	{
		void Start(string arguments);
		Task<string> ReadLineAsync();
		// ReSharper disable UnusedMember.Global
		Task WaitForExitAsync();
		void Close();
		// ReSharper restore UnusedMember.Global
		bool Started { get; }
	}
}