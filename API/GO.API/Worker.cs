using Microsoft.AspNetCore.SignalR;

namespace GO.API
{
    public class Worker : BackgroundService
    {
        private readonly IHubContext<Socket> _hubContext;
        private readonly TimeSpan _interval = TimeSpan.FromSeconds(3);

        public Worker(IHubContext<Socket> hubContext)
        {
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_interval, stoppingToken); // Đợi 15 giây

                var model = new SocketModel { Flag = true };

                await _hubContext.Clients.All.SendAsync("Flag", model); // Gửi cho tất cả client
            }
        }

    }
}
