using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;
using System.Net;

namespace GenericRoguelike
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Initalise(object sender, EventArgs e) //Checks for critical data
        {
            PlayButton.Text = "Checking data folder...";
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Data"))
            {
                PlayButton.Text = "Found" + FileSystem.AppDataDirectory + "//Data";
            }
            else
            {
                Permissions.CheckStatusAsync<Permissions.StorageRead>();
                Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                PlayButton.Text = FileSystem.AppDataDirectory + "//Data Doesn't exist!";
                Directory.CreateDirectory(FileSystem.AppDataDirectory + "//Data");
                PlayButton.Text = "Made Data directory";
            }

            using (var client = new WebClient())
            {
                client.DownloadFile("https://github.com/Rarisma/Yet-Another-Generic-Rougelike-Game/tree/main/Resources/", FileSystem.AppDataDirectory + "//Rarisma//");
            }
            PlayButton.Text = FileSystem.AppDataDirectory + "//Rarisma//";


        }
    }
}
