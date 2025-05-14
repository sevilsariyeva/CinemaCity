
using CinemaCity.Data;
using Microsoft.EntityFrameworkCore;

namespace CinemaCity.Services.Concrete
{
    public class TicketRemoveService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);

        public TicketRemoveService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) 
            { 
                using(var scope = _serviceScopeFactory.CreateScope())
                {
                    var context=scope.ServiceProvider.GetRequiredService<CinemaCityDbContext>();

                    var expiredTickets = await context.Tickets
                   .Where(t => t.BasketId != null && DateTime.UtcNow > t.ReservedAt.AddMinutes(10))
                   .ToListAsync();

                    if (expiredTickets.Any())
                    {
                        context.Tickets.RemoveRange(expiredTickets);
                        await context.SaveChangesAsync();
                    }
                    
                }
                await Task.Delay(_interval, stoppingToken);
            }
            throw new NotImplementedException();
        }
    }
}
