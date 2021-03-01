using Microsoft.Extensions.DependencyInjection;

namespace RockPaperScissors.Services.Common
{

	public static class Extensions
	{

		public static IServiceCollection AddServiceCommon(this IServiceCollection services)
		{
			services.AddSingleton<ICommonErrorService, CommonErrorService>();
			services.AddSingleton<ICommonLogsService, CommonLogsService>();
			services.AddSingleton<ICommonSettingsServiceBool, CommonSettingsServiceBool>();
			return services;
		}

	}

}
