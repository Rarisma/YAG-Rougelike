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
/* Yet Another Rougelike - ZeroTwo Edition
⣿⣿⣿⣿⣯⣿⣿⠄⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠈⣿⣿⣿⣿⣿⣿⣆⠄
⢻⣿⣿⣿⣾⣿⢿⣢⣞⣿⣿⣿⣿⣷⣶⣿⣯⣟⣿⢿⡇⢃⢻⣿⣿⣿⣿⣿⢿⡄
⠄⢿⣿⣯⣏⣿⣿⣿⡟⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣧⣾⢿⣮⣿⣿⣿⣿⣾⣷
⠄⣈⣽⢾⣿⣿⣿⣟⣄⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣝⣯⢿⣿⣿⣿⣿
⣿⠟⣫⢸⣿⢿⣿⣾⣿⢿⣿⣿⢻⣿⣿⣿⢿⣿⣿⣿⢸⣿⣼⣿⣿⣿⣿⣿⣿⣿
⡟⢸⣟⢸⣿⠸⣷⣝⢻⠘⣿⣿⢸⢿⣿⣿⠄⣿⣿⣿⡆⢿⣿⣼⣿⣿⣿⣿⢹⣿
⡇⣿⡿⣿⣿⢟⠛⠛⠿⡢⢻⣿⣾⣞⣿⡏⠖⢸⣿⢣⣷⡸⣇⣿⣿⣿⢼⡿⣿⣿
⣡⢿⡷⣿⣿⣾⣿⣷⣶⣮⣄⣿⣏⣸⣻⣃⠭⠄⠛⠙⠛⠳⠋⣿⣿⣇⠙⣿⢸⣿
⠫⣿⣧⣿⣿⣿⣿⣿⣿⣿⣿⣿⠻⣿⣾⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣹⢷⣿⡼⠋
⠄⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⣿⣿⣿⠄⠄
⠄⠄⢻⢹⣿⠸⣿⣿⣿⣿⣿⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣼⣿⣿⣿⣿⡟⠄⠄
⠄⠄⠈⢸⣿⠄⠙⢿⣿⣿⣹⣿⣿⣿⣿⣟⡃⣽⣿⣿⡟⠁⣿⣿⢻⣿⣿⢿⠄⠄
⠄⠄⠄⠘⣿⡄⠄⠄⠙⢿⣿⣿⣾⣿⣷⣿⣿⣿⠟⠁⠄⠄⣿⣿⣾⣿⡟⣿⠄⠄
⠄⠄⠄⠄⢻⡇⠸⣆⠄⠄⠈⠻⣿⡿⠿⠛⠉⠄⠄⠄⠄⢸⣿⣇⣿⣿⢿⣿⠄
*/

namespace YetAnotherGenericRougelikeGame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameView : ContentPage
    {
        public GameView()
        {
            InitializeComponent();
 
            WorldGeneration.ReloadResources();
            TXTLocation.Text = WorldGeneration.LocationGenerate();
            Task.Delay(10000);

        }
    }

    public class WorldGeneration
    {
        public static List<string> RegularLocations = new List<string>(); // Places that have no special terrain effects
        public static List<string> ForestLocations = new List<string>(); 
        public static List<string> CaveLocations = new List<string>(); 
        public static List<string> MountainLocations = new List<string>(); 

        public static void ReloadResources() //Calling this function will reload all resources
        {
            RegularLocations.Clear();
            ForestLocations.Clear();
            CaveLocations.Clear();
            MountainLocations.Clear();

            WorldGeneration.RegularLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "\\Data\\Resources\\World\\Locations\\Regular\\Regular"));
            WorldGeneration.ForestLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "\\Data\\Resources\\World\\Locations\\Regular\\Forest"));
            WorldGeneration.CaveLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "\\Data\\Resources\\World\\Locations\\Regular\\Caves"));
            WorldGeneration.MountainLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "\\Data\\Resources\\World\\Locations\\Regular\\Mountain"));
        }


        public static string LocationGenerate()
        {
            Random rnd = new Random();
            int TerrainDecider = rnd.Next(0,100);
            /*Heres how the terrain gen works:
             00-50 Normal
             51-80 Forrest
             81-90 Cave
             91-96 Mountain
             97-100 - Unique (Not implemented yet)*/
            string Location = "";

            //The following if's find the value of Terrain decider and then picks a random item from the corresponding list
            if (TerrainDecider <= 50) {Location = WorldGeneration.RegularLocations[rnd.Next(0, WorldGeneration.RegularLocations.Count())]; }
            else if (TerrainDecider >= 51 && TerrainDecider <= 80) {Location = WorldGeneration.ForestLocations[rnd.Next(0, WorldGeneration.ForestLocations.Count())]; }
            else if (TerrainDecider >= 81 && TerrainDecider <= 90) { Location = WorldGeneration.CaveLocations[rnd.Next(0, WorldGeneration.CaveLocations.Count())]; }
            else if (TerrainDecider >= 91 && TerrainDecider <= 96) { Location = WorldGeneration.MountainLocations[rnd.Next(0, WorldGeneration.MountainLocations.Count())]; }
            else if (TerrainDecider >= 96 && TerrainDecider <= 100) { Location = WorldGeneration.CaveLocations[rnd.Next(0, WorldGeneration.CaveLocations.Count())]; } //should be changed for unique settlements

            return Location;
        }
    }
}