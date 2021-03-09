using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Xamarin.Essentials;

//1.5 to stay alive.
namespace YAGRougelike
{
    public class GameData
    {
        //Revisions are changes to generation changes that are hard coded however versions are just updates the might add new items/resources that don't need hardcoded changes
        public static int[] ResourceRevision = { 1, 0 }; //This helps the app understand if the resources are the latest version

        public static List<List<string>> Terrain = new List<List<string>>();        // 0 - Regular      1 - Forrest     2 - Caves       3 - Mountains
        public static List<List<string>> Enemy = new List<List<string>>();          // 0 - Prefix       1 - Name        2 - Suffix
        public static List<List<string>> Resources = new List<List<string>>();      // 0 - Random       1 - Shards      2 - Crystals    3 - Farm        4 - Fruit       5 - Waterplants     6 - Trees     7 - Metal
        public static List<string> PassiveCreatures = new List<string>();

        public static bool DisableCustomResources = false;

        public static Int32[] PlayerDataCoordinates = { 0, 0 };
        public static List<string> PlayerInventory = new List<string>();
        public static List<Int64> PlayerInventoryAmmount = new List<Int64>();
        public static List<Int64> Debugint = new List<Int64>();

        public static void ClearResources() //Should be called before using ReloadResources()
        {
            Terrain.Clear();
            Enemy.Clear();
            Resources.Clear();
            PassiveCreatures.Clear();
            for (int a = 0; a < 10; a++)
            {
                GameData.Terrain.Add(new List<string> { });
                GameData.Enemy.Add(new List<string> { });
                GameData.Resources.Add(new List<string> { });
            }

            DisableCustomResources = false;
        }

        public static void ReloadResources() //Calling this function will reload all resources
        { //Update to use LibRarisma.CSVToList
            GameData.Terrain[0].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Terrain//Regular//"));
            GameData.Terrain[1].AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "//Data//Resources//Terrain//Forests"));
            GameData.Terrain[2].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Terrain//Caves//"));
            GameData.Terrain[3].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Terrain//Mountains//"));

            Enemy[0].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//"));
            Enemy[1].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Enemy//"));
            Enemy[2].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//"));

            Resources[0].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Items//Random//"));
            Resources[1].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Items//Shards//"));
            Resources[2].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Items//Crystals//"));
            Resources[3].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Items//Agriculture//Farm//"));
            Resources[4].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Items//Agriculture//Fruit//"));
            Resources[5].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Items//Agriculture//Waterplants//"));
            Resources[6].AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "//Data//Resources//Items//Agriculture//Normal Trees"));
            Resources[7].AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "//Data//Resources//Items//Agriculture//Rare Trees"));
            Resources[8].AddRange(LibRarisma.ListFilesInDir(FileSystem.AppDataDirectory + "//Data//Resources//Items//Metals//"));

            PassiveCreatures.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Passive//Land"));
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

        public static void PlayerDataReset()
        {
            PlayerDataCoordinates[0] = 0;
            PlayerDataCoordinates[1] = 0;
            GameData.PlayerInventory.Clear();
            GameData.PlayerInventoryAmmount.Clear();
            Debugint.Add(0);
            Debugint.Add(0);
        }
    }
}