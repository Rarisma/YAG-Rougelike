using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Xamarin.Essentials;

//You can lean to leviate with a little help.
namespace YAGRougelike
{
    public class Resource
    {
        //Revisions are changes to generation changes that are hard coded however versions are just updates the might add new items/resources that don't need hardcoded changes
        public static int ResourceRevision = 0; //This helps the app understand if the resources are the latest version

        public static int ResourceVersion;

        public static List<string> RegularLocations = new List<string>();
        public static List<string> WoodTypes = new List<string>();
        public static List<string> CaveLocations = new List<string>();
        public static List<string> MountainLocations = new List<string>();

        public static List<string> BushResources = new List<string>();
        public static List<string> FloorPlantResources = new List<string>();
        public static List<string> WaterPlantResources = new List<string>();

        public static List<string> FruitResources = new List<string>();
        public static List<string> TreeResources = new List<string>();
        public static List<string> RareTreeResources = new List<string>();

        public static List<string> MetalResources = new List<string>();

        public static List<string> PassiveCreatures = new List<string>();
        public static List<string> EnemyPrefix = new List<string>();
        public static List<string> Enemies = new List<string>();
        public static List<string> EnemySuffix = new List<string>();
        public static List<string> ForrestPrefixes = new List<string>();

        public static void ClearResources() //Should be called before using ReloadResources()
        {
            RegularLocations.Clear();
            WoodTypes.Clear();
            CaveLocations.Clear();
            MountainLocations.Clear();
            BushResources.Clear();
            FloorPlantResources.Clear();
            WaterPlantResources.Clear();
            FruitResources.Clear();
            TreeResources.Clear();
            TreeResources.Clear();
            RareTreeResources.Clear();
            PassiveCreatures.Clear();
            EnemyPrefix.Clear();
            Enemies.Clear();
            EnemySuffix.Clear();
            ForrestPrefixes.Clear();
            MetalResources.Clear();
        }

        public static void ReloadResources() //Calling this function will reload all resources
        { //Update to use LibRarisma.CSVToList
            Resource.RegularLocations.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Terrain//Regular//"));
            Resource.CaveLocations.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Terrain//Caves//"));
            Resource.WoodTypes.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Normal Trees"));
            Resource.MountainLocations.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Terrain//Mountains//"));
            Resource.ForrestPrefixes.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Terrain/Forests"));

            Resource.BushResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Bushes"));
            Resource.FloorPlantResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Floor"));
            Resource.WaterPlantResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Waterplants"));

            Resource.FruitResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Fruit"));
            Resource.TreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Normal Trees"));
            Resource.RareTreeResources.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Items/Flora/Rare Trees"));

            Resource.PassiveCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "/Data/Resources/Creatures/Passive/Land"));
            Resource.EnemyPrefix.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Creatures//Hostile//Prefix"));
            Resource.Enemies.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Creatures//Hostile//Enemy"));
            Resource.EnemySuffix.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Creatures//Hostile//Suffix"));
            Resource.MetalResources.AddRange(Resource.FileBasedResourceLoader("//Data//Resources//Items//Metals//"));
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
                    temp += Prefixes[i];
                }
            }
            return output;
        }

        public static List<string> ResourceIDsLoader(string PathToResource) //Could be merged with TerrainPrefixLoader if a lines to skip was added or if sender is checked
        {
            string Prefixes = File.ReadLines(FileSystem.AppDataDirectory + PathToResource).Skip(3).Take(1).First();
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
                    temp += Prefixes[i];
                }
            }
            return output;
        }

        public static bool AreResourcesUpToDate()
        {
            //Step 1 Check if revision is the latest
            int CurrentResourceVersion;
            try { CurrentResourceVersion = Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Metadata//").Skip(6).Take(1).First()); }
            catch { CurrentResourceVersion = -1; } //If this fails for any reason (Eg first run) just assume that //resources// doesnt exist

            //This downloads and saves the update metadata file for comparsion
            using (var client = new System.Net.WebClient()) { client.DownloadFile("https://github.com/Rarisma/YAG-Rougelike/raw/main/Resources/Metadata", FileSystem.AppDataDirectory + "//UpdateMetadata"); }
            int UpdateVersion = Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//UpdateMetadata").Skip(6).Take(1).First());

            return CurrentResourceVersion == UpdateVersion;
        }

        public static string ResourceUpdate()
        {
            try // This will download the resources.zip
            {
                using (var client = new System.Net.WebClient()) { client.DownloadFile("https://github.com/Rarisma/YAG-Rougelike/raw/main/Resources/Resources.zip", FileSystem.AppDataDirectory + "//Resouces.zip"); }
            }
            catch { return "Error Code 1\nFailed to download resources?\nAre you connected to the internet and can you access github?"; } //This should only happen if the user cannot access github

            try { Directory.Delete(FileSystem.AppDataDirectory + "//Data//Resources//", true); } //Deletes Resources folder and any subfolders
            catch { } // This will fail if resources has never been downloaded before so it does nothing

            try { ZipFile.ExtractToDirectory(FileSystem.AppDataDirectory + "//Resouces.zip", FileSystem.AppDataDirectory + "//Data//Resources//"); }
            catch { return "Error Code 2\nFailed to extract resources?\nIs the zip corrupted\n\nYou should check your connection, try again and if this persists contact the developers.\n\nYou are likely to crash if you press continue."; }

            return "Success!";
        }
    }
}