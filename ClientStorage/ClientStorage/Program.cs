using ClientStorage.Database;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Text;

var builder = WebApplication.CreateBuilder();

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register DbContext with PostgresSQL
builder.Services.AddDbContext<ClientDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Register classes and interfaces
builder.Services.AddScoped<ClientRepository>();

// Add Cross Origin Requests (CORS)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Add Websocket connections
app.UseWebSockets();
app.Map("/ws", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var buffer = new byte[1024 * 4];
        while (true)
        {
            var result = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer),
                CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Closing",
                    CancellationToken.None);
                break;
            }

            var receivedText = Encoding.UTF8.GetString(buffer, 0, result.Count);

            Console.WriteLine($"[Server] Received: {receivedText}");

            var responseText = "Hello from server.";
            var responseBytes = Encoding.UTF8.GetBytes(responseText);

            webSocket.SendAsync(new ArraySegment<byte>(responseBytes),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }
    }
    else
    {
        context.Response.StatusCode = 400;
    }
});

// Middleware
app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Client Storage v1");
        c.RoutePrefix = "swagger";
    });
}

// Migrate Database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ClientDbContext>();
    db.Database.Migrate();
}

app.Run();

