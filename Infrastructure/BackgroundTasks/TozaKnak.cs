using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class CleanupWorker(IServiceProvider serviceProvider) : BackgroundService
{
    private readonly IServiceProvider _service = serviceProvider;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            using (var s = _service.CreateScope())
            {
                var _context = s.ServiceProvider.GetRequiredService<ApplicationDBContext>();
                var a = DateTime.UtcNow.AddMinutes(-2);
                var CreatedAtWith2Minutes = _context.Products.Where(p => p.CreatedAt < a).ToList();
                int count = CreatedAtWith2Minutes.Count;
                _context.Products.RemoveRange(CreatedAtWith2Minutes);
                await _context.SaveChangesAsync();
                System.Console.WriteLine($"Number of products that were deleted: {count}");
            }
            await Task.Delay(30000, ct);
        }
    }
}