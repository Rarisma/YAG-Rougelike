using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace YAGRougelike
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public static class WorldData
    {
        public static List<string[]> WorldResources = new List<string[]>();
    }

    public partial class Overworld : ContentPage
    {
        public Overworld()
        {
            InitializeComponent();
            GameData.ClearResources();
            GameData.ReloadResources();
            GameData.PlayerDataReset();
            WorldGen();
        }

        private void Move(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string ButtonText = Convert.ToString(button.Text.Replace("Move ", ""));

            if (ButtonText == "North") { GameData.PlayerDataCoordinates[1] += 1; }
            if (ButtonText == "South") { GameData.PlayerDataCoordinates[1] -= 1; }
            if (ButtonText == "East") { GameData.PlayerDataCoordinates[0] += 1; }
            if (ButtonText == "West") { GameData.PlayerDataCoordinates[0] -= 1; }
            WorldGen();
        }

        public void WorldGen()
        {
            WorldData.WorldResources.Clear();
            string[] Terrain = Generate.Terrain();
            WorldData.WorldResources.AddRange(Generate.Resources("//Data//Resources//Terrain//" + Terrain[0] + "//" + Terrain[1], Terrain[2]));
            for (int i = 0; i < Resources.Count; i++) { if (WorldData.WorldResources[i][3].Length <= 25 && WorldData.WorldResources[i][3].Length >= 5) { WorldData.WorldResources[i][3] = ""; } } //prevents There is a from showing up in frount

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
            if (Terrain[0] == "Forests") { DisplayText.Text = "You are " + Terrain[1]; }
            else { DisplayText.Text = "You are" + Terrain[2] + " " + Terrain[1]; }
            DisplayText.Text = DisplayText.Text + WorldData.WorldResources[0][3] + WorldData.WorldResources[1][3] + WorldData.WorldResources[2][3] + WorldData.WorldResources[3][3];
            if (Convert.ToString(Creature[0]) == "true") { DisplayText.Text += Creature[2]; } //This only displays creatures if its enabled

            //for (int i = 0; i < Resources.Count; i++) { if (Resources[i][3].Length <= 25 && Resources[i][3].Length >= 5) { Resources[i][2] += " <---This line got Vectored!"; } } //prevents There is a from showing up in frount
            //DisplayText.Text += "\nDebug info\nRes0: " + Resources[0][0] + " " + Resources[0][1] + " " + Resources[0][2] + "\nRes1: " + Resources[1][0] + " " + Resources[1][1] + " " + Resources[1][2] + "\nRes2: " + Resources[2][0] + " " + Resources[2][1] + " " + Resources[2][2] + "\nRes3: " + Resources[3][0] + " " + Resources[3][1] + " " + Resources[3][2];
            Coords.Text = "Coordinates: (" + GameData.PlayerDataCoordinates[0] + "," + GameData.PlayerDataCoordinates[1] + ")";
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
                output[0] = "\nThere is a " + GameData.PassiveCreatures[rnd.Next(0, GameData.PassiveCreatures.Count)].ToLower();
                output[1] = "false";
            }
            return output;
        }

        private void CollectItems(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (GameData.PlayerInventory.Contains(WorldData.WorldResources[i][2]))
                    {
                        GameData.PlayerInventoryAmmount[GameData.PlayerInventory.IndexOf(WorldData.WorldResources[i][2])] = GameData.PlayerInventoryAmmount[GameData.PlayerInventory.IndexOf(WorldData.WorldResources[i][2])] + Convert.ToInt64(WorldData.WorldResources[i][0]);
                    }
                    else { GameData.PlayerInventory.Add(WorldData.WorldResources[i][2]); GameData.PlayerInventoryAmmount.Add(Convert.ToInt64(WorldData.WorldResources[i][0])); }
                }
            }
            catch
            {
                int a = 0;
            }
            WorldGen();
        }

        private void Inventory(object sender, EventArgs e)
        {
            string output = "";
            for (int i = 0; i < GameData.PlayerInventory.Count - 1; i++)
            {
                output += "\n" + GameData.PlayerInventory[i] + " - " + GameData.PlayerInventoryAmmount[i];
            }
            DisplayAlert("Current Inventory", output, "ok");
        }
    }
}