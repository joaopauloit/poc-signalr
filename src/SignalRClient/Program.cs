using Microsoft.AspNetCore.SignalR.Client;

var connection = new HubConnectionBuilder()
    .WithUrl("http://localhost:5254/chat") // URL do servidor SignalR
    .Build();

connection.On<string, string>("ReceiveMessage", (user, message) =>
{
    Console.WriteLine($"{user}: {message}");
});

try
{
    await connection.StartAsync();
    Console.WriteLine("Conexão estabelecida com o SignalR.");
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao conectar: {ex.Message}");
}

while (true)
{
    Console.WriteLine("Digite seu nome:");
    var user = Console.ReadLine();
    Console.WriteLine("Digite sua mensagem:");
    var message = Console.ReadLine();

    if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(message))
    {
        await connection.InvokeAsync("SendMessage", user, message);
    }
}
