using LofiQuest.Entities;
using LofiQuest.HeroesCFG;
using LofiQuest.Models;
using System;
using System.Drawing;

namespace LofiQuest.Controllers
{
    /// <summary>
    /// Обработка "физики" программы
    /// </summary>
    public static class PhysicsController
    {
        public static Graphics g;

        /// <summary>
        /// Метод, обрабатывающий cтолкновения с объектами на карте
        /// </summary>
        /// <param name="entity"> Переменная типа Hero - персонаж </param>
        /// <param name="dir"> Направление движения персонажа </param>
        /// <returns>
        /// True - персонаж столкнулся с объектом, движение запрещено;
        /// False - персонаж не столкнулся с объектом, движение разрешено
        /// </returns>
        public static bool IsCollide(Hero entity, Point dir)
        {
            int xTolerance = 120;
            int yTolerance = 60;
            var length = entity.sizeX;
            var heigth = entity.sizeY;

            if (MapController.currentLVL == "Levels\\Maze.png" || MapController.currentLVL == "Levels\\MazePlank.png")
            {
                if (entity.posX + dir.X <= 0 || entity.posX + dir.X >= (MapController.cellSize * MapController.mapWidth - 20) || entity.posY + dir.Y <= 0 || entity.posY + dir.Y >= (MapController.cellSize * MapController.mapHeight - 60))
                    return true;

                length = 12;
                heigth = 28;
                xTolerance = 48;
                yTolerance = 53;
            }
            else if (entity.posX + dir.X <= 0 || entity.posX + dir.X >= MapController.cellSize * (MapController.mapWidth - 1) || entity.posY + dir.Y <= 0 || entity.posY + dir.Y >= MapController.cellSize * (MapController.mapHeight - 2.3))
                return true;     
            else
            {
                length = entity.sizeX;
                heigth = entity.sizeY;
                xTolerance = 120;
                yTolerance = 60;
            }

            ChangeLocation(entity);

            for (int i = 0; i < MapController.mapObjects.Count; i++)
            {
                var currObject = MapController.mapObjects[i];
                PointF delta = new PointF();             

                delta.X = (entity.posX + length) - (currObject.position.X + currObject.size.Width / 2);
                delta.Y = (entity.posY + 2 * heigth) - (currObject.position.Y + currObject.size.Height / 2);               

                if (Math.Abs(delta.X) <= xTolerance)
                {
                    if (Math.Abs(delta.Y) <= yTolerance)
                    {
                        if (delta.X < 0 && dir.X == 12 && currObject.position.Y < entity.posY + 2 * heigth && currObject.position.Y + currObject.size.Height > entity.posY + 2 * heigth)
                        {
                            return true;
                        }
                        if (delta.X > 0 && dir.X == -12 && currObject.position.Y < entity.posY + 2 * heigth && currObject.position.Y + currObject.size.Height > entity.posY + 2 * heigth)
                        {
                            return true;
                        }
                        if (delta.Y < 0 && dir.Y == 12 && currObject.position.X < entity.posX + length && currObject.position.X + currObject.size.Width > entity.posX + length)
                        {
                            return true;
                        }
                        if (delta.Y > 0 && dir.Y == -12 && currObject.position.X < entity.posX + length && currObject.position.X + currObject.size.Width > entity.posX + length)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Метод, проверяющий условия при которых меняется локация
        /// </summary>
        /// <param name="entity"> Переменная типа Hero - персонаж </param>
        static void ChangeLocation(Hero entity)
        {
            if ((MapController.currentLVL == "Levels\\Room.png") && !HeroParams.gateIsOpen && (entity.posX > 1010))
            {
                MapController.Init("Levels\\Yard.png");
                DialogController.FirstActionDone = true;
                DialogController.FirstTimeOnLevel = true;
                entity.posX = 300;
                entity.posY = 20;
            }

            if ((MapController.currentLVL == "Levels\\Room.png") && HeroParams.gateIsOpen && (entity.posX > 1010))
            {
                MapController.Init("Levels\\YardGate.png");
                entity.posX = 300;
                entity.posY = 20;
            }

            if ((MapController.currentLVL == "Levels\\Yard.png" || MapController.currentLVL == "Levels\\YardGate.png") && (entity.posX < 200) && (entity.posY < 60))
            {
                MapController.Init("Levels\\Room.png");
                entity.posX = 1000;
                entity.posY = 410;
            }

            if ((MapController.currentLVL == "Levels\\Yard.png") && (!HeroParams.havePlank) && (entity.posX < 100) && (entity.posY > 100))
            {
                DialogController.FirstTimeOnLevel = true;
                MapController.Init("Levels\\GroundWorks.png");
                entity.posX = 1000;
                entity.posY = 410;
            }

            if ((MapController.currentLVL == "Levels\\Yard.png" || MapController.currentLVL == "Levels\\YardGate.png") && (HeroParams.havePlank) && (entity.posX < 100) && (entity.posY > 100))
            {
                if (!HeroParams.haveKey)
                    DialogController.FirstTimeOnLevel = true;
                else DialogController.FirstTimeOnLevel = false;
                MapController.Init("Levels\\GroundBridge.png");

                if(HeroParams.generatorDisabled)
                    MapController.Init("Levels\\GroundFinal.png");

                entity.posX = 1000;
                entity.posY = 410;
            }

            if ((MapController.currentLVL == "Levels\\Yard.png") && !HeroParams.havePlank && (entity.posY > 400))
            {
                MapController.mapHeight = 12;
                MapController.mapWidth = 20;
                MapController.cellSize = 60;
                MapController.Init("Levels\\MazePlank.png");
                entity.posX = 70;
                entity.posY = 40;
            }

            if ((MapController.currentLVL == "Levels\\Yard.png" || MapController.currentLVL == "Levels\\YardGate.png") && HeroParams.havePlank && (entity.posY > 400))
            {
                MapController.mapHeight = 12;
                MapController.mapWidth = 20;
                MapController.cellSize = 60;
                DialogController.FirstTimeOnLevel = false;
                MapController.Init("Levels\\Maze.png");
                entity.posX = 70;
                entity.posY = 40;
            }

            if ((MapController.currentLVL == "Levels\\MazePlank.png") && (entity.posY > 400) && (entity.posX > 900))
            {
                HeroParams.havePlank = true;
                DialogController.FirstTimeOnLevel = true;
                MapController.Init("Levels\\Maze.png");
            }

            if ((MapController.currentLVL == "Levels\\Maze.png" || MapController.currentLVL == "Levels\\MazePlank.png") && (entity.posY < 40) && (entity.posX < 100))
            {
                MapController.mapHeight = 6;
                MapController.mapWidth = 10;
                MapController.cellSize = 120;
                if (!HeroParams.gateIsOpen)
                    MapController.Init("Levels\\Yard.png");
                else MapController.Init("Levels\\YardGate.png");
                entity.posX = 570;
                entity.posY = 340;
            }

            if ((MapController.currentLVL == "Levels\\GroundBridge.png") && (entity.posX < 400))
            {
                if (HeroParams.visitTree)
                    HeroParams.canVisitStock = true;
                if (HeroParams.generatorDisabled)
                    DialogController.ShowScene("End");
            }

            if ((MapController.currentLVL == "Levels\\GroundWorks.png" || MapController.currentLVL == "Levels\\GroundBridge.png") && !HeroParams.gateIsOpen && (entity.posX > 1000))
            {
                Worker.Deactivate();
                DialogController.FirstTimeOnLevel = true;
                MapController.Init("Levels\\Yard.png");
                entity.posX = 100;
                entity.posY = 200;
            }

            if ((MapController.currentLVL == "Levels\\GroundBridge.png") && HeroParams.gateIsOpen && (entity.posX > 1000))
            {
                Worker.Deactivate();
                MapController.Init("Levels\\YardGate.png");
                entity.posX = 100;
                entity.posY = 200;
            }

            if ((MapController.currentLVL == "Levels\\YardGate.png") && HeroParams.gateIsOpen && (entity.posX > 480) && (entity.posX < 600) && (entity.posY < 30))
            {
                MapController.Init("Levels\\Tree.png");
                HeroParams.visitTree = true;
                entity.posX = 190;
                entity.posY = 410;
            }

            if ((MapController.currentLVL == "Levels\\Tree.png") && (entity.posX < 120))
            {
                MapController.Init("Levels\\YardGate.png");
                entity.posX = 480;
                entity.posY = 10;
            }

            if ((MapController.currentLVL == "Levels\\Yard.png") && HeroParams.haveKey && (entity.posX > 900) && (entity.posY < 80))
            {
                DialogController.FirstTimeOnLevel = true;
                MapController.Init("Levels\\SecurityRoom.png");
                entity.posX = 350;
                entity.posY = 400;
            }

            if ((MapController.currentLVL == "Levels\\YardGate.png") && (entity.posX > 900) && (entity.posY < 80))
            {
                MapController.Init("Levels\\SecurityRoomGreen.png");
                entity.posX = 350;
                entity.posY = 400;
            }           

            if ((MapController.currentLVL == "Levels\\SecurityRoom.png" || MapController.currentLVL == "Levels\\SecurityElectro.png") && (entity.posX < 110) && (entity.posY > 360) && !HeroParams.gateIsOpen)
            {
                CabelsGame.HideElements();
                MapController.Init("Levels\\Yard.png");
                entity.posX = 900;
                entity.posY = 120;
            }

            if ((MapController.currentLVL == "Levels\\SecurityRoom.png" || MapController.currentLVL == "Levels\\SecurityRoomGreen.png") && (entity.posX < 110) && (entity.posY > 360) && HeroParams.gateIsOpen)
            {
                CabelsGame.HideElements();
                MapController.Init("Levels\\YardGate.png");
                entity.posX = 900;
                entity.posY = 120;
            }

            if ((MapController.currentLVL == "Levels\\SecurityRoom.png") && (entity.posX < 250) && (entity.posY < 230) && (Hero.canInteract))
            {
                MapController.Init("Levels\\SecurityElectro.png");
            }

            if ((MapController.currentLVL == "Levels\\YardGate.png") && (entity.posX > 1050) && (entity.posY > 100) && HeroParams.canVisitStock)
            {
                MapController.Init("Levels\\Stock.png");
                entity.posX = 350;
                entity.posY = 400;
            }

            if ((MapController.currentLVL == "Levels\\Stock.png") && (entity.posX > 700) && (entity.posX < 850) && (entity.posY > 120) && (entity.posY < 250) && (Hero.canInteract))
            {
                MapController.Init("Levels\\StockGame.png");
            }

            if ((MapController.currentLVL == "Levels\\Stock.png" || MapController.currentLVL == "Levels\\StockOff.png") && (entity.posX < 110) && (entity.posY > 360) && HeroParams.gateIsOpen)
            {
                CabelsGame.HideElements();
                MapController.Init("Levels\\YardGate.png");
                entity.posX = 940;
                entity.posY = 200;
            }
        }
    }
}