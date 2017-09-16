using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTEBot.Modules;
using Discord.WebSocket;
using MultiLib;

namespace RTEBot.Games
{
    class MGS_PSX : ModuleBase<SocketCommandContext>
    {
        DiscordSocketClient _client;
        public MGS_PSX(DiscordSocketClient client)
        {
            client = _client;
        }
        public GameShark GS { get { return new GameShark(); } }
        public CCAPI CCAPI { get { return new CCAPI(); } }
        public Extension Extension { get { return new Extension(SelectAPI.ControlConsole); } }
        public async void SendMessage(string input)
        {
            await Context.Channel.SendMessageAsync(input);
        }
        public short SwitchInt16(short input)
        {
            return (short)((input << 8) + (input >> 8));
        }
        public enum MGS : uint
        {
            Socom = 0x382922,
            Famas = 0x382924,
            Grenade = 0x382926,
            Nikita = 0x382928,
            Stinger = 0x38292A,
            Claymore = 0x38292C,
            C4 = 0x38292E,
            Stun_G = 0x382930,
            Chaff_G = 0x382932,
            PSG1 = 0x382934
        }

        [Command("GiveWeapon")]
        [Alias("Give")]
        public async Task GiveWeapon(string Weap)
        {
            if (Variables.IsConnected)
            {
                byte[] give = { 0x10, 0x00 };
                switch (Weap)
                {
                    case "Socom": Extension.WriteBytes((uint)MGS.Socom, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "Famas": Extension.WriteBytes((uint)MGS.Famas, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "Greanade": Extension.WriteBytes((uint)MGS.Grenade, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "Nikita": Extension.WriteBytes((uint)MGS.Nikita, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "Stinger": Extension.WriteBytes((uint)MGS.Stinger, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "Claymore": Extension.WriteBytes((uint)MGS.Claymore, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "C4": Extension.WriteBytes((uint)MGS.C4, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "Stun Grenade": Extension.WriteBytes((uint)MGS.Stun_G, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "Chaff Grenade": Extension.WriteBytes((uint)MGS.Chaff_G, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "PSG1": Extension.WriteBytes((uint)MGS.PSG1, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "socom": Extension.WriteBytes((uint)MGS.Socom, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "famas": Extension.WriteBytes((uint)MGS.Famas, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "greanade": Extension.WriteBytes((uint)MGS.Grenade, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "nikita": Extension.WriteBytes((uint)MGS.Nikita, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "stinger": Extension.WriteBytes((uint)MGS.Stinger, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "claymore": Extension.WriteBytes((uint)MGS.Claymore, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "c4": Extension.WriteBytes((uint)MGS.C4, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "stun grenade": Extension.WriteBytes((uint)MGS.Stun_G, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "chaff grenade": Extension.WriteBytes((uint)MGS.Chaff_G, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "psg1": Extension.WriteBytes((uint)MGS.PSG1, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "All": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                    case "all": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{0} Given! Thanks {1}!!", Weap, Context.User.Mention)); break;
                }
            }
        }
        [Command("GetCurrentWeapon")]
        public async Task CurrentWeapon()
        {

        }
        [Command("CheckHealth")]
        [Alias("GetHealth", "HP")]
        public async Task GHealth()
        {
            short current = Extension.ReadInt16(0x382916), max = Extension.ReadInt16(0x382918);
            double per = (double)SwitchInt16(current) / SwitchInt16(max);
            SendMessage(string.Format("Snake has {0:0.0%} Health!", per));
        }
    }
}
