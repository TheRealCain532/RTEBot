using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiLib;


namespace RTEBot.Modules
{
    public class PS3 : ModuleBase<SocketCommandContext>
    {
        public MultiConsoleAPI _PS3 = new MultiConsoleAPI(SelectAPI.ControlConsole);
        public async void SendMessage(string input)
        {
            await Context.Channel.SendMessageAsync(input);
        }
        private Boolean ConnectPSX(string IP = "192.168.0.13")
        {
            _PS3.ChangeAPI(SelectAPI.ControlConsole);
            if(_PS3.GameShark.Connect(IP)) return true;
            else return false;
        }

        [Command("Connect")]
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
        [Command("WriteGSMem")]
        public async Task GS_Send(string input, string input2)
        {
            if (Variables.IsConnected)
            {
                ConnectPSX(); _PS3.GameShark.GSWrite(input, input2);
            }
            else
                SendMessage("Console is offline");
        }
        [Command("WriteGSMem")]
        public async Task GS_Send(string input)
        {
            if (Variables.IsConnected)
            {
                ConnectPSX(); _PS3.GameShark.GSWrite(input);
            }
            else
                SendMessage("Console is offline");
        }
        [Command("WriteMem")]
        public async Task _Send(string address, string bytes)
        {
            if (Variables.IsConnected)
            {
                uint buffer = Convert.ToUInt32(address, 16);
                byte[] array = Variables.STB(bytes);
                _PS3.Extension.WriteBytes(buffer, array);
            }
            else
                SendMessage("Console is offline");
        }
    }

}
