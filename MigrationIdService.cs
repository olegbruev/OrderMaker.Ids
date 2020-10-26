using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
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

        public MigrationIdService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var idnContext = scope.ServiceProvider.GetRequiredService<IdentityContext>();
            IEnumerable<string> pm = await idnContext.Database.GetPendingMigrationsAsync();
            bool idnMigration = pm.Any();

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
