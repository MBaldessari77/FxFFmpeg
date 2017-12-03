using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RxFFmpegCore.Models;
using RxFFmpegWebCore.Model;

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

			List<MediaVideoStreamView> outputs = new List<MediaVideoStreamView>();

			while (true)
			{
				var output = await ffmpeg.GetOutputAsync();

				if (output == null)
					break;

				var mediaVideoStream = output as MediaVideoStream;
				if (mediaVideoStream != null)
					outputs.Add(new MediaVideoStreamView(mediaVideoStream));
			}

			return Ok(outputs);
		}
	}
}