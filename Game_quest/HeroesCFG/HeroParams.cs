namespace Game_quest.Models
{
    /// <summary>
    /// Параметры персонажа
    /// </summary>
    public static class HeroParams
    {
        // Параметры анимации персонажа
        public static int totalFrames = 2; // Макс. кол-во кадров для анимации
        public static int runDownFrames = 2; // Кол-во кадров анимации бега вниз
        public static int runUpFrames = 2; // Кол-во кадров анимации бега вверх
        public static int runLeftFrames = 2; // Кол-во кадров анимации бега влево
        public static int runRightFrames = 2; // Кол-во кадров анимации бега вправо

        // Инвентарь персонажа и совершённые им действия
        public static bool firstActionDone = false;
        public static bool havePlank = false;
        public static bool plankIsPlaced = false;
        public static bool haveKey = false;
        public static bool gateIsOpen = false;
        public static bool visitYard = false;
        public static bool canVisitStock = false;
        public static bool generatorDisabled = false;
    }
}
