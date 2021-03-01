using Microsoft.Extensions.DependencyInjection;

namespace RockPaperScissors.Services.Support
{

	public static class Extensions
	{

		public static IServiceCollection AddServiceSupport(this IServiceCollection services)
		{
			services.AddSingleton<ISupportContactUsService, SupportContactUsService>();
			services.AddSingleton<ISupportArticleService, SupportArticleService>();
			return services;
		}

	}

}
