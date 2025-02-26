using Com.Gosol.BUS.NghiepVu;
using Com.Gosol.Models.NghiepVu;
using GroupDocs.Viewer.Caching;
using Microsoft.AspNetCore.SignalR;

namespace GO.API
{
    public class Socket :Hub
    {
        private HistoryEmotionBUS _historyEmotionBUS;
        public Socket() 
        {
            _historyEmotionBUS = new HistoryEmotionBUS();
        }

        public async Task RegisterDevice()
        {
            Console.WriteLine("New client connected: " + Context.ConnectionId);
            await Clients.Caller.SendAsync("Registered", Context.ConnectionId);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Client disconnected: " + Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        public List<EmotionData> Data()
        {
           return _historyEmotionBUS.Data();
        }
    }
    public class SocketModel
    {
        public bool Flag { get; set; }
    }
}
