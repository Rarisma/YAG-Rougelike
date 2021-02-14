using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//This class stores playerdata such as Coordinates, Xp, Level, inventory ect.
namespace YAGRougelike
{
    public class PlayerData
    {
        public static Int32[] Coordinates = { 0, 0};

        public static void Reset()
        {
            Random rnd = new Random();
            PlayerData.Coordinates[0] = rnd.Next(-100000, 100000);
            PlayerData.Coordinates[1] = rnd.Next(-100000, 100000);

        }
    }
}
