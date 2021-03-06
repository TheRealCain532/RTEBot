﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;

namespace RTEBot
{
    class CommandHandler
    {
        private DiscordSocketClient _Sclient;
        private CommandService _service;

        public CommandHandler(DiscordSocketClient client)
        {
            _Sclient = client;

            _service = new CommandService();

            _service.AddModulesAsync(Assembly.GetEntryAssembly());

            _Sclient.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_Sclient, msg);

            int argPos = 0;
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
