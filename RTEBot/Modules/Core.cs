using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Webhook;
using Discord;
using Discord.WebSocket;
using Discord.Commands;

namespace RTEBot.Modules
{
    public class Core : ModuleBase<SocketCommandContext>
    {
        [Command("CleanHouse", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task Prune(int count)
        {
            await Prune(Context.Channel, count);
        }



        [Command("CleanHouse", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public async Task Prune(IMessageChannel channel, int count)
        {
            var messages = await channel.GetMessagesAsync(count).Flatten();
            int messageCount = messages.Count();
            this.Context.Channel.DeleteMessagesAsync(messages);
            IUserMessage message = null;
            if (messageCount == 1)
                message = await channel.SendMessageAsync("Deleted 1 message");
            else
                message = await channel.SendMessageAsync($"Deleted {messageCount} messages.");

            await Task.Delay(2000);
            await message.DeleteAsync();
        }
        [Command("CleanHouse", RunMode = RunMode.Async)]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireUserPermission(ChannelPermission.ManageMessages)]
        [RequireBotPermission(ChannelPermission.ManageMessages)]
        public Task Prune() => ReplyAsync("'!CleanHouse {message count}'");
    }
}
