using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiLib;

namespace RTEBot.Modules
{
    public class Variables
    {
        public static byte[] STB(string hex)
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
        public static Boolean IsConnected { get; set; }
        public enum MGSWeapons : uint
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
    }
}
