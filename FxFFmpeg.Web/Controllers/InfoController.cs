using System.Reflection;
using System.Threading.Tasks;
using FxFFmpeg.Web.Objects;
using Microsoft.AspNetCore.Mvc;

namespace FxFFmpeg.Web.Controllers
{
	[Route("api/[controller]")]
	public class InfoController : Controller
	{
		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> Versions()
		{
			FFmpegTask ffmpeg = new FFmpegTask();
			ffmpeg.Start();

			FFmpegVersion version = await ffmpeg.GetOutputAsync() as FFmpegVersion;

			return Ok(version);
		}

		[HttpGet]
		[Route("[action]")]
		public IActionResult Version()
		{
			string version = Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;

			return Ok(version);
		}
	}
}