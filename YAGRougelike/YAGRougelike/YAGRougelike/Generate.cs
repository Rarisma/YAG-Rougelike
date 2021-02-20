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
                string[] output = { "0", Convert.ToString(Resource.RegularLocations[rnd.Next(0, Resource.RegularLocations.Count)]), "" };
                Prefixes.AddRange(Resource.TerrainPrefixLoader("//Data//Resources//Terrain//Regular//" + output[1]));
                output[2] = " " + Prefixes[rnd.Next(0, Prefixes.Count())];
                return output;
            }
            else if (TerrainDecider <= 28 && TerrainDecider > 20)
            {
                string[] output = { "1", Convert.ToString(Resource.ForrestPrefixes[rnd.Next(0, Resource.ForrestPrefixes.Count())] + " " + Resource.WoodTypes[rnd.Next(0, Resource.WoodTypes.Count())]) + " forrest.", "" };
                return output;
            }
            else if (TerrainDecider <= 32 && TerrainDecider > 28)
            {
                string[] output = { "2", Convert.ToString(Resource.CaveLocations[rnd.Next(0, Resource.CaveLocations.Count)]), "" };
                Prefixes.AddRange(Resource.TerrainPrefixLoader("//Data//Resources//Terrain//Caves//" + output[1]));
                output[2] = " " + Prefixes[rnd.Next(0, Prefixes.Count() - 1)];
                return output;
            }
            else if (TerrainDecider <= 35 && TerrainDecider > 32)
            {
                string[] output = { "3", Convert.ToString(Resource.MountainLocations[rnd.Next(0, Resource.MountainLocations.Count)]), "" };
                Prefixes.AddRange(Resource.TerrainPrefixLoader("//Data//Resources//Terrain//Mountains//" + output[1]));
                output[2] = " " + Prefixes[rnd.Next(0, Prefixes.Count() - 1)];
                return output;
            }

            return fixer; //shouldn't be run but visual studio keeps annoying me
        }

        public static string[] ResouceGenerate() //This generates a single resource ResourceDisplay() creates a full set of resources.
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
            string[] output = { "", "", "" };

            if (ResourceDecider <= 15)
            {
                output[0] = "0";
                output[1] = Convert.ToString(rnd.Next(0, Resource.BushResources.Count()));
                output[2] = "There is a " + Resource.BushResources[Convert.ToInt32(output[1])].ToLower() + "\n";
            }
            else if (ResourceDecider >= 16 && ResourceDecider <= 30)
            {
                output[0] = "1";
                output[1] = Convert.ToString(rnd.Next(0, Resource.FloorPlantResources.Count()));
                output[2] = "There is a " + Resource.FloorPlantResources[Convert.ToInt32(output[1])].ToLower() + "\n";
            }
            else if (ResourceDecider >= 31 && ResourceDecider <= 40)
            {
                output[0] = "2";
                output[1] = Convert.ToString(rnd.Next(0, Resource.WaterPlantResources.Count()));
                output[2] = "There is a " + Resource.WaterPlantResources[Convert.ToInt32(output[1])].ToLower() + "\n";
            }
            else if (ResourceDecider >= 41 && ResourceDecider <= 60)
            {
                output[0] = "3";
                output[1] = Convert.ToString(rnd.Next(0, Resource.FruitResources.Count()));
                output[2] = "There is a " + Resource.FruitResources[Convert.ToInt32(output[1])].ToLower() + " tree\n";
            }
            else if (ResourceDecider >= 61 && ResourceDecider <= 96)
            {
                output[0] = "4";
                output[1] = Convert.ToString(rnd.Next(0, Resource.TreeResources.Count()));
                output[2] = "There is a " + Resource.TreeResources[Convert.ToInt32(output[1])].ToLower() + " tree\n";
            }
            else if (ResourceDecider >= 97 && ResourceDecider <= 100)
            {
                output[0] = "4";
                output[1] = Convert.ToString(rnd.Next(0, Resource.TreeResources.Count()));
                output[2] = "There is a " + Resource.TreeResources[Convert.ToInt32(output[1])].ToLower() + " tree\n";
            }
            return output;
        }

        public static string[] NewResourceGenerate(int TerrainID, string TerrainName)
        {
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
            List<string[]> EnabledResources = new List<string[]>();
            List<string> TerrainSettings = new List<string>();
            //You can use the code from TerrainPrefixLoader to load the ResourceIDs from the files
            //Plus if you edit the other Terrains and put the IDs on the same line for all the terrains
            //You will only have to write custom code for Forrests.

            //Aight Imma head out
            //Rarisma (you)
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

            if (rnd.Next(1, 5) == 3) //20% Chance of loading a prefix
            {//could be made into a for loop at some point and possibly put into a function
                string Prefix = Resource.EnemyPrefix[rnd.Next(0, Resource.EnemyPrefix.Count())]; //This used for loading
                Output[0] = "Prefix" + Prefix + Output[0];
                Output[1] = Convert.ToInt32(Output[1]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(1).Take(1).First());
                Output[2] = Convert.ToInt32(Output[2]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(2).Take(1).First());
                Output[3] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(5).Take(1).First());
                Output[4] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(7).Take(1).First());
                Output[5] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Prefix//" + Prefix).Skip(9).Take(1).First());
            }

            if (rnd.Next(0, 9) == 3) //10% Chance of loading a prefix
            {//could be made into a for loop at some point and possibly put into a function
                string Suffix = Resource.EnemySuffix[rnd.Next(0, Resource.EnemySuffix.Count())]; //This used for loading
                Output[0] = Output[0] + "Suffix" + Suffix;
                Output[1] = Convert.ToInt32(Output[1]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(1).Take(1).First());
                Output[2] = Convert.ToInt32(Output[2]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(2).Take(1).First());
                Output[3] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(5).Take(1).First());
                Output[4] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(7).Take(1).First());
                Output[5] = Convert.ToInt32(Output[3]) + Convert.ToInt32(File.ReadLines(FileSystem.AppDataDirectory + "//Data//Resources//Creatures//Hostile//Suffix//" + Suffix).Skip(9).Take(1).First());
            }

            return Output;
        }
    }
}