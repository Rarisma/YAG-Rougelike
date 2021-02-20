using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Xamarin.Essentials;

namespace YAGRougelike
{
    public class Generate
    {
        public static string[] Terrain()
        {
            /*Heres how the terrain gen works:
             Random Number      Name            ID
             00-20              Normal          0
             21-28              Forrest         1
             29-32              Cave            2
             33-35              Mountain        3
             ??-???             Unique           (Not implemented yet)
             ID's are used to let the code understand what list is being picked from

             The code below basically gets a random number and then
             goes to the corresponding if, then it picks a random item
             in the corresponding list.*/

            Random rnd = new Random();
            int TerrainDecider = rnd.Next(0, 35);
            string[] fixer = { "", "", "" };
            List<String> Prefixes = new List<String>();
            if (TerrainDecider <= 20)
            {
                string[] output = { "Regular", Convert.ToString(Resource.RegularLocations[rnd.Next(0, Resource.RegularLocations.Count)]), "" };
                Prefixes.AddRange(Resource.TerrainPrefixLoader("//Data//Resources//Terrain//Regular//" + output[1]));
                output[2] = " " + Prefixes[rnd.Next(0, Prefixes.Count())];
                return output;
            }
            else if (TerrainDecider <= 28 && TerrainDecider > 20)
            {
                string ForrestWoodtype = Resource.WoodTypes[rnd.Next(0, Resource.WoodTypes.Count())];
                string[] output = { "Forests", Convert.ToString(Resource.ForrestPrefixes[rnd.Next(0, Resource.ForrestPrefixes.Count())] + " " + ForrestWoodtype + " forrest."), "" };
                return output;
            }
            else if (TerrainDecider <= 32 && TerrainDecider > 28)
            {
                string[] output = { "Caves", Convert.ToString(Resource.CaveLocations[rnd.Next(0, Resource.CaveLocations.Count)]), "" };
                Prefixes.AddRange(Resource.TerrainPrefixLoader("//Data//Resources//Terrain//Caves//" + output[1]));
                output[2] = " " + Prefixes[rnd.Next(0, Prefixes.Count() - 1)];
                return output;
            }
            else if (TerrainDecider <= 35 && TerrainDecider > 32)
            {
                string[] output = { "Mountains", Convert.ToString(Resource.MountainLocations[rnd.Next(0, Resource.MountainLocations.Count)]), "" };
                Prefixes.AddRange(Resource.TerrainPrefixLoader("//Data//Resources//Terrain//Mountains//" + output[1]));
                output[2] = " " + Prefixes[rnd.Next(0, Prefixes.Count() - 1)];
                return output;
            }

            return fixer; //shouldn't be run but visual studio keeps annoying me
        }

        public static string[] ResouceGenerate(string PathToTerrain)
        {
            //Writing this gave me a PHD in for loops
            /* Heres how the resource gen works
            Random Value   Name               ID

            Null      -    Load nothing      -2
            Null      -    Custom Resource   -1
            00-15     -    Bush               0
            16-30     -    Floor plants       1
            31-40     -    Waterplants        2
            41-60     -    Fruit Trees        3
            61-96     -    Regular Trees      4
            96-100    -    Rare               5
            96-100    -    Metal              6

            ID's are used to let the code understand what list is being picked from

            The code below basically gets a random number and then
            goes to the corresponding if, then it picks a random item
            in the corresponding list.*/

            string[] output = { "", "", "" };
            List<string> TempEnabledResources = new List<string>(); //used to get the output of LibRarisma.CSVToListFromFile
            List<int> EnabledResources = new List<int>();           //This is used to store the converted output of TempEnabledResources
            List<int[]> AllowedResources = new List<int[]>();       //This is used to decide the resource to call
            TempEnabledResources.AddRange(LibRarisma.CSVToListFromFile(FileSystem.AppDataDirectory + "//" + PathToTerrain, 3));
            for (int i = 0; i < TempEnabledResources.Count; i++) { EnabledResources.Add(Convert.ToInt32(TempEnabledResources[i])); }

            //Possibly add feature to decide weighting per terrain
            if (EnabledResources.Contains(-1) == true) { AllowedResources.Add(new[] { -1, 25 }); }
            if (EnabledResources.Contains(0) == true) { AllowedResources.Add(new[] { 0, 5 }); }
            if (EnabledResources.Contains(1) == true) { AllowedResources.Add(new[] { 1, 5 }); }
            if (EnabledResources.Contains(2) == true) { AllowedResources.Add(new[] { 2, 10 }); }
            if (EnabledResources.Contains(3) == true) { AllowedResources.Add(new[] { 3, 5 }); }
            if (EnabledResources.Contains(4) == true) { AllowedResources.Add(new[] { 4, 12 }); }
            if (EnabledResources.Contains(5) == true) { AllowedResources.Add(new[] { 5, 1 }); }
            if (EnabledResources.Contains(6) == true) { AllowedResources.Add(new[] { 6, 15 }); }

            int TotalSum = 0;
            for (int i = 0; i < EnabledResources.Count(); i++) { TotalSum += AllowedResources[i][1]; } //This loop adds the second number in each array from the list EnabledResources

            //This part actually decides the resource type
            Random rnd = new Random();
            int ResourceChooser = rnd.Next(0, TotalSum); //This gets a random number between the given weights the for loop below this will decide it
            int SelectedID = 1911;
            int ResourceCounter = 0;
            for (int i = 0; ResourceCounter < ResourceChooser; i++) { ResourceCounter += AllowedResources[i][1]; SelectedID = AllowedResources[i][0]; }

            //This part takes the decided type and gets a random item from said type
            if (SelectedID == 0) { output[2] = Resource.BushResources[rnd.Next(0, Resource.BushResources.Count)]; }
            if (SelectedID == 1) { output[2] = Resource.FloorPlantResources[rnd.Next(0, Resource.FloorPlantResources.Count)]; }
            if (SelectedID == 2) { output[2] = Resource.WaterPlantResources[rnd.Next(0, Resource.WaterPlantResources.Count)]; }
            if (SelectedID == 3) { output[2] = "ripe " + Resource.FruitResources[rnd.Next(0, Resource.FruitResources.Count)] + " tree"; }
            if (SelectedID == 4) { output[2] = Resource.WoodTypes[rnd.Next(0, Resource.WoodTypes.Count)] + " tree"; }
            if (SelectedID == 5) { output[2] = "rare " + Resource.RareTreeResources[rnd.Next(0, Resource.RareTreeResources.Count)] + " tree here"; }
            if (SelectedID == 6) { output[2] = "cluster of " + Resource.MetalResources[rnd.Next(0, Resource.MetalResources.Count)] + " ore"; }

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
            Random rnd = new Random();
            List<object> Output = new List<object>(); // this stores names and numbers
            Output.Add(Resource.Enemies[rnd.Next(0, Resource.Enemies.Count())]); //Gets a random enemy
            Output.AddRange(File.ReadAllLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Enemy//" + Output[0]));

            //This cleans the list
            Output.RemoveAt(9);
            Output.RemoveAt(7);
            Output.RemoveAt(5);
            Output.RemoveAt(3);
            Output.RemoveAt(1);

            if (rnd.Next(1, 3) == 2) //50% Chance of loading a prefix
            {//could be made into a for loop at some point and possibly put into a function
                string Prefix = Resource.EnemyPrefix[rnd.Next(0, Resource.EnemyPrefix.Count())]; //This used for loading
                Output[0] = Prefix + " " + Output[0];
                Output[1] = Convert.ToInt32(Output[1]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(1).Take(1).First());
                Output[2] = Convert.ToInt32(Output[2]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(3).Take(1).First());
                Output[3] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(5).Take(1).First());
                Output[4] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(7).Take(1).First());
                Output[5] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(9).Take(1).First());
            }

            if (rnd.Next(0, 5) == 3) //10% Chance of loading a prefix
            {//could be made into a for loop at some point and possibly put into a function
                string Suffix = Resource.EnemySuffix[rnd.Next(0, Resource.EnemySuffix.Count())]; //This used for loading
                Output[0] = Output[0] + " " + Suffix;
                Output[1] = Convert.ToInt32(Output[1]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(1).Take(1).First());
                Output[2] = Convert.ToInt32(Output[2]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(3).Take(1).First());
                Output[3] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(5).Take(1).First());
                Output[4] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(7).Take(1).First());
                Output[5] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(9).Take(1).First());
            }

            return Output;
        }
    }
}