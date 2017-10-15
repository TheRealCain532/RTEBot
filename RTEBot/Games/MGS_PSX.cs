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
    public class MGS_PSX : ModuleBase<SocketCommandContext>
    {
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
            PSG1 = 0x382934,

            Cig = 0x38294A,
            Scope = 0x38294C,
            CardboardBoxA = 0x38294E,
            CardboardBoxB = 0x382950,
            CardboardBoxC = 0x382952,
            NVG = 0x382954,
            ThermG = 0x382956,
            GasM = 0x382958,
            BodyArmor = 0x38295A,
            Ketchup = 0x38295C,
            Stealth = 0x38295E,
            Bandanna = 0x382960,
            Camera = 0x382962,
            Ration = 0x382964,
            Medicine = 0x382966,
            Diazepam = 0x382968,
            PalKey = 0x38296A,
            KeyCard = 0x38296C,
            //TimeBomb = 0x38296E,
            MineDetector = 0x382970,
            Disc = 0x382972,
            Rope = 0x382974,
            Handkerchief = 0x382976,
            Suppressor = 0x382978,
        }

        public void GiveWeapon(MGS address, string Weapon)
        {
            Extension.WriteBytes((uint)address, new byte[] { 0x14, 0x00 });
            SendMessage(string.Format("{0}, Given! Thanks {1}!!", Weapon, Context.User.Mention));
        }
        public void TakeWeapon(MGS address, string Weapon)
        {
            Extension.WriteBytes((uint)address, new byte[] { 0xFF, 0xFF});
            SendMessage(string.Format("{{1} Took your {0}!", Weapon, Context.User.Mention));
        }
        [Command("GiveWeapon")]
        [Alias("Give")]
        public async Task _GiveWeapon(string Weap)
        {
            if (Variables.IsConnected)
            {
                byte[] give = { 0x14, 0x00 };
                switch (Weap)
                {
                    case "Socom": GiveWeapon(MGS.Socom, Weap); break;
                    case "socom": GiveWeapon(MGS.Socom, Weap); break;
                    case "Famas": GiveWeapon(MGS.Famas, Weap); break;
                    case "famas": GiveWeapon(MGS.Famas, Weap); break;
                    case "Grenade": GiveWeapon(MGS.Grenade, Weap); break;
                    case "grenade": GiveWeapon(MGS.Grenade, Weap); break;
                    case "Nikita": GiveWeapon(MGS.Nikita, Weap); break;
                    case "nikita": GiveWeapon(MGS.Nikita, Weap); break;
                    case "Stinger": GiveWeapon(MGS.Stinger, Weap); break;
                    case "stinger": GiveWeapon(MGS.Stinger, Weap); break;
                    case "Claymore": GiveWeapon(MGS.Claymore, Weap); break;
                    case "claymore": GiveWeapon(MGS.Claymore, Weap); break;
                    case "C4": GiveWeapon(MGS.C4, Weap); break;
                    case "c4": GiveWeapon(MGS.C4, Weap); break;
                    case "Stun Grenade": GiveWeapon(MGS.Stun_G, Weap); break;
                    case "stun grenade": GiveWeapon(MGS.Stun_G, Weap); break;
                    case "Chaff Grenade": GiveWeapon(MGS.Chaff_G, Weap); break;
                    case "chaff grenade": GiveWeapon(MGS.Chaff_G, Weap); break;
                    case "PSG1": GiveWeapon(MGS.PSG1, Weap); break;
                    case "psg1": GiveWeapon(MGS.PSG1, Weap); break;
                    case "All": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Every Weapon!!", Weap, Context.User.Mention)); break;
                    case "all": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Every Weapon!!", Weap, Context.User.Mention)); break;
                }
            }
        }
        [Command("TakeWeapon")]
        [Alias("Take")]
        public async Task _TakeWeapon(string Weap)
        {
            if (Variables.IsConnected)
            {
                byte[] take = { 0xFF, 0xFF };
                switch (Weap)
                {
                    case "Socom": TakeWeapon(MGS.Socom, Weap); break;
                    case "socom": TakeWeapon(MGS.Socom, Weap); break;
                    case "Famas": TakeWeapon(MGS.Famas, Weap); break;
                    case "famas": TakeWeapon(MGS.Famas, Weap); break;
                    case "Grenade": TakeWeapon(MGS.Grenade, Weap); break;
                    case "grenade": TakeWeapon(MGS.Grenade, Weap); break;
                    case "Nikita": TakeWeapon(MGS.Nikita, Weap); break;
                    case "nikita": TakeWeapon(MGS.Nikita, Weap); break;
                    case "Stinger": TakeWeapon(MGS.Stinger, Weap); break;
                    case "stinger": TakeWeapon(MGS.Stinger, Weap); break;
                    case "Claymore": TakeWeapon(MGS.Claymore, Weap); break;
                    case "claymore": TakeWeapon(MGS.Claymore, Weap); break;
                    case "C4": TakeWeapon(MGS.C4, Weap); break;
                    case "c4": TakeWeapon(MGS.C4, Weap); break;
                    case "Stun Grenade": TakeWeapon(MGS.Stun_G, Weap); break;
                    case "stun grenade": TakeWeapon(MGS.Stun_G, Weap); break;
                    case "Chaff Grenade": TakeWeapon(MGS.Chaff_G, Weap); break;
                    case "chaff grenade": TakeWeapon(MGS.Chaff_G, Weap); break;
                    case "PSG1": TakeWeapon(MGS.PSG1, Weap); break;
                    case "psg1": TakeWeapon(MGS.PSG1, Weap); break;
                    case "All": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, take); SendMessage(string.Format("{1} Took EVERYTHING!!", Weap, Context.User.Mention)); break;
                    case "all": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, take); SendMessage(string.Format("{1} Took EVERYTHING!!", Weap, Context.User.Mention)); break;
                }
            }
        }
        [Command("Giveitem")]
        [Alias("Give")]
        public async Task _GiveItem(string Weap)
        {
            if (Variables.IsConnected)
            {
                byte[] give = { 0x14, 0x00 };
                switch (Weap)
                {
                    case "Socom": GiveWeapon(MGS.Socom, Weap); break;
                    case "socom": GiveWeapon(MGS.Socom, Weap); break;
                    case "Famas": GiveWeapon(MGS.Famas, Weap); break;
                    case "famas": GiveWeapon(MGS.Famas, Weap); break;
                    case "Grenade": GiveWeapon(MGS.Grenade, Weap); break;
                    case "grenade": GiveWeapon(MGS.Grenade, Weap); break;
                    case "Nikita": GiveWeapon(MGS.Nikita, Weap); break;
                    case "nikita": GiveWeapon(MGS.Nikita, Weap); break;
                    case "Stinger": GiveWeapon(MGS.Stinger, Weap); break;
                    case "stinger": GiveWeapon(MGS.Stinger, Weap); break;
                    case "Claymore": GiveWeapon(MGS.Claymore, Weap); break;
                    case "claymore": GiveWeapon(MGS.Claymore, Weap); break;
                    case "C4": GiveWeapon(MGS.C4, Weap); break;
                    case "c4": GiveWeapon(MGS.C4, Weap); break;
                    case "Stun Grenade": GiveWeapon(MGS.Stun_G, Weap); break;
                    case "stun grenade": GiveWeapon(MGS.Stun_G, Weap); break;
                    case "Chaff Grenade": GiveWeapon(MGS.Chaff_G, Weap); break;
                    case "chaff grenade": GiveWeapon(MGS.Chaff_G, Weap); break;
                    case "PSG1": GiveWeapon(MGS.PSG1, Weap); break;
                    case "psg1": GiveWeapon(MGS.PSG1, Weap); break;
                    case "All": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Every Weapon!!", Weap, Context.User.Mention)); break;
                    case "all": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Every Weapon!!", Weap, Context.User.Mention)); break;
                }
            }
        }
        [Command("Takeitem")]
        [Alias("Take")]
        public async Task _Takeitem(string Weap)
        {
            if (Variables.IsConnected)
            {
                byte[] take = { 0xFF, 0xFF };
                switch (Weap)
                {
                    case "Socom": TakeWeapon(MGS.Socom, Weap); break;
                    case "socom": TakeWeapon(MGS.Socom, Weap); break;
                    case "Famas": TakeWeapon(MGS.Famas, Weap); break;
                    case "famas": TakeWeapon(MGS.Famas, Weap); break;
                    case "Grenade": TakeWeapon(MGS.Grenade, Weap); break;
                    case "grenade": TakeWeapon(MGS.Grenade, Weap); break;
                    case "Nikita": TakeWeapon(MGS.Nikita, Weap); break;
                    case "nikita": TakeWeapon(MGS.Nikita, Weap); break;
                    case "Stinger": TakeWeapon(MGS.Stinger, Weap); break;
                    case "stinger": TakeWeapon(MGS.Stinger, Weap); break;
                    case "Claymore": TakeWeapon(MGS.Claymore, Weap); break;
                    case "claymore": TakeWeapon(MGS.Claymore, Weap); break;
                    case "C4": TakeWeapon(MGS.C4, Weap); break;
                    case "c4": TakeWeapon(MGS.C4, Weap); break;
                    case "Stun Grenade": TakeWeapon(MGS.Stun_G, Weap); break;
                    case "stun grenade": TakeWeapon(MGS.Stun_G, Weap); break;
                    case "Chaff Grenade": TakeWeapon(MGS.Chaff_G, Weap); break;
                    case "chaff grenade": TakeWeapon(MGS.Chaff_G, Weap); break;
                    case "PSG1": TakeWeapon(MGS.PSG1, Weap); break;
                    case "psg1": TakeWeapon(MGS.PSG1, Weap); break;
                    case "All": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, take); SendMessage(string.Format("{1} Took EVERYTHING!!", Weap, Context.User.Mention)); break;
                    case "all": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, take); SendMessage(string.Format("{1} Took EVERYTHING!!", Weap, Context.User.Mention)); break;
                }
            }
        }
        [Command("CheckHealth")]
        [Alias("GetHealth", "HP")]
        public async Task GHealth()
        {
            short current = Extension.ReadInt16(0x382916), max = Extension.ReadInt16(0x382918);
            double per = (double)SwitchInt16(current) / SwitchInt16(max);
            SendMessage(string.Format("Snake has {0:0.0%} Health!", per));
        }

        [Command("GiveHealth")]
        [Alias("Give Health")]
        public async Task GiveHealth()
        {
            short max = Extension.ReadInt16(0x382918);
            Extension.WriteInt16(0x382916, max);
        }
        [Command("KillSnake")]
        [Alias("Kill Snake")]
        public async Task KillSnke()
        {
            Extension.WriteBytes(0x38296E, new byte[] { 0x01, 0x00 });
            SendMessage(string.Format("{0}, you killed Snake! You Bastard!", Context.User.Username));
        }
    }
}
