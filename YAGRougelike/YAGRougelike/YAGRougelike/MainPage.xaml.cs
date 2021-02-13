using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace YAGRougelike
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        async void Play(object sender, EventArgs e)
        {
            try// These seem to fail on UWP (Win10/Xbox)
            {
                await Permissions.CheckStatusAsync<Permissions.StorageRead>();
                await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            }
            catch { await Task.Delay(0); }

            PlayButton.Text = "Attempting to update...";
            await Task.Delay(1); //Delays task to make sure the play button updates
            using (var client = new WebClient()) { client.DownloadFile("https://github.com/Rarisma/YAG-Rougelike/raw/main/Resources/Resources.zip", FileSystem.AppDataDirectory + "//Resouces.zip"); }

            try { Directory.Delete(FileSystem.AppDataDirectory + "//Data//Resources//", true); } //Deletes Resources folder and any subfolders
            catch { await Task.Delay(1); } // Does nothing, just prevents crash

            try { ZipFile.ExtractToDirectory(FileSystem.AppDataDirectory + "//Resouces.zip", FileSystem.AppDataDirectory + "//Data//Resources//"); }
            catch { await Task.Delay(1); }

            await Navigation.PushModalAsync(new Overworld());
            PlayButton.Text = "Update complete!";
            await Task.Delay(1000);
            PlayButton.Text = "Play";

        }
    }
}
