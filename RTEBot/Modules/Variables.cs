﻿using System;
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
        public static GameShark GS { get { return new GameShark(); } }
        private static CCAPI CCAPI { get { return new CCAPI(); } }
        public static Boolean IsConnected { get; set; }
        public static string _IP = "192.168.0.13";
        public static String[] Admins = { "Cain532", "iHawe", "Luckeyy" };
    }
}
