using System.Threading.Tasks;

namespace RxFFmpegCore.Contracts
{
	public interface IFFmpegProcess
	{
		void Start(string arguments);
		Task<string> ReadLineAsync();
		Task WaitForExitAsync();
		void Close();
		bool Started { get; }
	}
}