using Game_quest.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_quest.Controllers
{
    /// <summary>
    /// Класс, отвечающий за отрисовку карты и обработку карты нормалей
    /// </summary>
    public static class MapController
    {
        public static int mapHeight = 6;
        public static int mapWidth = 10;
        public static int cellSize = 120;
        public static int[,] map;
        public static List<MapObjects> mapObjects;
        public static string currentLVL;
        public static Form1 formEditor;

        /// <summary>
        /// Инициализация;
        /// Действия, которые необходимо выполнить при вызове данного метода
        /// </summary>
        /// <param name="lvl"> Название уровня </param>
        public static void Init(string lvl)
        {
            map = GetObjectsMap(lvl);
            DrawBackground(lvl);
            mapObjects = new List<MapObjects>();
        }

        /// <summary>
        /// Метод, содержащий карты расположений объектов
        /// </summary>
        /// <param name="lvl"> Название уровня </param>
        /// <returns> Двумерный массив - карта нормалей </returns>
        public static int[,] GetObjectsMap(string lvl)
        {
            map = new int[mapHeight, mapWidth];
            currentLVL = lvl;

            switch (lvl)
            {
                default:
                    return new int[,] 
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 0, 0, 0, 0, 0, 0, 1, 1 },
                        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    };
               
                case ("Levels\\Yard.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                        { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                    };

                case ("Levels\\YardGate.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                        { 1, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                        { 1, 1, 1, 1, 0, 0, 1, 1, 1, 1 },
                    };

                case ("Levels\\Tree.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                    };

                case ("Levels\\GroundWorks.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 0, 0, 0, 1, 1, 0, 0, 0 },
                        { 1, 1, 0, 0, 0, 1, 1, 0, 0, 0 },
                        { 1, 1, 0, 0, 0, 1, 1, 0, 0, 0 },
                        { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                    };

                case ("Levels\\GroundBridge.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 0, 0, 0, 1, 1, 0, 0, 0 },
                        { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
                        { 1, 1, 0, 0, 0, 1, 1, 0, 0, 0 },
                        { 1, 1, 1, 1, 1, 1, 1, 0, 0, 0 },
                    };

                case ("Levels\\SecurityRoom.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    };

                case ("Levels\\SecurityRoomGreen.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    };

                case ("Levels\\SecurityElectro.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    };

                case ("Levels\\Stock.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    };

                case ("Levels\\StockOff.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 0, 0, 0, 0, 0, 0, 0, 1, 1 },
                        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                    };

                case ("Levels\\StockGame.png"):
                    return new int[,]
                    {
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
                        { 1, 1, 1, 1, 1, 1, 0, 1, 1, 1 },
                    };

                case ("Levels\\Maze.png"):
                    return new int[,]
                    {
                        { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                        { 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1 },
                        { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 0, 1 },
                        { 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1 },
                        { 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1 },
                        { 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1 },
                        { 1, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
                        { 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
                        { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    };

                case ("Levels\\MazePlank.png"):
                    return new int[,]
                    {
                        { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                        { 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
                        { 1, 1, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1, 0, 1 },
                        { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
                        { 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 1, 0, 1 },
                        { 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 1 },
                        { 1, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 0, 1, 1 },
                        { 1, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1 },
                        { 1, 0, 1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
                        { 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 1 },
                        { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1 },
                        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    };
            }
        }

        /// <summary>
        /// Отрисовка изображения локации
        /// </summary>
        /// <param name="lvl"> Название уровня </param>
        public static void DrawBackground(string lvl)
        {
            formEditor.BackgroundImage = Image.FromFile(Path.Combine(new DirectoryInfo(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString(), lvl));
        }

        /// <summary>
        /// Перебор всех объектов на карте и добавление их в список объектов
        /// </summary>
        public static void GetCollision()
        {
            for (int i = 0; i < mapHeight; i++)
                for (int j = 0; j < mapWidth; j++)
                    if (map[i, j] == 1)
                    {
                        MapObjects mapEntity = new MapObjects(new Point(j * cellSize, i * cellSize), new Size(cellSize, cellSize));
                        mapObjects.Add(mapEntity);
                    }
        }

        /// <summary>
        /// Получение ширины карты
        /// </summary>
        /// <returns> Ширина карты </returns>
        public static int GetWidth()
        {
            return cellSize * mapWidth;
        }

        /// <summary>
        /// Получение длины карты
        /// </summary>
        /// <returns> Длина карты </returns>
        public static int GetHeight()
        {
            return cellSize * mapHeight;
        }
    }
}