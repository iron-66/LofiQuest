using Game_quest.Controllers;
using Game_quest.Entities;
using Game_quest.HeroesCFG;
using Game_quest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_quest
{
    /// <summary>
    /// Главный класс, содержащий параметры формы
    /// </summary>
    public partial class Form1 : Form
    {       
        public Image lofiSheet;
        public Hero player;
        public static PictureBox Scene;

        public Form1()
        {
            InitializeComponent();
          
            timer1.Interval = 50;
            timer1.Tick += new EventHandler(Update);

            MouseClick += CabelsGame.Click;
            MouseClick += LeverGame.Click;
            KeyDown += new KeyEventHandler(OnPress);
            KeyUp += new KeyEventHandler(OnKeyUp);

            Init("Levels\\Room.png");

            this.Top = Screen.PrimaryScreen.Bounds.Height / 2 - 500;
            this.Left = Screen.PrimaryScreen.Bounds.Width/ 2 - 600;

            var cabelsGrid = new List<PictureBox>
            {
                pictureBox1, pictureBox2, pictureBox3,
                pictureBox4, pictureBox5, pictureBox6,
                pictureBox7, pictureBox8, pictureBox9,
            };

            Scene = pictureBox15;

            var levers = new List<PictureBox> { pictureBox11, pictureBox12, pictureBox13, pictureBox14 };            

            CabelsGame.Init(cabelsGrid, player);
            LeverGame.Init(levers, player);
            DialogController.Init(pictureBox16, player);
            Worker.Init(pictureBox10);
        }

        /// <summary>
        /// Метод, содержащий действия, происходящие
        /// после отжатия клавиш клавиатуры
        /// Необходим, чтобы предотвратить "input lag"
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Действие клавиатуры </param>
        public void OnKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.dirY = 0;
                    break;
                case Keys.S:
                    player.dirY = 0;
                    break;
                case Keys.A:
                    player.dirX = 0;
                    break;
                case Keys.D:
                    player.dirX = 0;
                    break;
                case Keys.E:
                    Hero.canInteract = false;
                    break;
            }

            if (player.dirX == 0 && player.dirY == 0)
            {
                player.isMoving = false;
                if (player.lastkey == "W")
                    player.SetAnimationConfiguration(5);
                else if (player.lastkey == "A")
                    player.SetAnimationConfiguration(6);
                else if (player.lastkey == "D")
                    player.SetAnimationConfiguration(7);
                else player.SetAnimationConfiguration(0);
            }
        }

        /// <summary>
        /// Метод, обрабатывающий нажатия клавиш
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Действие клавиатуры </param>
        public void OnPress(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    player.dirY = -12;
                    player.isMoving = true;
                    player.lastkey = "W";
                    player.SetAnimationConfiguration(1);
                    break;
                case Keys.S:
                    player.dirY = 12;
                    player.isMoving = true;
                    player.lastkey = "S";
                    player.SetAnimationConfiguration(2);
                    break;
                case Keys.A:
                    player.dirX = -12;
                    player.isMoving = true;
                    player.lastkey = "A";
                    player.SetAnimationConfiguration(3);
                    break;
                case Keys.D:
                    player.dirX = 12;
                    player.isMoving = true;
                    player.lastkey = "D";
                    player.SetAnimationConfiguration(4);
                    break;
                case Keys.E:
                    Hero.canInteract = true;
                    HeroParams.firstActionDone = true;
                    break;
            }
            player.isMoving = true;
        }

        /// <summary>
        /// Инициализация начальных объетов;
        /// Установка размеров формы
        /// </summary>
        /// <param name="lvl"> Название уровня </param>
        public void Init(string lvl)
        {
            MapController.formEditor = this;
            MapController.Init(lvl);

            this.Width = 1200 + 16;
            this.Height = 900 + 59;
           
            lofiSheet = new Bitmap(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "HeroesSprites\\Lofi.png"));
            player = new Hero(150, 250, HeroParams.totalFrames, HeroParams.runUpFrames, HeroParams.runDownFrames, HeroParams.runLeftFrames, HeroParams.runRightFrames, lofiSheet);
            timer1.Start();
        }

        /// <summary>
        /// Метод, содержащий операции, которые необходимо производить постоянно
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы событий </param>
        public void Update(object sender, EventArgs e)
        {
            Worker.Activate();
            if (!PhysicsController.IsCollide(player, new Point(player.dirX, player.dirY)))
            {
                if (player.isMoving)
                    player.Move();
            }

            DialogController.DisplayText();
            CabelsGame.Top = this.Location.Y;
            CabelsGame.Left = this.Location.X;
            LeverGame.Top = this.Location.Y;
            LeverGame.Left = this.Location.X;

            Invalidate();
        }

        /// <summary>
        /// Отрисовка объектов
        /// </summary>
        /// <param name="sender"> Отправитель </param>
        /// <param name="e"> Аргументы событий </param>
        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            PhysicsController.g = g;
            MapController.GetCollision();
            player.PlayAnimation(g);
        }

        /// <summary>
        /// Отображение кат-сцен (начало, конец игры и некорорые элементы взаимодействия)
        /// </summary>
        public static void ShowScene()
        {
            Scene.ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Levels\\End.png"));
            Scene.Visible = true;
        }
    }
}
