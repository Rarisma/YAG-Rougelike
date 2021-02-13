using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;

namespace YAGRougelike
{
    public class Resource
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

        public static List<string> PassiveCreatures = new List<string>();
        public static List<string> LesserPrefixHostileCreatures = new List<string>();
        public static List<string> LesserNameHostileCreatures = new List<string>();
        public static List<string> PrefixHostileCreatures = new List<string>();
        public static List<string> NameHostileCreatures = new List<string>();
        public static List<string> SuffixHostileCreatures = new List<string>();
        public static List<string> GreaterPrefixHostileCreatures = new List<string>();
        public static List<string> GreaterNameHostileCreatures = new List<string>();
        public static List<string> GreaterSuffixHostileCreatures = new List<string>();

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
            PassiveCreatures.Clear();
            LesserPrefixHostileCreatures.Clear();
            LesserNameHostileCreatures.Clear();
            PrefixHostileCreatures.Clear();
            NameHostileCreatures.Clear();
            SuffixHostileCreatures.Clear();
            GreaterPrefixHostileCreatures.Clear();
            GreaterNameHostileCreatures.Clear();
            GreaterSuffixHostileCreatures.Clear();
        }
        public static void ReloadResources() //Calling this function will reload all resources
        {
            Resource.RegularLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Locations/Regular/Regular"));
            Resource.ForestLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Locations/Regular/Forest"));
            Resource.CaveLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Locations/Regular/Caves"));
            Resource.MountainLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Locations/Regular/Mountain"));

            Resource.BushResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Plants/Bushes"));
            Resource.FloorPlantResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Plants/Floor"));
            Resource.WaterPlantResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Plants/Waterplants"));

            Resource.FruitTreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Trees/Fruit"));
            Resource.TreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Trees/Regular"));
            Resource.RareTreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/World/Trees/Rare"));

            Resource.PassiveCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Passive/Land/Names"));
            Resource.LesserPrefixHostileCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Hostile/Lesser/Prefix"));
            Resource.LesserNameHostileCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Hostile/Lesser/Enemy"));
            Resource.PrefixHostileCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Hostile/Normal/Prefix"));
            Resource.NameHostileCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Hostile/Normal/Enemy"));
            //Resource.SuffixHostileCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Hostile/Normal/Suffix"));
            Resource.GreaterPrefixHostileCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Hostile/Greater/Prefix"));
            Resource.GreaterNameHostileCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Hostile/Greater/Enemy"));
            //Resource.GreaterSuffixHostileCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Hostile/Greater/Suffix"));
        }
    }
}
