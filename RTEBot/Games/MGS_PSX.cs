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
        public CCAPI CCAPI { get { return new CCAPI(); } }
        System.Timers.Timer aTimer;
        bool infammo = false;
        public async void SendMessage(string input)
        {
            await Context.Channel.SendMessageAsync(input);
        }
        public short SwitchInt16(short input)
        {
            return (short)((input << 8) + (input >> 8));
        }
        public enum current : int //0x38291C
        {
            non = 0xFFFF,
            socom = 0x0000,
            famas = 0x0100,
            grenade = 0x0200,
            nikita = 0x0300,
            stinger = 0x0400,
            claymore = 0x0500,
            c4 = 0x0600,
            stng = 0x0700,
            chg = 0x0800,
            psg1 = 0x0900
        }
        [Command("InfAmmo")]
        public async Task _InfAmmo()
        {
            infammo = !infammo;
            Console.WriteLine(infammo.ToString());
            while (infammo)
            {
                int _current = Extension.ReadInt16(0x38291C);
                switch (_current)
                {
                    case (int)current.socom: Giveth(MGS.Socom); break;
                    case (int)current.famas: Giveth(MGS.Famas); break;
                    case (int)current.grenade: Giveth(MGS.Grenade); break;
                    case (int)current.nikita: Giveth(MGS.Nikita); break;
                    case (int)current.stinger: Giveth(MGS.Stinger); break;
                    case (int)current.claymore: Giveth(MGS.Claymore); break;
                    case (int)current.c4: Giveth(MGS.C4); break;
                    case (int)current.stng: Giveth(MGS.Stun_G); break;
                    case (int)current.chg: Giveth(MGS.Chaff_G); break;
                    case (int)current.psg1: Giveth(MGS.PSG1); break;
                    case (int)current.non: infammo = false; break;
                }
                Extension.WriteBytes(0x379553, new byte[] { 0x55, 0x55 });
            }
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


        public void Giveth(MGS address, string Weapon)
        {
            Extension.WriteBytes((uint)address, new byte[] { 0x14, 0x00 });
            SendMessage(string.Format("{0}, Given! Thanks {1}!!", Weapon, Context.User.Mention));
        }
        public void Giveth(MGS address)
        {
            Extension.WriteBytes((uint)address, new byte[] { 0x14, 0x00 });
        }
        public void Taketh(MGS address, string Weapon)
        {
            Extension.WriteBytes((uint)address, new byte[] { 0xFF, 0xFF});
            SendMessage(string.Format("{1} Took your {0}!", Weapon, Context.User.Mention));
        }

        [Command("Give")]
        public async Task _Give(string Weap)
        {//Next time try ToLower or ToUpper :P dingus
            if (Variables.IsConnected)
            {
                string stuff = "";
                stuff = Weap.Replace(" ", "_").ToLower();
                Console.WriteLine(stuff);
                byte[] give = { 0x14, 0x00 };
                switch (stuff)
                {
                    case "socom": Giveth(MGS.Socom, Weap); break;
                    case "famas": Giveth(MGS.Famas, Weap); break;
                    case "grenade": Giveth(MGS.Grenade, Weap); break;
                    case "nikita": Giveth(MGS.Nikita, Weap); break;
                    case "stinger": Giveth(MGS.Stinger, Weap); break;
                    case "claymore": Giveth(MGS.Claymore, Weap); break;
                    case "c4": Giveth(MGS.C4, Weap); break;
                    case "stun_grenade": Giveth(MGS.Stun_G, Weap); break;
                    case "chaff_grenade": Giveth(MGS.Chaff_G, Weap); break;
                    case "psg1": Giveth(MGS.PSG1, Weap); break;

                    case "cigs": Giveth(MGS.Cig, Weap); break;
                    case "scope": Giveth(MGS.Scope, Weap); break;
                    case "cardboard_box_a": Giveth(MGS.CardboardBoxA, Weap); break;
                    case "cardboard_box_b": Giveth(MGS.CardboardBoxB, Weap); break;
                    case "cardboard_box_c": Giveth(MGS.CardboardBoxC, Weap); break;
                    case "night_vision_goggles": Giveth(MGS.NVG, Weap); break;
                    case "thermal_goggles": Giveth(MGS.ThermG, Weap); break;
                    case "gas_mask": Giveth(MGS.GasM, Weap); break;
                    case "body_armor": Giveth(MGS.BodyArmor, Weap); break;
                    case "ketchup": Giveth(MGS.Ketchup, Weap); break;
                    case "stealth": Giveth(MGS.Stealth, Weap); break;
                    case "bandana": Giveth(MGS.Bandanna, Weap); break;
                    case "camera": Giveth(MGS.Camera, Weap); break;
                    case "ration": Giveth(MGS.Ration, Weap); break;
                    case "medicine": Giveth(MGS.Medicine, Weap); break;
                    case "diazepam": Giveth(MGS.NVG, Weap); break;
                    case "pal": Giveth(MGS.PalKey, Weap); break;
                    case "key": Giveth(MGS.KeyCard, Weap); break;
                    case "mine_detector": Giveth(MGS.MineDetector, Weap); break;
                    case "disc": Giveth(MGS.Disc, Weap); break;
                    case "rope": Giveth(MGS.Rope, Weap); break;
                    case "handkerchief": Giveth(MGS.Handkerchief, Weap); break;
                    case "suppressor": Giveth(MGS.Suppressor, Weap); break;
                        //case "All": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Everything!!", Weap, Context.User.Mention)); break;
                        case "all": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Everything!!", Weap, Context.User.Mention)); break;
                }
            }
        }
        [Command("Take")]
        public async Task _Take(string Weap)
        {
            string stuff = "";
            stuff = Weap.Replace(" ", "_").ToLower();
            if (Variables.IsConnected)
            {
                byte[] take = { 0xFF, 0xFF };
                switch (stuff)
                {
                    case "socom": Taketh(MGS.Socom, Weap); break;
                    case "famas": Taketh(MGS.Famas, Weap); break;
                    case "grenade": Taketh(MGS.Grenade, Weap); break;
                    case "nikita": Taketh(MGS.Nikita, Weap); break;
                    case "stinger": Taketh(MGS.Stinger, Weap); break;
                    case "claymore": Taketh(MGS.Claymore, Weap); break;
                    case "c4": Taketh(MGS.C4, Weap); break;
                    case "stun_grenade": Taketh(MGS.Stun_G, Weap); break;
                    case "chaff_grenade": Taketh(MGS.Chaff_G, Weap); break;
                    case "psg1": Taketh(MGS.PSG1, Weap); break;
                    case "cigs": Taketh(MGS.Cig, Weap); break;
                    case "scope": Taketh(MGS.Scope, Weap); break;
                    case "cardboard_box_a" : Taketh(MGS.CardboardBoxA, Weap); break;
                    case "cardboard_box_b": Taketh(MGS.CardboardBoxB, Weap); break;
                    case "cardboard_box_c": Taketh(MGS.CardboardBoxC, Weap); break;
                    case "night_vision_goggles": Taketh(MGS.NVG, Weap); break;
                    case "thermal_goggles": Taketh(MGS.ThermG, Weap); break;
                    case "gas_mask": Taketh(MGS.GasM, Weap); break;
                    case "body_armor": Taketh(MGS.BodyArmor, Weap); break;
                    case "ketchup": Taketh(MGS.Ketchup, Weap); break;
                    case "stealth": Taketh(MGS.Stealth, Weap); break;
                    case "bandana": Taketh(MGS.Bandanna, Weap); break;
                    case "camera": Taketh(MGS.Camera, Weap); break;
                    case "ration": Taketh(MGS.Ration, Weap); break;
                    case "medicine": Taketh(MGS.Medicine, Weap); break;
                    case "diazepam": Taketh(MGS.NVG, Weap); break;
                    case "pal": Taketh(MGS.PalKey, Weap); break;
                    case "key": Taketh(MGS.KeyCard, Weap); break;
                    case "mine_detector": Taketh(MGS.MineDetector, Weap); break;
                    case "disc": Taketh(MGS.Disc, Weap); break;
                    case "rope": Taketh(MGS.Rope, Weap); break;
                    case "handkerchief": Taketh(MGS.Handkerchief, Weap); break;
                    case "suppressor": Taketh(MGS.Suppressor, Weap); break;
                    case "All": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, take); SendMessage(string.Format("{1} Took EVERYTHING!!", Weap, Context.User.Mention)); break;
                    case "all": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, take); SendMessage(string.Format("{1} Took EVERYTHING!!", Weap, Context.User.Mention)); break;
                }
            }
        }
        private byte[] STB(string hex)
        {
            if ((hex.Length % 2) > 0)
            {
                hex = "0" + hex;
            }
            int length = hex.Length;
            byte[] buffer = new byte[((length / 2) - 1) + 1];
            for (int i = 0; i < length; i += 2)
            {
                buffer[i / 2] = Convert.ToByte(hex.Substring(i, 2), 0x10);
            }
            return buffer;
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
        [Alias("Give Health", "Save")]
        public async Task GiveHealth()
        {
            short max = Extension.ReadInt16(0x382918);
            Extension.WriteInt16(0x382916, max);
        }
        [Command("KillSnake")]
        [Alias("Kill Snake", "Kill")]
        public async Task KillSnke()
        {
            Extension.WriteBytes(0x38296E, new byte[] { 0x14, 0x00 });
            SendMessage(string.Format("{0}, You're a Monster!!!", Context.User.Username));
            CCAPI.Notify(CCAPI.NotifyIcon.CAUTION, "Time Bomb Added!!");
        }
    }
}
