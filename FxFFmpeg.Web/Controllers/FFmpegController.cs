using System.Collections.Generic;
using System.Threading.Tasks;
using FxFFmpeg.Web.Objects;
using Microsoft.AspNetCore.Mvc;

namespace FxFFmpeg.Web.Controllers
{
	[Route("api/[controller]")]
	public class FFmpegController : Controller
	{
		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetMediaInfo(string path)
		{
			FFmpegTask ffmpeg = new FFmpegTask();
			ffmpeg.Start($"-i \"{path}\"");

			List<MediaVideoStream> outputs = new List<MediaVideoStream>();

			while (true)
			{
				var output = await ffmpeg.GetOutputAsync();

				if (output == null)
					break;

				if (output is MediaVideoStream mediaVideoStream)
					outputs.Add(mediaVideoStream);
			}

			return Ok(outputs.ToArray());
		}
	}
}