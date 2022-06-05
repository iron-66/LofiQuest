using Game_quest.Controllers;
using Game_quest.Models;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Game_quest.HeroesCFG
{
    /// <summary>
    /// Функциональная составляющая персонажа - строителя
    /// </summary>
    class Worker
    {
        public static PictureBox Sprite; // Спрайт строителя
        public static int Counter = 0; // Счётчик для смены анимации

        /// <summary>
        /// Инициалтзация компонентов, необходимых для работоспособности строителя
        /// </summary>
        /// <param name="sprite"> Изображение строителя </param>
        public static void Init(PictureBox sprite)
        {
            Sprite = sprite;
            Sprite.BackColor = System.Drawing.Color.Transparent;
        }

        /// <summary>
        /// Отрисовка спрайта и анимации строителя
        /// </summary>
        public static void Activate()
        {
            if ((MapController.currentLVL == "Levels\\GroundWorks.png" || MapController.currentLVL == "Levels\\GroundBridge.png") && !HeroParams.generatorDisabled)
            {               
                Sprite.ImageLocation = (Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), "HeroesSprites\\WorkerWorry.png"));
                Sprite.Visible = true;
                if (Counter == 0)
                {
                    Sprite.Location = new Point(Sprite.Location.X, Sprite.Location.Y + 10);
                    Counter++;
                }
                else
                {
                    Sprite.Location = new Point(Sprite.Location.X, Sprite.Location.Y - 10);
                    Counter--;
                }
            }
        }

        /// <summary>
        /// Метод, который стирает спрайт строителя,
        /// т.е. деактивирующий его
        /// </summary>
        public static void Deactivate()
        {
            Sprite.Visible = false;
        }
    }
}
