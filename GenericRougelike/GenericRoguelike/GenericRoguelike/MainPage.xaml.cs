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
using System.IO.Compression;
using System.Threading;

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
                ResouceUpdater();
            }
            else
            {
                Permissions.CheckStatusAsync<Permissions.StorageRead>();
                Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                PlayButton.Text = FileSystem.AppDataDirectory + "//Data Doesn't exist!";
                Directory.CreateDirectory(FileSystem.AppDataDirectory + "//Data");
                PlayButton.Text = "Made Data directory";
                ResouceUpdater();
            }
        }

        async void ResouceUpdater()
        {
            PlayButton.Text = "Attempting to update...";
            Thread.Sleep(1);
            using (var client = new WebClient()) { client.DownloadFile("https://raw.githubusercontent.com/Rarisma/Yet-Another-Generic-Rougelike-Game/main/Resources/Resources.zip", FileSystem.AppDataDirectory + "//Resouces.zip");}
            //Above downloads the Resouces.Zip from GitHub

            try // Tries to delete Resources folder
            {
                Directory.Delete(FileSystem.AppDataDirectory + "//Data//Resources//",true);
            }
            catch { Thread.Sleep(0); } // Does nothing, just prevents crash

            ZipFile.ExtractToDirectory(FileSystem.AppDataDirectory + "//Resouces.zip", FileSystem.AppDataDirectory + "//Data//Resources//");
            PlayButton.Text = "Update complete!";
            await Navigation.PushAsync(new CharacterCreator());
        }
    }
}
