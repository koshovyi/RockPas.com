using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RockPaperScissors.Core;
using RockPaperScissors.Database;
using RockPaperScissors.Services;
using RockPaperScissors.Services.Common;
using RockPaperScissors.Services.Games;
using RockPaperScissors.Services.Profiles;
using RockPaperScissors.Services.Support;
using System;

namespace RockPaperScissors
{
	public class Startup
	{

		public IConfiguration Configuration { get; }

		public IWebHostEnvironment Environment { get; }

		public Config Config { get; }

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			this.Configuration = configuration;
			this.Environment = env;
			this.Config = this.Configuration.Get<Config>();
		}

		#region Configure Add()

		public void ConfigureServices(IServiceCollection services)
		{
			this.ConfigureStandart(services);
			this.ConfigureDI(services);
			this.ConfigureCustom(services);
			this.ConfigureAuth(services);
		}

		private void ConfigureStandart(IServiceCollection services)
		{
			//MVC
			services.AddControllersWithViews(o =>
			{
				o.Filters.Add(typeof(ViewBagFilter));
			}).AddRazorRuntimeCompilation();

			services.AddRouting(o => o.LowercaseUrls = true);
			services.Configure<Config>(Configuration);
			services.AddOptions();
			services.AddHttpClient();
		}

		private void ConfigureDI(IServiceCollection services)
		{
			services.AddScoped<GoogleRecaptchaService>();
		}

		private void ConfigureCustom(IServiceCollection services)
		{
			//db
			string connectionString = this.Config.DataBase.FormatConnectionString();
			services.AddDataBase(connectionString);
			services.AddFluentMigrations(connectionString);

			//services
			services.AddServiceCommon();
			services.AddServiceSupport();
			services.AddServiceProfile();
			services.AddServiceGames();
		}

		private void ConfigureAuth(IServiceCollection services)
		{
			TimeSpan expirationAt = new TimeSpan(8, 0, 0);
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.Cookie.SecurePolicy = this.Environment.IsProduction()
												? CookieSecurePolicy.Always
												: CookieSecurePolicy.None;
					options.Cookie.Name = "RPSGame_" + this.Environment.EnvironmentName;
					options.ExpireTimeSpan = expirationAt;
					options.LoginPath = new PathString("/auth/signin");
					options.LogoutPath = new PathString("/auth/signout");
					options.AccessDeniedPath = new PathString("/error/503");
				});
		}

		#endregion

		#region Configure Use()

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			ConfigureStandart(app, env);
			ConfigureCustom(app, env);
		}

		private void ConfigureStandart(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
				app.UseExceptionHandler("/error/500");
			}

			app.UseStatusCodePages();
			app.UseStatusCodePagesWithReExecute("/error/{0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "areas",
					pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		private void ConfigureCustom(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseFluentMigrations();
		}

		#endregion

	}

}
