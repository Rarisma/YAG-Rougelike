using System;
using System.Collections.Generic;

namespace YAGRougelike
{
    public class Display
    {
        public static List<string[]> Resources(string TerrainPath)
        {
            List<string[]> output = new List<string[]>();
            string[] Resource0 = { "", "", "" };
            string[] Resource1 = { "", "", "" };
            string[] Resource2 = { "", "", "" };
            string[] Resource3 = { "", "", "" };

            if (TerrainPath.Contains("Forests") || TerrainPath.Contains("Forrests")) //Forrests don't follow to normal terrain gen rules and will simply generate 4 of the same tree
            {
                output.Add(Resource0);
                output.Add(Resource1);
                output.Add(Resource2);
                output.Add(Resource3);

                return output;
            }

            Random rnd = new Random();
            int Decider = rnd.Next(0, 10);
            if (Decider > 0) { Resource0 = Generate.ResouceGenerate(TerrainPath); Resource0[2] = "There is a " + Resource0[2] + "\n"; } // 90% chance for 1 resource
            if (Decider >= 5) { Resource1 = Generate.ResouceGenerate(TerrainPath); Resource1[2] = "There is a " + Resource1[2] + "\n"; } // 50% chance for 2 Resource
            if (Decider >= 7) { Resource2 = Generate.ResouceGenerate(TerrainPath); Resource2[2] = "There is a " + Resource2[2] + "\n"; } // 30% chance for 3 Resource
            if (Decider >= 9) { Resource3 = Generate.ResouceGenerate(TerrainPath); Resource3[2] = "There is a " + Resource3[2] + "\n"; } // 10% chance for 4 resouces

            output.Add(Resource0);
            output.Add(Resource1);
            output.Add(Resource2);
            output.Add(Resource3);

            return output;
        }
    }
}