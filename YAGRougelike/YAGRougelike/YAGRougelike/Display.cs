using System;
using System.Collections.Generic;

namespace YAGRougelike
{
    public class Display
    {
        public static List<string[]> Resources()
        {
            List<string[]> output = new List<string[]>();
            string[] Resource0 = { "", "", "" };
            string[] Resource1 = { "", "", "" };
            string[] Resource2 = { "", "", "" };
            string[] Resource3 = { "", "", "" };

            Random rnd = new Random();
            int Decider = rnd.Next(0, 10);
            if (Decider > 0) { Resource0 = Generate.ResouceGenerate(); } // 90% chance for 1 resource
            if (Decider >= 5) { Resource1 = Generate.ResouceGenerate(); } // 50% chance for 2 Resource
            if (Decider >= 7) { Resource2 = Generate.ResouceGenerate(); } // 30% chance for 3 Resource
            if (Decider >= 9) { Resource3 = Generate.ResouceGenerate(); } // 10% chance for 4 resouces

            output.Add(Resource0);
            output.Add(Resource1);
            output.Add(Resource2);
            output.Add(Resource3);

            return output;
        }
    }
}