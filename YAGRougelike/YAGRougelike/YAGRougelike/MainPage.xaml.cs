using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace YAGRougelike //Get F.lux!
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Play(object sender, EventArgs e)
        {
            await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            await Permissions.CheckStatusAsync<Permissions.StorageWrite>();
            PlayButton.Text = "Checking for update"; //Makes sure the user doesn't think the app is frozen
            await Task.Delay(1000); //Delays to allow the UI button to update

            string UpdateStatus;
            if (GameData.AreResourcesUpToDate() == false) //this attempts to update and gets the result
            {
                try { Directory.Delete(FileSystem.AppDataDirectory + "//Data//Resources//", true); } //Deletes Resources folder and any subfolders
                catch { } // This will fail if resources has never been downloaded before so it does nothing
                UpdateStatus = LibRarisma.DownloadFile("https://github.com/Rarisma/YAG-Rougelike/raw/main/Resources/Resources.zip", true);
                PlayButton.Text = "Updating..."; //makes sure the user doesn't think the app is frozen
                await Task.Delay(1000);
            }
            else { UpdateStatus = "Success!"; }

            if (UpdateStatus != "Success!") { await DisplayAlert("Error", "The following error occured:\n" + UpdateStatus + "\n\nYou may be able to continue however you might encounter crashes.\nIf this is your first time running the app then you will crash by pressing continue.", "Continue"); }
            await Navigation.PushModalAsync(new Overworld()); //Either way the use will get sent here
        }

        async private void Settings(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Creator());
        }
    }
}