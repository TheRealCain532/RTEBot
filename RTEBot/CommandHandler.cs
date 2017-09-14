using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace RTEBot
{
    class CommandHandler
    {
        private DiscordSocketClient _client;

        private CommandService _service;

        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;

            _service = new CommandService();

            _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _client.MessageReceived += HandleCommandAsync;


        }

        Boolean isAdmin = false;
        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
            for (int i = 0; i < Modules.Variables.Admins.Length; i++)
                if (context.User.Username == Modules.Variables.Admins[i]) isAdmin = true;
            if (isAdmin)
            {
                if (msg.HasCharPrefix('!', ref argPos))
                {
                    var result = await _service.ExecuteAsync(context, argPos);

                    if (!result.IsSuccess && result.Error != CommandError.UnknownCommand)
                    {
                        await context.Channel.SendMessageAsync("Something went Horribly Wrong!\n\nBut don't worry, we are working to fix it\n\n" + result.ErrorReason);
                    }
                }
            }

        }


    }
}
