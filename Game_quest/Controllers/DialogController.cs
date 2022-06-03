using Game_quest.Controllers;
using Game_quest.Entities;
using Game_quest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_quest
{
    class DialogController
    {
        public static PictureBox Text;
        public static Hero Player;

        public static void Init(PictureBox dialBox, Hero player)
        {
            Text = dialBox;
            Player = player;
        }

        public static void DisplayText()
        {
            if (!HeroParams.firstActionDone)
                Text.ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Start.png"));

            else if (MapController.currentLVL == "Levels\\Room.png" && Hero.canInteract)
            {
                //if 
                Text.ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Base.png"));
            }
                
            else Text.ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Base.png"));
        }
    }
}
