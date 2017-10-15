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


        public void Giveeth(MGS address, string Weapon)
        {
            Extension.WriteBytes((uint)address, new byte[] { 0x14, 0x00 });
            SendMessage(string.Format("{0}, Given! Thanks {1}!!", Weapon, Context.User.Mention));
        }
        public void Taketh(MGS address, string Weapon)
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
                    case "Socom": Giveeth(MGS.Socom, Weap); break;
                    case "socom": Giveeth(MGS.Socom, Weap); break;
                    case "Famas": Giveeth(MGS.Famas, Weap); break;
                    case "famas": Giveeth(MGS.Famas, Weap); break;
                    case "Grenade": Giveeth(MGS.Grenade, Weap); break;
                    case "grenade": Giveeth(MGS.Grenade, Weap); break;
                    case "Nikita": Giveeth(MGS.Nikita, Weap); break;
                    case "nikita": Giveeth(MGS.Nikita, Weap); break;
                    case "Stinger": Giveeth(MGS.Stinger, Weap); break;
                    case "stinger": Giveeth(MGS.Stinger, Weap); break;
                    case "Claymore": Giveeth(MGS.Claymore, Weap); break;
                    case "claymore": Giveeth(MGS.Claymore, Weap); break;
                    case "C4": Giveeth(MGS.C4, Weap); break;
                    case "c4": Giveeth(MGS.C4, Weap); break;
                    case "Stun Grenade": Giveeth(MGS.Stun_G, Weap); break;
                    case "stun grenade": Giveeth(MGS.Stun_G, Weap); break;
                    case "Chaff Grenade": Giveeth(MGS.Chaff_G, Weap); break;
                    case "chaff grenade": Giveeth(MGS.Chaff_G, Weap); break;
                    case "PSG1": Giveeth(MGS.PSG1, Weap); break;
                    case "psg1": Giveeth(MGS.PSG1, Weap); break;
                    case "All": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Everything!!", Weap, Context.User.Mention)); break;
                    case "all": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Everything!!", Weap, Context.User.Mention)); break;
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
                    case "Socom": Taketh(MGS.Socom, Weap); break;
                    case "socom": Taketh(MGS.Socom, Weap); break;
                    case "Famas": Taketh(MGS.Famas, Weap); break;
                    case "famas": Taketh(MGS.Famas, Weap); break;
                    case "Grenade": Taketh(MGS.Grenade, Weap); break;
                    case "grenade": Taketh(MGS.Grenade, Weap); break;
                    case "Nikita": Taketh(MGS.Nikita, Weap); break;
                    case "nikita": Taketh(MGS.Nikita, Weap); break;
                    case "Stinger": Taketh(MGS.Stinger, Weap); break;
                    case "stinger": Taketh(MGS.Stinger, Weap); break;
                    case "Claymore": Taketh(MGS.Claymore, Weap); break;
                    case "claymore": Taketh(MGS.Claymore, Weap); break;
                    case "C4": Taketh(MGS.C4, Weap); break;
                    case "c4": Taketh(MGS.C4, Weap); break;
                    case "Stun Grenade": Taketh(MGS.Stun_G, Weap); break;
                    case "stun grenade": Taketh(MGS.Stun_G, Weap); break;
                    case "Chaff Grenade": Taketh(MGS.Chaff_G, Weap); break;
                    case "chaff grenade": Taketh(MGS.Chaff_G, Weap); break;
                    case "PSG1": Taketh(MGS.PSG1, Weap); break;
                    case "psg1": Taketh(MGS.PSG1, Weap); break;
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
                    case "Cigs": Giveeth(MGS.Cig, Weap); break;
                    case "cigs": Giveeth(MGS.Cig, Weap); break;
                    case "Scope": Giveeth(MGS.Scope, Weap); break;
                    case "scope": Giveeth(MGS.Scope, Weap); break;
                    case "Cardboard Box A": Giveeth(MGS.CardboardBoxA, Weap); break;
                    case "Box A": Giveeth(MGS.CardboardBoxA, Weap); break;
                    case "Cardboard Box B": Giveeth(MGS.CardboardBoxB, Weap); break;
                    case "Box B": Giveeth(MGS.CardboardBoxB, Weap); break;
                    case "Cardboard Box C": Giveeth(MGS.CardboardBoxC, Weap); break;
                    case "Box C": Giveeth(MGS.CardboardBoxC, Weap); break;
                    case "Night Vision Goggles": Giveeth(MGS.NVG, Weap); break;
                    case "NVG": Giveeth(MGS.NVG, Weap); break;
                    case "Thermal Goggles": Giveeth(MGS.ThermG, Weap); break;
                    case "ThermG": Giveeth(MGS.ThermG, Weap); break;
                    case "Gas Mask": Giveeth(MGS.GasM, Weap); break;
                    case "GMask": Giveeth(MGS.GasM, Weap); break;
                    case "Body Armor": Giveeth(MGS.BodyArmor, Weap); break;
                    case "B Armor": Giveeth(MGS.BodyArmor, Weap); break;
                    case "Ketchup": Giveeth(MGS.Ketchup, Weap); break;
                    case "kethcup": Giveeth(MGS.Ketchup, Weap); break;

                    case "Stealth": Giveeth(MGS.Stealth, Weap); break;
                    case "Camo": Giveeth(MGS.Stealth, Weap); break;
                    case "Bandanna": Giveeth(MGS.Bandanna, Weap); break;
                    case "bandanna": Giveeth(MGS.Bandanna, Weap); break;
                    case "Camera": Giveeth(MGS.Camera, Weap); break;
                    case "camera": Giveeth(MGS.Camera, Weap); break;
                    case "Ration": Giveeth(MGS.Ration, Weap); break;
                    case "ration": Giveeth(MGS.Ration, Weap); break;
                    case "Medicine": Giveeth(MGS.Medicine, Weap); break;
                    case "medicine": Giveeth(MGS.Medicine, Weap); break;
                    case "Diazepam": Giveeth(MGS.NVG, Weap); break;
                    case "diazepam": Giveeth(MGS.NVG, Weap); break;
                    case "PAL": Giveeth(MGS.PalKey, Weap); break;
                    case "Pal Key": Giveeth(MGS.PalKey, Weap); break;
                    case "KeyCard": Giveeth(MGS.KeyCard, Weap); break;
                    case "Key": Giveeth(MGS.KeyCard, Weap); break;
                    case "Mine Detector": Giveeth(MGS.MineDetector, Weap); break;
                    case "Mine D": Giveeth(MGS.MineDetector, Weap); break;
                    case "Disc": Giveeth(MGS.Disc, Weap); break;
                    case "disc": Giveeth(MGS.Disc, Weap); break;
                    case "Rope": Giveeth(MGS.Rope, Weap); break;
                    case "rope": Giveeth(MGS.Rope, Weap); break;
                    case "Handkerchief": Giveeth(MGS.Handkerchief, Weap); break;
                    case "handkerchief": Giveeth(MGS.Handkerchief, Weap); break;
                    case "Suppressor": Giveeth(MGS.Suppressor, Weap); break;
                    case "suppressor": Giveeth(MGS.Suppressor, Weap); break;
                    case "All": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Everything!!", Weap, Context.User.Mention)); break;
                    case "all": foreach (var item in Enum.GetValues(typeof(MGS))) Extension.WriteBytes((uint)item, give); SendMessage(string.Format("{1} Gave you Everything!!", Weap, Context.User.Mention)); break;
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
                    case "Cigs": Taketh(MGS.Cig, Weap); break;
                    case "cigs": Taketh(MGS.Cig, Weap); break;
                    case "Scope": Taketh(MGS.Scope, Weap); break;
                    case "scope": Taketh(MGS.Scope, Weap); break;
                    case "Cardboard Box A": Taketh(MGS.CardboardBoxA, Weap); break;
                    case "Box A": Taketh(MGS.CardboardBoxA, Weap); break;
                    case "Cardboard Box B": Taketh(MGS.CardboardBoxB, Weap); break;
                    case "Box B": Taketh(MGS.CardboardBoxB, Weap); break;
                    case "Cardboard Box C": Taketh(MGS.CardboardBoxC, Weap); break;
                    case "Box C": Taketh(MGS.CardboardBoxC, Weap); break;
                    case "Night Vision Goggles": Taketh(MGS.NVG, Weap); break;
                    case "NVG": Taketh(MGS.NVG, Weap); break;
                    case "Thermal Goggles": Taketh(MGS.ThermG, Weap); break;
                    case "ThermG": Taketh(MGS.ThermG, Weap); break;
                    case "Gas Mask": Taketh(MGS.GasM, Weap); break;
                    case "GMask": Taketh(MGS.GasM, Weap); break;
                    case "Body Armor": Taketh(MGS.BodyArmor, Weap); break;
                    case "B Armor": Taketh(MGS.BodyArmor, Weap); break;
                    case "Ketchup": Taketh(MGS.Ketchup, Weap); break;
                    case "kethcup": Taketh(MGS.Ketchup, Weap); break;
                    case "Stealth": Taketh(MGS.Stealth, Weap); break;
                    case "Camo": Taketh(MGS.Stealth, Weap); break;
                    case "Bandanna": Taketh(MGS.Bandanna, Weap); break;
                    case "bandanna": Taketh(MGS.Bandanna, Weap); break;
                    case "Camera": Taketh(MGS.Camera, Weap); break;
                    case "camera": Taketh(MGS.Camera, Weap); break;
                    case "Ration": Taketh(MGS.Ration, Weap); break;
                    case "ration": Taketh(MGS.Ration, Weap); break;
                    case "Medicine": Taketh(MGS.Medicine, Weap); break;
                    case "medicine": Taketh(MGS.Medicine, Weap); break;
                    case "Diazepam": Taketh(MGS.NVG, Weap); break;
                    case "diazepam": Taketh(MGS.NVG, Weap); break;
                    case "PAL": Taketh(MGS.PalKey, Weap); break;
                    case "Pal Key": Taketh(MGS.PalKey, Weap); break;
                    case "KeyCard": Taketh(MGS.KeyCard, Weap); break;
                    case "Key": Taketh(MGS.KeyCard, Weap); break;
                    case "Mine Detector": Taketh(MGS.MineDetector, Weap); break;
                    case "Mine D": Taketh(MGS.MineDetector, Weap); break;
                    case "Disc": Taketh(MGS.Disc, Weap); break;
                    case "disc": Taketh(MGS.Disc, Weap); break;
                    case "Rope": Taketh(MGS.Rope, Weap); break;
                    case "rope": Taketh(MGS.Rope, Weap); break;
                    case "Handkerchief": Taketh(MGS.Handkerchief, Weap); break;
                    case "handkerchief": Taketh(MGS.Handkerchief, Weap); break;
                    case "Suppressor": Taketh(MGS.Suppressor, Weap); break;
                    case "suppressor": Taketh(MGS.Suppressor, Weap); break;
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
