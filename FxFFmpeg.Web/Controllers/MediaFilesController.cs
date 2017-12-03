using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RxFFmpegCore.Services;

namespace FxFFmpeg.Web.Controllers
{
	[Route("api/[controller]")]
	public class MediaFilesController : Controller
	{
		readonly MediaFileService _mediaFileServices;

		public MediaFilesController(MediaFileService mediaFileServices) { _mediaFileServices = mediaFileServices; }

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> GetFiles(string path, bool includeSubFolders) { return Ok(await Task.Run(() => _mediaFileServices.GetMediaFiles(path, includeSubFolders).OrderByDescending(f => f.SizeInGb))); }
	}
}