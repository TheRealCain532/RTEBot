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
        public static GameShark GS { get { return new GameShark(); } }
        private static CCAPI CCAPI { get { return new CCAPI(); } }
        public Extension Extension { get { return new Extension(SelectAPI.ControlConsole); } }

        public async void SendMessage(string input)
        {
            await Context.Channel.SendMessageAsync(input);
        }

        public short SwitchInt16(short input)
        {
            return (short)((input << 8) + (input >> 8));
        }


        private Boolean ConnectPSX(string IP = "192.168.0.13")
        {
            if (GS.Connect(IP)) return true;
            else return false;
        }
        [Command("Connect")]
        [RequireOwner]
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
            uint buffer = Convert.ToUInt32(input, 16);
            if (Variables.IsConnected)
            {
                if (buffer < Variables.Ranges[1] && buffer > Variables.Ranges[0])
                    GS.GSWrite(input, input2);
                else
                    SendMessage("Range is incompatible! Please consult Cain532");
            }
            else
                SendMessage("Console is offline");
        }
        [Command("WriteGSMem")]
        public async Task GS_Send(string input)
        {
            uint buffer = Convert.ToUInt32(input, 16);

            if (Variables.IsConnected)
            {
                    GS.GSWrite(input);
            }
            else
                SendMessage("Console is offline");
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
            {
                SendMessage(BitConverter.ToString(Extension.ReadBytes(buffer, 2)));
            }
        }
        [Command("ReadMem")]
        public async Task _Read(string input, string length = "2")
        {
            uint buffer = Convert.ToUInt32(input, 16);
            if (Variables.IsConnected)
            {
                SendMessage(BitConverter.ToString(Extension.ReadBytes(buffer, int.Parse(length))));
            }
        }
        [Command("ReadString")]
        public async Task _ReadS(string address)
        {
            uint buffer = Convert.ToUInt32(address, 16);
            if (Variables.IsConnected)
            {
                SendMessage(Extension.ReadString(buffer));
            }
        }
        [Command("Disconnect")]
        [RequireOwner]
        public async Task _T()
        {
            Context.Client.LogoutAsync();

        }
        [Command("GiveSnakeSocom")]
        public async Task _Socom()
        {
            if (Variables.IsConnected)
                Extension.WriteBytes(0x382922, new byte[] { 0x55, 0x55 });
            SendMessage(string.Format("Thank you, {0}! Snake now has a Socom", Context.User.Username));
        }

        [Command("CheckHealth")]
        [Alias("GetHealth", "HP")]
        public async Task GHealth()
        {
            short current = Extension.ReadInt16(0x382916), max = Extension.ReadInt16(0x382918);
            double per = (double)SwitchInt16(current)/ SwitchInt16(max);
            SendMessage(string.Format("Snake has {0:0.0%} Health!", per));
        }
    }

}
