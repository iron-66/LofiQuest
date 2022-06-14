using LofiQuest.Controllers;
using LofiQuest.Entities;
using LofiQuest.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LofiQuest
{
    /// <summary>
    /// Функционал миниигры "соедини провода"
    /// </summary>
    class CabelsGame
    {
        public static int Top; // Крайняя верхняя координата окна
        public static int Left; // Крайняя левая координата окна
        public static List<PictureBox> Cabeles; // Список PictureBox'ов, соответствующих провода

        public static int[,] Switchboard = new int[,] // Массив с положениями элементов в электрощитке
        {
            { 3, 1, 1 },
            { 2, 1, 1 },
            { 2, 1, 1 },
        };

        /// <summary>
        /// Инициализация элементов, необходимых
        /// для работоспособности миниигры
        /// </summary>
        /// <param name="wire"> PictureBox'ы проводов </param>
        /// <param name="player"> Переменная, посредством которой происходит управление персонажем </param>
        public static void Init(List<PictureBox> wire, Hero player)
        {
            Cabeles = wire;
        }

        /// <summary>
        /// Метод, обрабатывающий положение курсора и клик мыши по объекту;
        /// По щелчку мыши на объект, вызывает метод поворота данного объекта
        /// </summary>
        /// <param name="sender"> Отправитеель </param>
        /// <param name="e"> Аргументы мыши (клики, координаты курсора)) </param>
        public static void Click(object sender, MouseEventArgs e)
        {
            if (MapController.currentLVL == "Levels\\SecurityElectro.png")
            {
                //Point Control.PointToClient(Point point);
                if ((Cursor.Position.X - Left > 779) && (Cursor.Position.X - Left < 898) && (Cursor.Position.Y - Top) > 189 && (Cursor.Position.Y - Top) < 309)
                {
                    var img = RotateElement(0, 0);
                    Cabeles[0].Visible = true;
                    Cabeles[0].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Cabeles\\1-"+img+".png"));
                    CheckSolve();
                }

                if ((Cursor.Position.X - Left > 898) && (Cursor.Position.X - Left < 1018) && (Cursor.Position.Y - Top) > 189 && (Cursor.Position.Y - Top) < 309)
                {
                    var img = RotateElement(0, 1);
                    Cabeles[1].Visible = true;
                    Cabeles[1].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Cabeles\\2-" + img + ".png"));
                    CheckSolve();
                }

                if ((Cursor.Position.X - Left > 1018) && (Cursor.Position.X - Left < 1138) && (Cursor.Position.Y - Top) > 189 && (Cursor.Position.Y - Top) < 309)
                {
                    var img = RotateElement(0, 2);
                    Cabeles[2].Visible = true;
                    Cabeles[2].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Cabeles\\1-" + img + ".png"));
                    CheckSolve();
                }
                // Вторая линия
                if ((Cursor.Position.X - Left > 779) && (Cursor.Position.X - Left < 898) && (Cursor.Position.Y - Top) > 309 && (Cursor.Position.Y - Top) < 429)
                {
                    var img = RotateElement(1, 0);
                    Cabeles[3].Visible = true;
                    Cabeles[3].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Cabeles\\3-" + img + ".png"));
                    CheckSolve();
                }

                if ((Cursor.Position.X - Left > 898) && (Cursor.Position.X - Left < 1018) && (Cursor.Position.Y - Top) > 309 && (Cursor.Position.Y - Top) < 429)
                {
                    var img = RotateElement(1, 1);
                    Cabeles[4].Visible = true;
                    Cabeles[4].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Cabeles\\3-" + img + ".png"));
                    CheckSolve();
                }

                if ((Cursor.Position.X - Left > 1018) && (Cursor.Position.X - Left < 1138) && (Cursor.Position.Y - Top) > 309 && (Cursor.Position.Y - Top) < 429)
                {
                    var img = RotateElement(1, 2);
                    Cabeles[5].Visible = true;
                    Cabeles[5].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Cabeles\\1-" + img + ".png"));
                    CheckSolve();
                }
                // Третья линия
                if ((Cursor.Position.X - Left > 779) && (Cursor.Position.X - Left < 898) && (Cursor.Position.Y - Top) > 429 && (Cursor.Position.Y - Top) < 549)
                {
                    var img = RotateElement(2, 0);
                    Cabeles[6].Visible = true;
                    Cabeles[6].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Cabeles\\1-" + img + ".png"));
                    CheckSolve();
                }

                if ((Cursor.Position.X - Left > 898) && (Cursor.Position.X - Left < 1018) && (Cursor.Position.Y - Top) > 429 && (Cursor.Position.Y - Top) < 549)
                {
                    var img = RotateElement(2, 1);
                    Cabeles[7].Visible = true;
                    Cabeles[7].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Cabeles\\1-" + img + ".png"));
                    CheckSolve();
                }

                if ((Cursor.Position.X - Left > 1018) && (Cursor.Position.X - Left < 1138) && (Cursor.Position.Y - Top) > 429 && (Cursor.Position.Y - Top) < 549)
                {
                    var img = RotateElement(2, 2);
                    Cabeles[8].Visible = true;
                    Cabeles[8].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Cabeles\\1-" + img + ".png"));
                    CheckSolve();
                }
            }
        }

        /// <summary>
        /// Поворот изображения под указанным индексом
        /// </summary>
        /// <param name="i"> Строка, в которой расположен элемент </param>
        /// <param name="j"> Столбец, в которой расположен элемент </param>
        /// <returns> Индекс поворота элемента </returns>
        public static int RotateElement(int i, int j)
        {
            if (Switchboard[i,j] == 1)
            {
                Switchboard[i, j] = 2;
                return 2;
            }
            if (Switchboard[i, j] == 2)
            {
                Switchboard[i, j] = 3;
                return 3;
            }
            if (Switchboard[i, j] == 3)
            {
                Switchboard[i, j] = 4;
                return 4;
            }
            if (Switchboard[i, j] == 4)
            {
                Switchboard[i, j] = 1;
                return 1;
            }
            return 1;
        }

        /// <summary>
        /// Метод, скрываающий изображения проводов с экрана
        /// </summary>
        public static void HideElements()
        {
            for (int i = 0; i < 9; i++)
                Cabeles[i].Visible = false;
        }

        /// <summary>
        /// Проверка, решена ли головоломка
        /// </summary>
        public static void CheckSolve()
        {
            if ((Switchboard[0, 0] == 1) && (Switchboard[0, 1] == 3 || Switchboard[0, 1] == 4) && (Switchboard[1, 1] == 2 || Switchboard[1, 1] == 4) && (Switchboard[2, 1] == 1) && (Switchboard[2, 2] == 3))
            {
                HeroParams.gateIsOpen = true;
                HideElements();
                MapController.Init("Levels\\SecurityRoomGreen.png");
            }
        }
    }
}
