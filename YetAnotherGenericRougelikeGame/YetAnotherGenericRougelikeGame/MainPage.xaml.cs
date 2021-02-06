using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace YetAnotherGenericRougelikeGame
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async private void Initalise(object sender, EventArgs e) //Checks for critical data
        {
            PlayButton.Text = "Checking data folder...";
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Data"))
            {
                PlayButton.Text = "Found" + FileSystem.AppDataDirectory + "//Data";
                ResouceUpdater();
            }
            else
            {
                await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
                PlayButton.Text = FileSystem.AppDataDirectory + "//Data Doesn't exist!";
                Directory.CreateDirectory(FileSystem.AppDataDirectory + "//Data");
                await Task.Delay(100);
                PlayButton.Text = "Made Data directory";
                await Task.Delay(100);
                ResouceUpdater();
            }
        }

        async void ResouceUpdater()
        {
            PlayButton.Text = "Attempting to update...";
            Thread.Sleep(1);
            try //Above downloads the Resouces.Zip from GitHub
            {
                using (var client = new WebClient()) { client.DownloadFile("https://raw.githubusercontent.com/Rarisma/Yet-Another-Generic-Rougelike-Game/main/Resources/Resources.zip", FileSystem.AppDataDirectory + "//Resouces.zip"); }
                try { Directory.Delete(FileSystem.AppDataDirectory + "//Data//Resources//", true); } //Deletes Resources folder and any subfolders
                catch { Thread.Sleep(0); } // Does nothing, just prevents crash

                ZipFile.ExtractToDirectory(FileSystem.AppDataDirectory + "//Resouces.zip", FileSystem.AppDataDirectory + "//Data//Resources//");
                PlayButton.Text = "Update complete!";
                await Task.Delay(3000);
                PlayButton.Text = "Play";
            }
            catch //If somthing prevents the user from downloading resources.zip then it checks for resources 
            {
                if (Directory.Exists(FileSystem.AppDataDirectory + "//Data//Resources//")) // Checks if user already has resources folder
                { await DisplayAlert("Resources Update failed!", "Couldn't connect to GitHub to download resources, you can continue however you might encounter errors/crashes.", "Ok"); } //allows user to continue if they already have /resources/
                else { await DisplayAlert("Failed to find and update resources", "Couldn't connect to GitHub to download resources.\nCheck your internet connection and try again.", "Ok"); } //Tells user an error has occured
            }
        }
    }
}
