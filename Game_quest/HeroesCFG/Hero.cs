using LofiQuest.Controllers;
using System.Drawing;

namespace LofiQuest.Entities
{
    /// <summary>
    /// Функциональная составляющая главного героя
    /// </summary>
    public class Hero
    {
        public int posX;
        public int posY;

        public int dirX;
        public int dirY;
        public bool isMoving;
        public static bool canInteract;

        public int currentAnimation;
        public int currentFrame;
        public int currentLimit;

        public int idleFrontFrames;
        public int idleBackFrames;
        public int idleLeftFrames;
        public int idleRightFrames;
        public int runUpFrames;
        public int runDownFrames;     
        public int runLeftFrames;
        public int runRightFrames;

        public string lastkey;

        public int sizeX;
        public int sizeY;
        public float PageScale { get; set; }

        public Image spriteSheet;

        /// <summary>
        /// Конструктор сущностей
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        /// <param name="idleFrames"></param>
        /// <param name="runUpFrames"></param>
        /// <param name="runDownFrames"></param>
        /// <param name="runLeftFrames"></param>
        /// <param name="runRightFrames"></param>
        /// <param name="spriteSheet"></param>
        public Hero(int posX, int posY, int idleFrames, int runUpFrames, int runDownFrames, int runLeftFrames, int runRightFrames, Image spriteSheet)
        {
            this.posX = posX;
            this.posY = posY;
            this.idleFrontFrames = idleFrames;
            this.runUpFrames = runUpFrames;
            this.runDownFrames = runDownFrames;
            this.runLeftFrames = runLeftFrames;
            this.runRightFrames = runRightFrames;
            this.spriteSheet = spriteSheet;
            sizeX = 60;
            sizeY = 140;
            currentAnimation = 0;
            currentFrame = 0;
            currentLimit = idleFrames;
        }

        /// <summary>
        /// Метод, отвечающий за смещение координаты персонажа
        /// </summary>
        public void Move()
        {          
            posX += dirX;
            posY += dirY;
        }

        /// <summary>
        /// Метод, отвечающий за отрисовку персонажа и анимации
        /// </summary>
        /// <param name="g"></param>
        /// <param name="scale"></param>
        public void PlayAnimation(Graphics g)
        {
            int scale = 4;

            if (currentFrame < idleFrontFrames - 1)
                currentFrame++;
            else currentFrame = 0;

            if (MapController.currentLVL == "Levels\\Maze.png" || MapController.currentLVL == "Levels\\MazePlank.png")
                scale = 1;

            g.DrawImage(spriteSheet, new Rectangle(new Point(posX, posY), new Size(sizeX * scale / 2, sizeY * scale / 2)), 60*currentFrame, 140*currentAnimation, sizeX, sizeY, GraphicsUnit.Pixel);
        }

        /// <summary>
        /// Метод, отвечающий за анимации передвижения
        /// </summary>
        /// <param name="currentAnimation"></param>
        public void SetAnimationConfiguration(int currentAnimation)
        {
            this.currentAnimation = currentAnimation;

            switch(currentAnimation)
            {
                case 0:
                    currentLimit = idleFrontFrames;
                    break;
                case 1:
                    currentLimit = runUpFrames;
                    break;
                case 2:
                    currentLimit = runDownFrames;
                    break;
                case 3:
                    currentLimit = runLeftFrames;
                    break;
                case 4:
                    currentLimit = runRightFrames;
                    break;
                case 5:
                    currentLimit = idleBackFrames;
                    break;
                case 6:
                    currentLimit = idleLeftFrames;
                    break;
                case 7:
                    currentLimit = idleRightFrames;
                    break;
            }
        }
    }
}
