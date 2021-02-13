using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//CodeClub

namespace YAGRougelike
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameView : ContentPage
    {
        public GameView()
        {
            Resource.ClearResources();
            Resource.ReloadResources();
            InitializeComponent();
            WorldGen();
        }

        /* To do
            - Make enemys have a 20ish % chance of disabling movement buttons
            - Work on coords
            - Add weather
            - Add battle button
            - Possibly try and make a mac vm and try building for iOS if it works
         */

        public void WorldGen()
        {
            Random rnd = new Random();
            int Decider = rnd.Next(0, 10);

            TXTLocation.Text = "You are " + Generate.Terrain()[2]; //Generates Terrain

            //Resource generation below

            List<string[]> Resources = Display.Resources();
            TXTResource0.Text = Resources[0][2];
            TXTResource1.Text = Resources[1][2];
            TXTResource2.Text = Resources[2][2];
            TXTResource3.Text = Resources[3][2];

            string[] Creature = CreatureDisplay();
            TXTEnemy.Text =  Creature[1]; //Displays Generated creature
        }

        public static string[] CreatureDisplay()
        {
            Random rnd = new Random();
            int Decider = rnd.Next(0, 9);

            string[] output = { "", "", "" };

            if (Decider < 2) { output[0] = ""; }                                // 30% for nothing
            else if (Decider > 2 && Decider < 8) { output = Generate.HostileGenerate(); } // 40% Chance for an enemy
            else if (Decider >= 8) { output = Generate.CreatureGenerate(); }              // 20% Chance for an animal

            if (Decider > 2) { output[1] = "There is a " + output[1]; } //Adds text before is a creature is generated
            if (Decider > 2 && Decider < 8 && rnd.Next(1,5) == 5) { }
            return output;
        }

        private void Move(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string ButtonText = Convert.ToString(button.Text.Replace("Move ", ""));
            WorldGen();
        }
    }
}