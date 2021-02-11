using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
/* Yet Another Rougelike - ZeroTwo Edition
⣿⣿⣿⣿⣯⣿⣿⠄⢠⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⠈⣿⣿⣿⣿⣿⣿⣆⠄
⢻⣿⣿⣿⣾⣿⢿⣢⣞⣿⣿⣿⣿⣷⣶⣿⣯⣟⣿⢿⡇⢃⢻⣿⣿⣿⣿⣿⢿⡄
⠄⢿⣿⣯⣏⣿⣿⣿⡟⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣧⣾⢿⣮⣿⣿⣿⣿⣾⣷
⠄⣈⣽⢾⣿⣿⣿⣟⣄⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣝⣯⢿⣿⣿⣿⣿
⣿⠟⣫⢸⣿⢿⣿⣾⣿⢿⣿⣿⢻⣿⣿⣿⢿⣿⣿⣿⢸⣿⣼⣿⣿⣿⣿⣿⣿⣿
⡟⢸⣟⢸⣿⠸⣷⣝⢻⠘⣿⣿⢸⢿⣿⣿⠄⣿⣿⣿⡆⢿⣿⣼⣿⣿⣿⣿⢹⣿
⡇⣿⡿⣿⣿⢟⠛⠛⠿⡢⢻⣿⣾⣞⣿⡏⠖⢸⣿⢣⣷⡸⣇⣿⣿⣿⢼⡿⣿⣿
⣡⢿⡷⣿⣿⣾⣿⣷⣶⣮⣄⣿⣏⣸⣻⣃⠭⠄⠛⠙⠛⠳⠋⣿⣿⣇⠙⣿⢸⣿
⠫⣿⣧⣿⣿⣿⣿⣿⣿⣿⣿⣿⠻⣿⣾⣿⣿⣿⣿⣿⣿⣿⣷⣿⣿⣹⢷⣿⡼⠋
⠄⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡟⣿⣿⣿⠄⠄
⠄⠄⢻⢹⣿⠸⣿⣿⣿⣿⣿⣷⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⣼⣿⣿⣿⣿⡟⠄⠄
⠄⠄⠈⢸⣿⠄⠙⢿⣿⣿⣹⣿⣿⣿⣿⣟⡃⣽⣿⣿⡟⠁⣿⣿⢻⣿⣿⢿⠄⠄
⠄⠄⠄⠘⣿⡄⠄⠄⠙⢿⣿⣿⣾⣿⣷⣿⣿⣿⠟⠁⠄⠄⣿⣿⣾⣿⡟⣿⠄⠄
⠄⠄⠄⠄⢻⡇⠸⣆⠄⠄⠈⠻⣿⡿⠿⠛⠉⠄⠄⠄⠄⢸⣿⣇⣿⣿⢿⣿⠄
*/

namespace YetAnotherGenericRougelikeGame
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GameView : ContentPage
    {
        public GameView()
        {
            Generate.ClearResources();
            Generate.ReloadResources();
            InitializeComponent();
            WorldGen();
        }

        /* To do
            - Put resources from Generate.cs into its own file
            - Split World gen into mutliple functions and make WorldGen call them
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
            string[] Resource0 = { "", "", "" };
            string[] Resource1 = { "", "", "" };
            string[] Resource2 = { "", "", "" };
            string[] Resource3 = { "", "", "" };
            string[] Creature  = { "", "", "" };
            
            if (Decider > 0)  {Resource0 = Generate.ResouceGenerate();} // 90% chance for 1 resource
            if (Decider >= 5) {Resource1 = Generate.ResouceGenerate();} // 50% chance for 2 resources
            if (Decider >= 7) {Resource2 = Generate.ResouceGenerate();} // 30% chance for 3 resources
            if (Decider >= 9) {Resource3 = Generate.ResouceGenerate();} // 10% chance for 4 resouces

            TXTResources0.Text = Resource0[2];
            TXTResources1.Text = Resource1[2];
            TXTResources2.Text = Resource2[2];
            TXTResources3.Text = Resource3[2];

            //Creature/Enemy/Boss Generation below
            Decider = rnd.Next(0, 9);
            TXTEnemy.IsEnabled = true; // re-enables TXTEnemy just incase its disabled

            if (Decider < 2) { TXTEnemy.IsEnabled = false; }                                // 30% for nothing
            else if (Decider > 2 && Decider < 8) { Creature = Generate.HostileGenerate(); } // 40% Chance for an enemy
            else if (Decider >= 8) { Creature = Generate.CreatureGenerate(); }              // 20% Chance for an animal

            TXTEnemy.Text = "There is a " + Creature[1]; //Displays Generated creature
        }

        private void Move(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string ButtonText = Convert.ToString(button.Text.Replace("Move ",""));
            WorldGen();
        }
    }
}