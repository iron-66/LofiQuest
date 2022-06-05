using Game_quest.Controllers;
using Game_quest.Entities;
using Game_quest.Models;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_quest
{
    /// <summary>
    /// Класс, обеспечивающий работоспособность появляющегося снизу текста и кат-сцен
    /// </summary>
    class DialogController
    {
        public static PictureBox Text;
        public static PictureBox SceneController;
        public static Hero Player;
        public static int Page = 0;
        public static int DialogPoint = 1;
        public static bool FirstActionDone = false;
        public static bool FirstTimeOnLevel = true;
        public static bool CanFlipPage = false;
        public static bool IntroSkipped = false;

        /// <summary>
        /// Инициализация компонентов для работоспособности появляющегося снизу текста и кат-сцен
        /// </summary>
        /// <param name="scene"> Название сцены, которую необходимо отрисовать </param>
        /// <param name="dialBox"> Диалоговое окно </param>
        /// <param name="player"> Переменная, посредством которой происходит управление персонажем </param>
        public static void Init(PictureBox scene, PictureBox dialBox, Hero player)
        {
            SceneController = scene;
            Text = dialBox;
            Player = player;
        }

        /// <summary>
        /// Метод, отвечающий за отрисовку появляющегося снизу текста
        /// </summary>
        public static void DisplayText()
        {
            if (IntroSkipped && !FirstActionDone)
                Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Start.png");

            else if (MapController.currentLVL == "Levels\\Room.png" && Hero.canInteract)
            {
                if (Player.posX > 360 && Player.posX < 607 && Player.posY < 170)
                {
                    if (HeroParams.delay == 0 && Player.lastkey == "W")
                    {
                        Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Anek" + Page + ".png");
                        UpdateСounter();
                        HeroParams.delay = 1;
                    }                    
                }
                else if (Player.posX < 250 && IntroSkipped)
                {
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\PC.png");
                    //ShowScene("Menu"); В будущем меню будет доделано
                }
                    
                else if (Player.posX > 835)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Bed.png");
                else if (IntroSkipped)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Base.png");
            }

            else if (MapController.currentLVL == "Levels\\Yard.png" || MapController.currentLVL == "Levels\\YardGate.png")
            {
                if (FirstTimeOnLevel && !HeroParams.haveKey)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\YardText.png");
                else if (FirstTimeOnLevel && HeroParams.haveKey && !HeroParams.visitTree && !HeroParams.gateIsOpen)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\CanGoToSeq.png");
                else if (HeroParams.visitTree && !HeroParams.canVisitStock)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\SawLadder.png");
                else if (HeroParams.canVisitStock && !HeroParams.generatorDisabled)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\NeedDisable.png");
                else if (HeroParams.generatorDisabled)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\GoToLadder.png");

                if ((Player.posX > 900) && (Player.posY < 80) && !HeroParams.haveKey)
                {
                    FirstTimeOnLevel = false;
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\NearSecurity.png");
                }

                else if ((Player.posX > 1050) && (Player.posY > 100) && !HeroParams.canVisitStock)
                {
                    FirstTimeOnLevel = false;
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\NearStock.png");
                }

                else if (Hero.canInteract)
                {
                    FirstTimeOnLevel = false;
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\SaveTheCat.png");
                }
            }

            else if (MapController.currentLVL == "Levels\\GroundWorks.png")
            {
                if (FirstTimeOnLevel)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\GroundText.png");

                if (Hero.canInteract)
                {
                    FirstTimeOnLevel = false;
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\NeedPlank.png");
                }
            }

            else if (MapController.currentLVL == "Levels\\Maze.png")
            {
                if (FirstTimeOnLevel)
                {
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Plank.png");
                }
            }

            else if (MapController.currentLVL == "Levels\\GroundBridge.png" || MapController.currentLVL == "Levels\\GroundFinal.png")
            {
                if (FirstTimeOnLevel)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\PlankPlaced.png");

                if (Hero.canInteract)
                {
                    FirstTimeOnLevel = false;
                    if (Player.posX < 400)
                    {
                        if (!HeroParams.haveKey && HeroParams.delay == 0 && DialogPoint < 10)
                        {
                            if (DialogPoint == 9)
                                HeroParams.haveKey = true;
                            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Dial" + DialogPoint + ".png");
                            DialogPoint++;
                            HeroParams.delay = 1;                           
                        }

                        if (HeroParams.visitTree && HeroParams.delay == 0 && DialogPoint < 16)
                        {
                            if (DialogPoint == 15)
                                HeroParams.canVisitStock = true;
                            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Dial" + DialogPoint + ".png");
                            DialogPoint++;
                            HeroParams.delay = 1;                                  
                        }

                        if (HeroParams.generatorDisabled && HeroParams.delay == 0 && DialogPoint < 27)
                        {
                            if (DialogPoint == 26)
                                ShowScene("End");
                            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Dial" + DialogPoint + ".png");
                            DialogPoint++;
                            HeroParams.delay = 1;
                        }
                    }
                    
                    else Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\SaveTheCat.png");
                }
            }

            else if (MapController.currentLVL == "Levels\\SecurityRoom.png")
            {
                Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\NearCabels.png");
            }

            else if (MapController.currentLVL == "Levels\\SecurityElectro.png")
            {
                Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\ElectroGame.png");
            }

            else if (MapController.currentLVL == "Levels\\SecurityRoomGreen.png")
            {
                Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\GateIsOpen.png");
            }

            else if (MapController.currentLVL == "Levels\\Tree.png")
            {
                if (Player.posX > 600)
                    Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\NeedLadder.png");
                else Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Ks.png");
            }

            else if (Hero.canInteract)
            {
                FirstTimeOnLevel = false;
                Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Base.png");
            }

            else if (MapController.currentLVL == "Levels\\Stock.png")
            {
                Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\NearLevers.png");
            }

            else if (MapController.currentLVL == "Levels\\StockGame.png")
            {
                Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\LeversGame.png");
            }

            else if (MapController.currentLVL == "Levels\\StockOff.png")
            {
                Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Disabled.png");
            }
        }

        /// <summary>
        /// Метод, отвечающий за обновление счётчика страниц (перелистывание);
        /// Всего 8 страниц, после 7 идёт нулевая
        /// </summary>
        public static void UpdateСounter()
        {
            if (Page < 7)
                Page++;
            if (Page == 7)
                Page = 0;
        }

        /// <summary>
        /// Отображение кат-сцен (начало, конец игры и некорорые элементы взаимодействия)
        /// </summary>
        public static void ShowScene(string scene)
        {
            if (scene == "Start")
            {
                IntroAsync.Start();
                IntroAsync.Wait();
                IntroAsync.Dispose();
            }
            
            else
            {
                SceneController.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Levels\\" + scene + ".png");
                SceneController.Visible = true;
            }
        }

        /// <summary>
        /// Метод, проигрывающий заставки при старте игры
        /// </summary>
        static Task IntroAsync = new Task(async () =>
        {
            SceneController.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Levels\\Start.png");
            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Intro1.png");
            SceneController.Visible = true;
            await Task.Delay(3000);
            SceneController.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Levels\\StartPixel.png");
            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Intro2.png");
            await Task.Delay(3000);
            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Intro3.png");
            await Task.Delay(3000);
            SceneController.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Levels\\CatSneefPixel.png");
            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Intro4.png");
            await Task.Delay(4000);
            SceneController.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Levels\\CatJumped.png");
            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Intro5.png");
            await Task.Delay(4000);
            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Intro6.png");
            await Task.Delay(4000);
            Text.ImageLocation = Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Dialogs\\Start.png");
            SceneController.Invoke((MethodInvoker) (() => SceneController.Visible = false));
            IntroSkipped = true;
        });
    }
}
