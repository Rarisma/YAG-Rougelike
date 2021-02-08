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

        async void Play(object sender, EventArgs e)
        {
            string UpdaterStatus = "Initalised";
            try// These seem to fail on UWP (Win10/Xbox)
            {
                await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            }
            catch {Task.Delay(0);}

            PlayButton.Text = "Attempting to update...";
            await Task.Delay(1); //Delays task to make sure the play button updates
            UpdaterStatus = "Requested Permissions";
            using (var client = new WebClient()) { client.DownloadFile("https://github.com/Rarisma/YAG-Rougelike/raw/main/Resources/Resources.zip", FileSystem.AppDataDirectory + "//Resouces.zip"); }

            UpdaterStatus = "Attempting to download file";
            try { Directory.Delete(FileSystem.AppDataDirectory + "//Data//Resources//", true); } //Deletes Resources folder and any subfolders
            catch { UpdaterStatus = "Failed to delete //Resources// (Probably doesn't exist)"; } // Does nothing, just prevents crash

            try { ZipFile.ExtractToDirectory(FileSystem.AppDataDirectory + "//Resouces.zip", FileSystem.AppDataDirectory + "//Data//Resources//"); }
            catch { UpdaterStatus = "Zip Extraction Failed."; }

            await Navigation.PushModalAsync(new GameView());
            PlayButton.Text = "Update complete!";
            await Task.Delay(1);
            PlayButton.Text = "Play"; 

        }
    }
}
