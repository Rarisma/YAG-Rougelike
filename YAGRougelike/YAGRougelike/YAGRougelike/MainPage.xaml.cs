using System;
using System.IO;
using System.IO.Compression;
using System.Net;
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

            string UpdateStatus = "Somthing has gone seriously wrong if you are seeing this";
            if (Resource.AreResourcesUpToDate() == false) //this attempts to update and gets the result
            {
                UpdateStatus = Resource.ResourceUpdate();
                PlayButton.Text = "Updating..."; //Makes sure the user doesn't think the app is frozen
                await Task.Delay(1000);
            }
            else { UpdateStatus = "Success!"; }

            if (UpdateStatus != "Success!") { await DisplayAlert("Error", "The following error occured:\n" + UpdateStatus + "\n\nYou may be able to continue however you might encounter crashes.\nIf this is your first time running the app then you will crash by pressing continue.", "Continue"); }
            await Navigation.PushModalAsync(new Overworld()); //Either way the use will get sent here
        }
    }
}