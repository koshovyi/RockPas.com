using Microsoft.Extensions.DependencyInjection;

namespace RockPaperScissors.Services.Games
{

	public static class Extensions
	{

		public static IServiceCollection AddServiceGames(this IServiceCollection services)
		{
			services.AddSingleton<IGamesService, GamesService>();
			return services;
		}

	}

}
