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

namespace YetAnotherGenericRougelikeGame
{
    public class Generate
    {
        public static List<string> RegularLocations = new List<string>();
        public static List<string> ForestLocations = new List<string>();
        public static List<string> CaveLocations = new List<string>();
        public static List<string> MountainLocations = new List<string>();
        
        public static List<string> BushResources = new List<string>();
        public static List<string> FloorPlantResources = new List<string>();
        public static List<string> WaterPlantResources = new List<string>();
        
        public static List<string> FruitTreeResources = new List<string>();
        public static List<string> TreeResources = new List<string>();
        public static List<string> RareTreeResources = new List<string>();

        public static void ClearResources() //Should be called before using ReloadResources()
        {
            RegularLocations.Clear();
            ForestLocations.Clear();
            CaveLocations.Clear();
            MountainLocations.Clear();
            BushResources.Clear();
            FloorPlantResources.Clear();
            WaterPlantResources.Clear();
            FruitTreeResources.Clear();
            TreeResources.Clear();
            ForestLocations.Clear();
            TreeResources.Clear();
            RareTreeResources.Clear();
        }

        public static void ReloadResources() //Calling this function will reload all resources
        {
            Generate.RegularLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Locations/Regular/Regular"));
            Generate.ForestLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Locations/Regular/Forest"));
            Generate.CaveLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Locations/Regular/Caves"));
            Generate.MountainLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Locations/Regular/Mountain"));
            
            Generate.BushResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Plants/Bushes"));
            Generate.FloorPlantResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Plants/Floor"));
            Generate.WaterPlantResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Plants/Waterplants"));
            
            Generate.FruitTreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Trees/Fruit"));
            Generate.TreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Trees/Regular"));
            Generate.RareTreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Trees/Rare"));
        }

        public static string[] Terrain()
        {
            /*Heres how the terrain gen works:
             Random Number      Name            ID
             00-50              Normal          0
             51-80              Forrest         1
             81-90              Cave            2
             91-96              Mountain        3
             97-100             Unique (Not implemented yet)
            ID's are used to let the code understand what list is being picked from

            The code below basically gets a random number and then
            goes to the corresponding if, then it picks a random item
            in the corresponding list.*/

            Random rnd = new Random();
            int TerrainDecider = rnd.Next(0, 100);
            string[] output = { "", "", ""};

            if (TerrainDecider <= 50) //Normal terrain
            {
                output[0] = "0";
                output[1] = Convert.ToString(rnd.Next(0, Generate.RegularLocations.Count()));
                output[2] = Generate.RegularLocations[Convert.ToInt32(output[1])];
            }
            else if (TerrainDecider >= 51 && TerrainDecider <= 80)
            {
                output[0] = "1";
                output[1] = Convert.ToString(rnd.Next(0, Generate.ForestLocations.Count()));
                output[2] = Generate.ForestLocations[Convert.ToInt32(output[1])];
            }
            else if (TerrainDecider >= 81 && TerrainDecider <= 90)
            {
                output[0] = "2";
                output[1] = Convert.ToString(rnd.Next(0, Generate.CaveLocations.Count()));
                output[2] = Generate.CaveLocations[Convert.ToInt32(output[1])];
            }
            else if (TerrainDecider >= 91 && TerrainDecider <= 96)
            {
                output[0] = "3";
                output[1] = Convert.ToString(rnd.Next(0, Generate.MountainLocations.Count()));
                output[2] = Generate.MountainLocations[Convert.ToInt32(output[1])];
            }
            else if (TerrainDecider >= 96 && TerrainDecider <= 100) //should be changed for unique settlements
            {
                output[0] = "2";
                output[1] = Convert.ToString(rnd.Next(0, Generate.CaveLocations.Count()));
                output[2] = Generate.CaveLocations[Convert.ToInt32(output[1])];
            }
            return output;
        }

        public static string[] ResouceGenerate()
        {
            /* Heres how the resource gen works
            Random Value   Name               ID
            
            00-15     -    Bush               0
            16-30     -    Floor plants       1
            31-40     -    Waterplants        2
            41-60     -    Fruit Trees        3
            61-96     -    Regular Trees      4
            96-100    -    Rare               5
            ID's are used to let the code understand what list is being picked from

            The code below basically gets a random number and then
            goes to the corresponding if, then it picks a random item
            in the corresponding list.*/

            Random rnd = new Random();
            int ResourceDecider = rnd.Next(0, 100);
            string[] output = { "", "", ""};

            if (ResourceDecider <= 15)
            {
                output[0] = "0";
                output[1] = Convert.ToString(rnd.Next(0, Generate.BushResources.Count()));
                output[2] = Generate.BushResources[Convert.ToInt32(output[1])];
            }
            else if (ResourceDecider >= 16 && ResourceDecider <= 30)
            {
                output[0] = "1";
                output[1] = Convert.ToString(rnd.Next(0, Generate.FloorPlantResources.Count()));
                output[2] = Generate.FloorPlantResources[Convert.ToInt32(output[1])];
            }
            else if (ResourceDecider >= 31 && ResourceDecider <= 40)
            {
                output[0] = "2";
                output[1] = Convert.ToString(rnd.Next(0, Generate.WaterPlantResources.Count()));
                output[2] = Generate.WaterPlantResources[Convert.ToInt32(output[1])];
            }
            else if (ResourceDecider >= 41 && ResourceDecider <= 60)
            {
                output[0] = "3";
                output[1] = Convert.ToString(rnd.Next(0, Generate.FruitTreeResources.Count()));
                output[2] = Generate.FruitTreeResources[Convert.ToInt32(output[1])];
            }
            else if (ResourceDecider >= 61 && ResourceDecider <= 96)
            {
                output[0] = "4";
                output[1] = Convert.ToString(rnd.Next(0, Generate.TreeResources.Count()));
                output[2] = Generate.TreeResources[Convert.ToInt32(output[1])];
            }
            else if (ResourceDecider >= 97 && ResourceDecider <= 100)
            {
                output[0] = "4";
                output[1] = Convert.ToString(rnd.Next(0, Generate.TreeResources.Count()));
                output[2] = Generate.TreeResources[Convert.ToInt32(output[1])];
            }
            return output;
        }
    }
}
