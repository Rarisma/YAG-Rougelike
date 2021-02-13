using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            WorldGen();
        }

        private void Move(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string ButtonText = Convert.ToString(button.Text.Replace("Move ", ""));
            WorldGen();
        }

        public void WorldGen()
        {
            Random rnd = new Random();
            int Decider = rnd.Next(0, 10);

            string[] Terrain = Generate.Terrain();
            List<string[]> Resources = Display.Resources();
            string[] Creature = CreatureDisplay();
            if (Creature[3] == "false")
            {
                North.IsVisible = false;
                West.IsVisible  = false;
                East.IsVisible  = false;
                South.IsVisible = false;
            }
            else
            {
                North.IsVisible = true;
                West.IsVisible  = true;
                East.IsVisible  = true;
                South.IsVisible = true;
            }
            DisplayText.Text = "You are " + Terrain[2] + "\n" + Resources[0][2] + Resources[1][2] + Resources[2][2] + Resources[3][2] + Creature[1];
        }
         
        public static string[] CreatureDisplay()
        {
            Random rnd = new Random();
            int Decider = rnd.Next(0, 9);

            string[] output = { "", "", "", "" };

            if (Decider < 2) { output[0] = ""; }                                          // 30% for nothing
            else if (Decider > 2 && Decider < 8) { output = Generate.HostileGenerate(); } // 40% Chance for an enemy
            else if (Decider >= 8) { output = Generate.CreatureGenerate(); }              // 20% Chance for an animal

            if (Decider > 2) { output[1] = "There is a " + output[1]; } //Adds text before is a creature is generated
            if (Decider > 2 && Decider < 8 && rnd.Next(0,2) == 1) { output[3] = "false"; }
            return output;
        }
    }
}