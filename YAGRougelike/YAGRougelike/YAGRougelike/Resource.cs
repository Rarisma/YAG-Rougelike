using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;

//You can lean to leviate with a little help.
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

        public static List<string> FruitResources = new List<string>();
        public static List<string> TreeResources = new List<string>();
        public static List<string> RareTreeResources = new List<string>();

        public static List<string> PassiveCreatures = new List<string>();
        public static List<string> EnemyPrefix = new List<string>();
        public static List<string> Enemies = new List<string>();
        public static List<string> EnemySuffix = new List<string>();



        public static void ClearResources() //Should be called before using ReloadResources()
        {
            RegularLocations.Clear();
            ForestLocations.Clear();
            CaveLocations.Clear();
            MountainLocations.Clear();
            BushResources.Clear();
            FloorPlantResources.Clear();
            WaterPlantResources.Clear();
            FruitResources.Clear();
            TreeResources.Clear();
            ForestLocations.Clear();
            TreeResources.Clear();
            RareTreeResources.Clear();
            PassiveCreatures.Clear();
            EnemyPrefix.Clear();
            Enemies.Clear();
            EnemySuffix.Clear();

        }

        public static void ReloadResources() //Calling this function will reload all resources
        {
            Resource.RegularLocations.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Terrain//Regular//"));
            Resource.CaveLocations.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Terrain//Caves//"));
            Resource.ForestLocations.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Terrain/Forests"));
            Resource.MountainLocations.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Terrain//Mountains//"));

            Resource.BushResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Bushes"));
            Resource.FloorPlantResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Floor"));
            Resource.WaterPlantResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Waterplants"));

            Resource.FruitResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Fruit"));
            Resource.TreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Normal Trees"));
            Resource.RareTreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Rare Trees"));

            Resource.PassiveCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Passive/Land"));
            Resource.EnemyPrefix.AddRange(Resource.FileBasedResourceLoader("Data//Resources//Creatures//Hostile//Prefix"));
            Resource.Enemies.AddRange(Resource.FileBasedResourceLoader("Data//Resources//Creatures//Hostile//Enemy"));
            Resource.EnemySuffix.AddRange(Resource.FileBasedResourceLoader("Data//Resources//Creatures//Hostile//Suffix"));
        }

        public static List<string> FileBasedResourceLoader(string PathToDirectoryToLoadFrom)
        {
            ///<summary>
            ///This is used for the new resource system which uses a directory to store individual resources rarther than a large text file
            ///</summary>

            DirectoryInfo d = new DirectoryInfo(FileSystem.AppDataDirectory + PathToDirectoryToLoadFrom);
            FileInfo[] Files = d.GetFiles();
            List<string> Resources = new List<string>();
            foreach (FileInfo file in Files) { Resources.Add(Convert.ToString(file.Name)); }
            return Resources;
        }

        public static List<string> TerrainPrefixLoader(string PathToResource)
        {
            string Prefixes = File.ReadLines(FileSystem.AppDataDirectory + PathToResource).Skip(1).Take(1).First();
            List<string> output = new List<string>();
            string temp = "";
            for (int i = 0; i < Prefixes.Length; i++)
            {
                if (Prefixes[i] == System.Convert.ToChar(","))
                {
                    output.Add(temp);
                    temp = "";
                }
                else
                {
                    temp = temp + Prefixes[i];
                }
            }
            return output;
        }
    }
}