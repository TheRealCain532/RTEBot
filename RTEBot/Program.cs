using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using Discord.Webhook;
namespace RTEBot
{
    class Program
    {
        static void Main(string[] args) =>
            new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        private CommandHandler _handler;


        public async Task StartAsync()
        {
            _client = new DiscordSocketClient();

            var token = "MzU3OTQ5ODU4MTYzODUxMjgx.DKHQLQ.5AKi_aMvBFQ9VMUsMkV0RqOGFU0";
            await _client.LoginAsync(TokenType.Bot, token);

            await _client.StartAsync();

            _handler = new CommandHandler(_client);

            await Task.Delay(-1);
        }
    }
}
