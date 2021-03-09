using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Xamarin.Essentials;
using YAGRougelike.WorldGeneration.Terrain;

namespace YAGRougelike
{ //Imagine having two functioning joycons, could not be me
    public class Generate
    {
        public static string[] Terrain()
        {   //Check GameData.cs for terrain types
            //The following lays out the ground work (Declaring variables ect)
            string[] output = { "", "", "", "", "" };   //  0 - TerrainType, 1 - TerrainID, 2 - Prefix, 3 - Display ready,  4 - Path
            int[] TerrainHelper = { 50, 70, 75, 80 };      // Helps terrain generation (Max numbers)
            string[] PrefixLocations = { "//Data//Resources//Terrain//Regular//", "Forrest", "//Data//Resources//Terrain//Caves//", "//Data//Resources//Terrain//Mountains//" };

            //The following decides the type and terrain
            bool ForLoopFixer = false;
            int test = LibRarisma.RandomNumber(0, TerrainHelper.Last());
            for (int i = 0; ForLoopFixer == true; i++)
            {
                if (TerrainHelper[i] <= test)
                {
                    output[0] = Convert.ToString(i);
                    ForLoopFixer = true;
                }
                else
                {
                    i++;
                }
            }

            output[1] = Convert.ToString(LibRarisma.RandomNumber(0, GameData.Terrain[Convert.ToInt32(output[0])].Count()));

            //This decides the prefix
            if (output[0] == "1") //For forests
            {
                output[0] = "1"; //Forest terrain type
                output[1] = GameData.Resources[6][LibRarisma.RandomNumber(0, GameData.Resources[6].Count())]; //This gets the type of wood as Terrain only stores the prefixes
                output[2] = GameData.Terrain[1][LibRarisma.RandomNumber(0, GameData.Terrain[1].Count())];
                output[3] = "You are " + output[2] + " " + output[1] + " forest.";
                output[4] = "forests"; //This makes ResourceGenerate work correctly
                GameData.Debugint[1]++;
            }
            else
            {
                List<String> Prefixes = new List<String>();

                Prefixes.AddRange(LibRarisma.CSVToListFromFile(Convert.ToString(FileSystem.AppDataDirectory + PrefixLocations[Convert.ToInt32(output[0])] + GameData.Terrain[Convert.ToInt32(output[0])][Convert.ToInt32(output[1])]), 1));
                output[2] = Prefixes[LibRarisma.RandomNumber(0, Prefixes.Count)];

                //This assembles everything for display
                output[3] = "You are " + output[2] + " " + GameData.Terrain[Convert.ToInt32(output[0])][Convert.ToInt32(output[1])];
                output[4] = FileSystem.AppDataDirectory + PrefixLocations[Convert.ToInt32(output[0])] + GameData.Terrain[Convert.ToInt32(output[0])][Convert.ToInt32(output[1])];
                GameData.Debugint[0]++;
            }
            output[3] += "\nNormal:" + GameData.Debugint[0] + "\nForest:" + GameData.Debugint[1];
            return output;
        }

        private static string[] ResouceGenerate(string PathToTerrain)
        {
            /*Writing this gave me a PHD in for loops
            See GameData.cs for resource IDs

            Special resource IDs:
            Name               ID
            Load nothing      -2
            Custom Resource   -1

            ID's are used to let the code understand what list is being picked from

            The code below basically gets a random number and then
            goes to the corresponding if, then it picks a random item
            in the corresponding list.*/

            string[] output = { "", "", "", "" }; //0 - Ammount   1 - Prefix   2 - Name   3 - Unused here but prevents display() crashes if not
            List<string> TempEnabledResources = new List<string>(); //used to get the output of LibRarisma.CSVToListFromFile
            List<int> EnabledResources = new List<int>();           //This is used to store the converted output of TempEnabledResources
            List<int[]> AllowedResources = new List<int[]>();       //This is used to decide the resource to call
            TempEnabledResources.AddRange(LibRarisma.CSVToListFromFile(PathToTerrain, 3));
            for (int i = 0; i < TempEnabledResources.Count; i++) { EnabledResources.Add(Convert.ToInt32(TempEnabledResources[i])); }

            //Possibly add feature to decide weighting per terrain
            if (EnabledResources.Contains(-1) == true) { AllowedResources.Add(new[] { -1, 25 }); }
            if (EnabledResources.Contains(0) == true) { AllowedResources.Add(new[] { 0, 10 }); }
            if (EnabledResources.Contains(1) == true) { AllowedResources.Add(new[] { 1, 25 }); }
            if (EnabledResources.Contains(2) == true) { AllowedResources.Add(new[] { 2, 5 }); }
            if (EnabledResources.Contains(3) == true) { AllowedResources.Add(new[] { 3, 4 }); }
            if (EnabledResources.Contains(4) == true) { AllowedResources.Add(new[] { 4, 5 }); }
            if (EnabledResources.Contains(5) == true) { AllowedResources.Add(new[] { 5, 1 }); }
            if (EnabledResources.Contains(6) == true) { AllowedResources.Add(new[] { 6, 5 }); }

            int TotalSum = 0;
            for (int i = 0; i < EnabledResources.Count(); i++) { TotalSum += AllowedResources[i][1]; } //This loop adds the second number in each array from the list EnabledResources

            //This part actually decides the resource type
            Random rnd = new Random();
            int ResourceChooser = rnd.Next(0, TotalSum); //This gets a random number between the given weights the for loop below this will decide it
            int SelectedID = 1;
            int ResourceCounter = 0;
            for (int i = 0; ResourceCounter < ResourceChooser; i++) { ResourceCounter += AllowedResources[i][1]; SelectedID = AllowedResources[i][0]; }

            //This part takes the decided type and gets a random item from said type

            if (SelectedID == -1) // -1 is strange as its defined by one of the last lines in a terrain file but differing terrains have different properties so this scans the file and finds it
            {
                if (GameData.DisableCustomResources == false)
                {
                    List<string> TerrainFile = new List<string>();
                    TerrainFile.AddRange(File.ReadAllLines(PathToTerrain));
                    output[2] = TerrainFile[TerrainFile.Count() - 1];
                    GameData.DisableCustomResources = true;
                }
            }
            else
            {
                int ResID = rnd.Next(0, GameData.Resources[SelectedID].Count);
                output[2] = GameData.Resources[SelectedID][ResID];
                GameData.Resources[SelectedID].RemoveAt(ResID);
            }

            //This handles the ammount of resources generated and the prefix for it
            string[] TempResourceAmmounts = { " are a few ", " are some ", " are lots of ", " are tons of " };
            output[0] = Convert.ToString(rnd.Next(0, 10));
            output[1] = TempResourceAmmounts[Convert.ToInt32(output[0]) / 4];

            return output;
        }

        public static List<object> HostileGenerate()
        {
            /* Enemy Generation guide
            Suffix - 10% chance
            Prefix - 20% Chance

            The value of Suffix and Prefix decider are added to output[2]
            this will be used in battle to enhance an enemys stats.

            TODO - Re add suffixes
             */

            //This loads the base enemy data into the list
            List<object> Output = new List<object>
                    {
                        GameData.Enemy[1][LibRarisma.RandomNumber(0, GameData.Enemy[1].Count())] //Gets a random enemy
                    }; // this stores names and numbers
            Output.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Enemy//" + Output[0]));

            //This cleans the list
            Output.RemoveAt(9);
            Output.RemoveAt(7);
            Output.RemoveAt(5);
            Output.RemoveAt(3);
            Output.RemoveAt(1);

            if (LibRarisma.RandomNumber(1, 3) == 2) //50% Chance of loading a prefix
            {//could be made into a for loop at some point and possibly put into a function
                string Prefix = GameData.Enemy[0][LibRarisma.RandomNumber(0, GameData.Enemy[0].Count())]; //This used for loading
                Output[0] = Prefix + " " + Output[0];
                Output[1] = Convert.ToInt32(Output[1]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(1).Take(1).First());
                Output[2] = Convert.ToInt32(Output[2]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(3).Take(1).First());
                Output[3] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(5).Take(1).First());
                Output[4] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(7).Take(1).First());
                Output[5] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(9).Take(1).First());
            }

            if (LibRarisma.RandomNumber(0, 5) == 3) //10% Chance of loading a prefix
            {//could be made into a for loop at some point and possibly put into a function
                string Suffix = GameData.Enemy[2][LibRarisma.RandomNumber(0, GameData.Enemy[2].Count())]; //This used for loading
                Output[0] = Output[0] + " " + Suffix;
                Output[1] = Convert.ToInt32(Output[1]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(1).Take(1).First());
                Output[2] = Convert.ToInt32(Output[2]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(3).Take(1).First());
                Output[3] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(5).Take(1).First());
                Output[4] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(7).Take(1).First());
                Output[5] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(9).Take(1).First());
            }
            return Output;
        }

        public static List<string[]> Resources(string TerrainPath, string ForestType = null)
        {
            List<string[]> output = new List<string[]>();
            string[] Resource0 = { "", "", "", "" };
            string[] Resource1 = { "", "", "", "" };
            string[] Resource2 = { "", "", "", "" };
            string[] Resource3 = { "", "", "", "" };

            if (TerrainPath.ToLower().Contains("forests") || TerrainPath.Contains("forrests")) //Forrests don't follow to normal terrain gen rules and will simply generate 4 of the same tree
            {
                Resource0[3] = "\nThere is a ton of " + ForestType + " trees";
            }
            else
            {
                Random rnd = new Random();
                int Decider = rnd.Next(10, 10);
                if (Decider >= 0) { Resource0 = Generate.ResouceGenerate(TerrainPath); Resource0[3] = "\nThere" + Resource0[1] + Resource0[2]; } // 90% chance for 1 resource
                if (Decider >= 5) { Resource1 = Generate.ResouceGenerate(TerrainPath); Resource1[3] = "\nThere" + Resource1[1] + Resource1[2]; } // 50% chance for 2 Resource
                if (Decider >= 7) { Resource2 = Generate.ResouceGenerate(TerrainPath); Resource2[3] = "\nThere" + Resource2[1] + Resource2[2]; } // 30% chance for 3 Resource
                if (Decider >= 9) { Resource3 = Generate.ResouceGenerate(TerrainPath); Resource3[3] = "\nThere" + Resource3[1] + Resource3[2]; } // 10% chance for 4 resouces
            }

            output.Add(Resource0);
            output.Add(Resource1);
            output.Add(Resource2);
            output.Add(Resource3);
            GameData.ReloadResources();

            return output;
        }
    }
}