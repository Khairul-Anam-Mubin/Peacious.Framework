using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Peacious.Framework.Interfaces;
using Peacious.Framework.ORM.Interfaces;
using Peacious.Framework.ORM.Migrations;

namespace Peacious.Framework.Extensions;

public static class WebApplicationExtension
{
    public static WebApplication DoCreateIndexes(this WebApplication application, bool doCreateIndexes)
    {
        if (!doCreateIndexes) return application;

        var indexCreators = application.Services.GetServices<IIndexCreator>().ToList();

        Console.WriteLine($"Index Creation Started. IndexCreator Found : {indexCreators.Count}");

        indexCreators.ForEach(indexCreator =>
        {
            indexCreator.CreateIndexes();
        });

        return application;
    }

    public static WebApplication DoMigration(this WebApplication application, MigrationConfig migrationConfig)
    {
        if (!migrationConfig.Enabled)
        {
            Console.WriteLine($"Migration Job Disabled.");
            return application;
        }

        Console.WriteLine($"Migrations Starting...");

        var enabledMigrationJobs =
            migrationConfig.MigrationJobs
            .Where(job => job.Enabled)
            .OrderBy(job => job.Order)
            .ToList();

        Console.WriteLine($"Enabled Migration Jobs Found : {enabledMigrationJobs.Count}");

        using var scope = application.Services.CreateScope();

        enabledMigrationJobs.ForEach(enabledMigrationJob =>
        {
            var migraionJob =
                scope.ServiceProvider.GetRequiredKeyedService<IMigrationJob>(enabledMigrationJob.Name);

            Console.WriteLine($"Start Migrations of : {enabledMigrationJob.Name}");

            migraionJob.MigrateAsync().Wait();

            Console.WriteLine($"Done Migrations of : {enabledMigrationJob.Name}");
        });

        Console.WriteLine($"Migrations Ended...");

        return application;
    }

    public static WebApplication StartInitialServices(this WebApplication app)
    {
        var initialServices = app.Services.GetServices<IInitialService>().ToList();

        Console.WriteLine($"Start Executing All Initial Services. Initial Services found: {initialServices.Count}.\n");

        var executedCount = 0;

        foreach (var initialService in initialServices)
        {
            try
            {
                Console.WriteLine($"Initial Service : {initialService.GetType().Name} Start Executing.");
                
                initialService.InitializeAsync().Wait();
                
                Console.WriteLine($"Initial Service : {initialService.GetType().Name} Executed Successfully.");

                executedCount++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        Console.WriteLine($"Executed {executedCount} Initial Services Successfully.");

        return app;
    }
}