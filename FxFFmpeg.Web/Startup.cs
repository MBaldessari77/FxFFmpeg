using FxCommonStandard.Contracts;
using FxCommonStandard.Services;
using FxFFmpeg.Web.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FxFFmpeg.Web
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// ReSharper disable once UnusedAutoPropertyAccessor.Local
		IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// ReSharper disable once UnusedMember.Global
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddSingleton<MediaFileService>();
			services.AddSingleton<PathService>();
			services.AddSingleton<IFileSystemService, FileSystemService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			app.UseFileServer(true);
			app.UseMvc();
		}
	}
}
