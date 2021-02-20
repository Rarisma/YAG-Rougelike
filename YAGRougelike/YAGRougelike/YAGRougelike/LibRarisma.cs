//The maze of life is a banger of a tune
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Xamarin.Essentials;
using System.Linq;

//Considering making my own library so for now here it is
//If this randomly disappears its because I decided it was a bad idea.
//Eventually this will be on github if I like it enough
//This adds things into C# I wish it would add
namespace YAGRougelike
{
    internal class LibRarisma
    {
        /// <summary>
        ///  This turns a string such as 0,24,432,565,5644,
        ///  into a list,no matter what this will return a string
        /// </summary>
        public static List<string> CSVToListFromFile(string Pathtofile, int LineToReadfrom)
        {
            string InputList = File.ReadLines(Pathtofile).Skip(LineToReadfrom).Take(1).First();
            List<string> output = new List<string>();
            string temp = "";
            for (int i = 0; i < InputList.Length; i++)
            {
                if (InputList[i] == System.Convert.ToChar(","))
                {
                    output.Add(temp);
                    temp = "";
                }
                else
                {
                    temp += InputList[i];
                }
            }
            return output;
        }
    }
}