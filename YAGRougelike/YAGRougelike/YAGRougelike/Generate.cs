using System;
using System.Linq;

namespace YAGRougelike
{
    public class Generate
    {
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
            string[] output = { "", "", "" };

            if (TerrainDecider <= 50) //Normal terrain
            {
                output[0] = "0";
                output[1] = Convert.ToString(rnd.Next(0, Resource.RegularLocations.Count()));
                output[2] = Resource.RegularLocations[Convert.ToInt32(output[1])];
            }
            else if (TerrainDecider >= 51 && TerrainDecider <= 80)
            {
                output[0] = "1";
                output[1] = Convert.ToString(rnd.Next(0, Resource.ForestLocations.Count()));
                output[2] = Resource.ForestLocations[Convert.ToInt32(output[1])];
            }
            else if (TerrainDecider >= 81 && TerrainDecider <= 90)
            {
                output[0] = "2";
                output[1] = Convert.ToString(rnd.Next(0, Resource.CaveLocations.Count()));
                output[2] = Resource.CaveLocations[Convert.ToInt32(output[1])];
            }
            else if (TerrainDecider >= 91 && TerrainDecider <= 96)
            {
                output[0] = "3";
                output[1] = Convert.ToString(rnd.Next(0, Resource.MountainLocations.Count()));
                output[2] = Resource.MountainLocations[Convert.ToInt32(output[1])];
            }
            else if (TerrainDecider >= 96 && TerrainDecider <= 100) //should be changed for unique settlements
            {
                output[0] = "2";
                output[1] = Convert.ToString(rnd.Next(0, Resource.CaveLocations.Count()));
                output[2] = Resource.CaveLocations[Convert.ToInt32(output[1])];
            }
            return output;
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

        public static string[] CreatureGenerate()
        {
            Random rnd = new Random();
            string[] output = { "", "", "-1", "true" };
            output[0] = "0";
            output[1] = Resource.PassiveCreatures[rnd.Next(0, Resource.PassiveCreatures.Count())].ToLower();
            return output;
        }

        public static string[] HostileGenerate()
        {
            /* Enemy Generation guide
            Suffix - 33% chance
            Prefix - 20% Chance

            The value of Suffix and Prefix decider are added to output[2]
            this will be used in battle to enhance an enemys stats.

            TODO - Re add suffixes
             */
            Random rnd = new Random();
            string[] output = { "", "enemytext", "1", "" };
            output[0] = "1";

            output[1] = output[1].ToLower();
            return output;
        }
    }
}