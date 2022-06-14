using LofiQuest.Controllers;
using LofiQuest.Entities;
using LofiQuest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LofiQuest
{
    /// <summary>
    /// Функционал миниигры "опусти рычаги"
    /// </summary>
    class LeverGame
    {
        public static int Top; // Крайняя верхняя координата окна
        public static int Left; // Крайняя левая координата окна
        public static Hero Player;
        public static List<PictureBox> Levers; // Список PictureBox'ов, соответствующих рычагам
        public static int[] LeverPanel = new int[] { 1, 1, 1, 1 }; // Массив с положениями рычагов на панели

        /// <summary>
        /// Инициализация компонентов, необходимых
        /// для работоспособности миниигры
        /// </summary>
        /// <param name="tumblers"> PictureBox'ы рычагов </param>
        /// <param name="player"> Переменная, посредством которой происходит управление персонажем </param>
        public static void Init(List<PictureBox> tumblers, Hero player)
        {
            Levers = tumblers;
            Player = player;
        }

        public static void Click(object sender, EventArgs e)
        {
            if (MapController.currentLVL == "Levels\\StockGame.png")
            {
                if ((Cursor.Position.X - Left > 111) && (Cursor.Position.X - Left < 144) && (Cursor.Position.Y - Top) > 261 && (Cursor.Position.Y - Top) < 452)
                { // Нажатие на первый, связанный с четвёртым
                    var position = SwitchElement(0);
                    DrawElement(0, position);
                    position = SwitchElement(3);
                    DrawElement(3, position);

                    Levers[0].Visible = true;
                    Levers[3].Visible = true;

                    CheckSolve();
                }

                if ((Cursor.Position.X - Left > 274) && (Cursor.Position.X - Left < 308) && (Cursor.Position.Y - Top) > 261 && (Cursor.Position.Y - Top) < 452)
                { // Нажатие на второй, связанный с первым и четвёртым
                    var position = SwitchElement(1);
                    DrawElement(1, position);
                    position = SwitchElement(0);
                    DrawElement(0, position);
                    position = SwitchElement(3);
                    DrawElement(3, position);

                    Levers[0].Visible = true;
                    Levers[1].Visible = true;
                    Levers[3].Visible = true;

                    CheckSolve();
                }

                if ((Cursor.Position.X - Left > 426) && (Cursor.Position.X - Left < 460) && (Cursor.Position.Y - Top) > 261 && (Cursor.Position.Y - Top) < 452)
                { // Нажатие на третий, связанный с третим
                    var position = SwitchElement(2);
                    DrawElement(2, position);
                    position = SwitchElement(1);
                    DrawElement(1, position);

                    Levers[2].Visible = true;
                    Levers[1].Visible = true;

                    CheckSolve();
                }

                if ((Cursor.Position.X - Left > 577) && (Cursor.Position.X - Left < 611) && (Cursor.Position.Y - Top) > 261 && (Cursor.Position.Y - Top) < 452)
                { // Нажатие на четвёртый, связанный со вторым
                    var position = SwitchElement(3);
                    DrawElement(3, position);
                    position = SwitchElement(1);
                    DrawElement(1, position);

                    Levers[1].Visible = true;
                    Levers[3].Visible = true;

                    CheckSolve();
                }
            }
        }

        /// <summary>
        /// Отрисовка указанного рычага
        /// </summary>
        /// <param name="i"> Индекс рычага </param>
        /// <param name="position"> Положение - вкл или выкл </param>
        public static void DrawElement(int i, int position)
        {
            Levers[i].ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "Levers\\" + position + ".png"));
        }

        /// <summary>
        /// Переключение рычага под указанным индексом
        /// </summary>
        /// <param name="i"> Индекс рычага </param>
        /// <returns>
        /// Позиция рычага:
        /// 0 - выкл
        /// 1 - вкл
        /// </returns>
        public static int SwitchElement(int i)
        {
            if (LeverPanel[i] == 1)
            {
                LeverPanel[i] = 0;
                return 0;
            }
            if (LeverPanel[i] == 0)
            {
                LeverPanel[i] = 1;
                return 1;
            }
            return 1;
        }

        /// <summary>
        /// Метод, скрываающий изображения проводов с экрана
        /// </summary>
        public static void HideElements()
        {
            for (int i = 0; i < 4; i++)
                Levers[i].Visible = false;
        }

        /// <summary>
        /// Проверка, решена ли головоломка
        /// </summary>
        public static void CheckSolve()
        {
            if ((LeverPanel[0] == 0) && (LeverPanel[1] == 0) && (LeverPanel[2] == 0) && (LeverPanel[3] == 0))
            {
                HeroParams.generatorDisabled = true;
                HideElements();
                MapController.Init("Levels\\StockOff.png");
            }
        }
    }
}
