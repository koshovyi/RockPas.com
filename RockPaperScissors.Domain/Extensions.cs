using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RockPaperScissors.Database.Database.Migrations;
using System;

namespace RockPaperScissors.Database
{

	public static class Extensions
	{

		public static IServiceCollection AddDataBase(this IServiceCollection services, string connectionString)
		{
			Db db = new Db(connectionString);
			services.AddSingleton<Db>(db);
			return services;
		}

		public static IServiceCollection AddFluentMigrations(this IServiceCollection services, string connectionString)
		{
			services.AddFluentMigratorCore().ConfigureRunner(builder => builder
				.AddMySql5()
				.WithGlobalConnectionString(connectionString)
				.ConfigureGlobalProcessorOptions(opt => opt.Timeout = TimeSpan.FromSeconds(120))
				.ScanIn(typeof(Migration_M0_Genesis).Assembly)
				.For
				.Migrations());

			return services;
		}

		//Fluent migrate UP
		public static IApplicationBuilder UseFluentMigrations(this IApplicationBuilder app)
		{
			using (IServiceScope scope = app.ApplicationServices.CreateScope())
			{
				IMigrationRunner runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
				if (runner.HasMigrationsToApplyUp())
					runner.MigrateUp();
			}

			return app;
		}

	}

}
