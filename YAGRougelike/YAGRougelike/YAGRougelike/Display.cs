using System;
using System.Collections.Generic;

namespace YAGRougelike
{
    public class Display
    {
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
                int Decider = rnd.Next(0, 10);
                if (Decider >= 0) { Resource0 = Generate.ResouceGenerate(TerrainPath); Resource0[3] = "\nThere" + Resource0[1] + Resource0[2]; } // 90% chance for 1 resource
                if (Decider >= 5) { Resource1 = Generate.ResouceGenerate(TerrainPath); Resource1[3] = "\nThere" + Resource1[1] + Resource1[2]; } // 50% chance for 2 Resource
                if (Decider >= 7) { Resource2 = Generate.ResouceGenerate(TerrainPath); Resource2[3] = "\nThere" + Resource2[1] + Resource2[2]; } // 30% chance for 3 Resource
                if (Decider >= 9) { Resource3 = Generate.ResouceGenerate(TerrainPath); Resource3[3] = "\nThere" + Resource3[1] + Resource3[2]; } // 10% chance for 4 resouces
            }

            output.Add(Resource0);
            output.Add(Resource1);
            output.Add(Resource2);
            output.Add(Resource3);
            Resource.ReloadResources();

            return output;
        }
    }
}