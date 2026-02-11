using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

public class UpdateActivityService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public UpdateActivityService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.ExecuteSqlRawAsync("EXEC [dbo].[UpdateActivity]");
                Console.WriteLine($"[SP Called] @ {DateTime.Now}");
            }

            await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
        }
    }
}
