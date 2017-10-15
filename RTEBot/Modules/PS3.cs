using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiLib;
using RTEBot.Games;

namespace RTEBot.Modules
{
    public class PS3 : ModuleBase<SocketCommandContext>
    {
        public GameShark GS { get { return new GameShark(); } }
        public Extension Extension { get { return new Extension(SelectAPI.ControlConsole); } }

        public async void SendMessage(string input)
        {
            await Context.Channel.SendMessageAsync(input);
        }

        private Boolean ConnectPSX(string IP = "192.168.0.13")
        {
            if (GS.Connect()) return true;
            else return false;
        }

        [Command("Connect")]
        [RequireUserPermission(Discord.GuildPermission.Administrator)]
        public async Task _Connect()
        {
            if (!Variables.IsConnected)
                SendMessage(string.Format("Connection/Attach - {0}", (Variables.IsConnected = ConnectPSX()) ? "Success!" : "Failure"));
            else
                SendMessage(string.Format("Hey, {0} We are already connected!", Context.User.Username));
        }
        [Command("MakeSandwich")]
        public async Task _Things()
        {
            SendMessage("oh Heck....Dropped the jelly");
        }
        [Command("WriteMem")]
        public async Task _Send(string address, string bytes)
        {
            uint buffer = Convert.ToUInt32(address, 16);
            if (Variables.IsConnected)
            {
                    uint buffer2 = Convert.ToUInt32(address, 16);
                    byte[] array = Variables.STB(bytes);
                    Extension.WriteBytes(buffer, array);
            }
            else
                SendMessage("Console is offline");
        }
        [Command("ReadMem")]
        public async Task _Read(string input)
        {
            uint buffer = Convert.ToUInt32(input, 16);
            if (Variables.IsConnected)
                SendMessage(BitConverter.ToString(Extension.ReadBytes(buffer, 2)));
            else
                SendMessage("Console is offline");
        }
        [Command("ReadMem")]
        public async Task _Read(string input, string length = "2")
        {
            uint buffer = Convert.ToUInt32(input, 16);
            if (Variables.IsConnected)
                SendMessage(BitConverter.ToString(Extension.ReadBytes(buffer, int.Parse(length))));
            else
                SendMessage("Console is offline");
        }
        [Command("ReadString")]
        public async Task _ReadS(string address)
        {
            uint buffer = Convert.ToUInt32(address, 16);
            if (Variables.IsConnected)
                SendMessage(Extension.ReadString(buffer));
            else
                SendMessage("Console is offline");
        }
        [Command("Disconnect")]
        [RequireOwner]
        public async Task _T()
        {
            Context.Client.LogoutAsync();
        }



    }

}
