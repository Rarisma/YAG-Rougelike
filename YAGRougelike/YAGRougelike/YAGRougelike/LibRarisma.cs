//The maze of life is a banger of a tune
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using System.Linq;
using System.IO.Compression;

//Considering making my own library so for now here it is
//If this randomly disappears its because I decided it was a bad idea.
//Eventually this will be on github if I like it enough
//This adds things into C# I wish it would add

//LibRarisma  Cleanup list
//DownloadFile - Remove References to FileSystem.AppData, Allow extraction location to be set

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

        public static string DownloadFile(string URL, bool Extract)
        {
            try // This will download the resources.zip
            {
                using (var client = new System.Net.WebClient()) { client.DownloadFile(URL, FileSystem.AppDataDirectory + "//Resouces.zip"); }
            }
            catch { return "Error Code 1\nFailed to download file?\nAre you connected to the internet?"; } //This should only happen if the user cannot access github

            if (Extract == true)
            {
                try { ZipFile.ExtractToDirectory(FileSystem.AppDataDirectory + "//Resouces.zip", FileSystem.AppDataDirectory + "//Data//Resources//"); }
                catch { return "Error Code 2\nFailed to extract resources?\nIs the zip corrupted\n\nYou should check your connection, try again and if this persists contact the developers.\n\nYou are likely to crash if you press continue."; }
            }

            return "Success!";
        }

        ///<summary>
        ///This is used for the new resource system which uses a directory to store individual resources rarther than a large text file
        ///</summary>
        public static List<string> ListFilesInDir(string PathToDirectoryToLoadFrom)
        {
            DirectoryInfo d = new DirectoryInfo(FileSystem.AppDataDirectory + PathToDirectoryToLoadFrom);
            FileInfo[] Files = d.GetFiles();
            List<string> Resources = new List<string>();
            foreach (FileInfo file in Files) { Resources.Add(Convert.ToString(file.Name)); }
            return Resources;
        }
    }
}