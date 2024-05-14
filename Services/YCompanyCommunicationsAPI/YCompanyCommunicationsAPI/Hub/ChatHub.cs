using YCompanyCommunicationsAPI.Models;
using YCompanyCommunicationsAPI.Services.EmailService;
using Microsoft.AspNetCore.SignalR;
namespace YCompanyCommunicationsAPI.Hub;

public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
{
    private readonly IDictionary<string, UserRoomConnection> _connection;
    private readonly IEmailService _emailService;

    public ChatHub(IDictionary<string, UserRoomConnection> connection, IEmailService emailService)
    {
        _connection = connection;
        _emailService = emailService;
    }

    public async Task JoinRoom(UserRoomConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room!);
        _connection[Context.ConnectionId] = userConnection;
        
        await Clients.Group(userConnection.Room!)
            .SendAsync("ReceiveMessage", "Lets Program Bot", $"{userConnection.User} has Joined the Group", DateTime.Now);
        await SendConnectedUser(userConnection.Room!);

        if (userConnection.User != "Admin")
        {
            EmailDto request = new EmailDto()
            {
                To = "coolnitesh8676@gmail.com",
                Subject = "customer support",
                Body = "x want to connect"
            };
            _emailService.SendEmail(request);

        }
    }

    public async Task SendMessage(string message)
    {
        if (_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection userRoomConnection))
        {
            await Clients.Group(userRoomConnection.Room!)
                .SendAsync("ReceiveMessage", userRoomConnection.User, message, DateTime.Now);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exp)
    {
        if (!_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection roomConnection))
        {
            return base.OnDisconnectedAsync(exp);
        }

        _connection.Remove(Context.ConnectionId);
        Clients.Group(roomConnection.Room!)
            .SendAsync("ReceiveMessage", "Lets Program bot", $"{roomConnection.User} has Left the Group", DateTime.Now);
        SendConnectedUser(roomConnection.Room!);
        return base.OnDisconnectedAsync(exp);
    }

    public Task SendConnectedUser(string room)
    {
        var users = _connection.Values
            .Where(u => u.Room == room)
            .Select(s => s.User);
        return Clients.Group(room).SendAsync("ConnectedUser", users);
    }
    
}