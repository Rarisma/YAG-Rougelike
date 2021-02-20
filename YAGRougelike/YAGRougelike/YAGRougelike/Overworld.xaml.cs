using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YAGRougelike
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Overworld : ContentPage
    {
        public Overworld()
        {
            InitializeComponent();
            Resource.ClearResources();
            Resource.ReloadResources();
            PlayerData.Reset();
            WorldGen();
        }

        private void Move(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string ButtonText = Convert.ToString(button.Text.Replace("Move ", ""));

            if (ButtonText == "North") { PlayerData.Coordinates[1] += 1; }
            if (ButtonText == "South") { PlayerData.Coordinates[1] -= 1; }
            if (ButtonText == "East") { PlayerData.Coordinates[0] += 1; }
            if (ButtonText == "West") { PlayerData.Coordinates[0] -= 1; }
            WorldGen();
        }

        public void WorldGen()
        {
            string[] Terrain = Generate.Terrain();
            List<string[]> Resources = Display.Resources("//Data//Resources//Terrain//" + Terrain[0] + "//" + Terrain[1]);
            for (int i = 0; i < Resources.Count; i++) { if (Resources[i][2] == "There is a \n") { Resources[i][2] = ""; } } //prevents There is a from showing up in frount
            string[] Creature = CreatureDisplay();
            if (Convert.ToString(Creature[3]) == "false")
            {
                North.IsVisible = false;
                West.IsVisible = false;
                East.IsVisible = false;
                South.IsVisible = false;
            }
            else
            {
                North.IsVisible = true;
                West.IsVisible = true;
                East.IsVisible = true;
                South.IsVisible = true;
            }
            DisplayText.Text = "You are" + Terrain[2] + " " + Terrain[1] + "\n" + Resources[0][2] + Resources[1][2] + Resources[2][2] + Resources[3][2];
            if (Convert.ToString(Creature[0]) == "true")
            {
                DisplayText.Text += Creature[2];
            }

            Debug.Text = "Debug info\nLocation " + Terrain + "\nRes0: " + Resources[0][0] + " " + Resources[0][1] + " " + Resources[0][2] + "Res1: " + Resources[1][0] + " " + Resources[1][1] + " " + Resources[1][2] + "Res2: " + Resources[2][0] + " " + Resources[2][1] + " " + Resources[2][2] + "Res3: " + Resources[3][0] + " " + Resources[3][1] + " " + Resources[3][2];
            Coords.Text = "Coordinates:\nX: " + PlayerData.Coordinates[0] + "\nY: " + PlayerData.Coordinates[1];
        }

        public static string[] CreatureDisplay()
        {
            Random rnd = new Random();
            string[] output = { "true", "", "", "", "", "", "", "", "" };
            /* 0 - Should anything be displayed (bool)
             * 1 - is this hostile (bool)
             * 2 - Enemy name
             * 3 - Enemy attack
             * 4 - Enemy Defence
             * 5 - Enemy Evasion
             * 6 - Enemy Magic
             * 7 - Enemy Health */

            int Decider = rnd.Next(0, 9);
            if (Decider < 2) { output[0] = "false"; } //20% chance of nothing spawning
            else if (Decider > 2 && Decider < 8)   // 60% chance of enemy
            {
                List<object> temp = new List<object>();
                temp.AddRange(Generate.HostileGenerate());
                output[1] = "true";
                output[2] = "\nThere is a " + Convert.ToString(temp[0]);

                output[3] = Convert.ToString(temp[1]);
                output[4] = Convert.ToString(temp[2]);
                output[5] = Convert.ToString(temp[3]);
                output[6] = Convert.ToString(temp[4]);
                output[7] = Convert.ToString(temp[5]);
            }
            else if (Decider >= 8)
            {
                output[0] = "\nThere is a " + Resource.PassiveCreatures[rnd.Next(0, Resource.PassiveCreatures.Count)].ToLower();
                output[1] = "false";
            }
            return output;
        }
    }
}