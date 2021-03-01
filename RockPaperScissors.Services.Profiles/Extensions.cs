using Microsoft.Extensions.DependencyInjection;

namespace RockPaperScissors.Services.Profiles
{

	public static class Extensions
	{

		public static IServiceCollection AddServiceProfile(this IServiceCollection services)
		{
			services.AddSingleton<IProfileService, ProfileService>();
			services.AddSingleton<IAuthService, AuthService>();
			return services;
		}

	}

}
