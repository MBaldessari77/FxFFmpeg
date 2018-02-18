using System.IO;
using System.Threading.Tasks;
using FxCommonStandard.Services;
using Microsoft.AspNetCore.Mvc;

namespace FxFFmpeg.Web.Controllers
{
	[Route("api/[controller]")]
	public class PathController : Controller
	{
		readonly PathService _pathService;

		public PathController(PathService pathService)
		{
			_pathService = pathService;
		}

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> SuggestPath(string path) { return Ok(await Task.Run(() => _pathService.SuggestPath(path) ?? path)); }

		[HttpGet]
		[Route("[action]")]
		public async Task<IActionResult> DirectoryExists(string path) { return Ok(await Task.Run(() => Directory.Exists(path))); }
	}
}