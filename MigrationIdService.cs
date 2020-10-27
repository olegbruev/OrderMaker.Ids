using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mtd.OrderMaker.Ids.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mtd.OrderMaker.Ids
{
    public class MigrationIdService : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger logger;
        public MigrationIdService(IServiceProvider serviceProvider, ILogger<MigrationIdService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var idnContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            bool idnMigration = false;

            try
            {
                IEnumerable<string> pm = await idnContext.Database.GetPendingMigrationsAsync();
                idnMigration = pm.Any();
            }
            catch (SqlException ex)
            {
                logger.LogError(ex.Message);
            }


            if (idnMigration)
            {
                await idnContext.Database.MigrateAsync();

            }

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
