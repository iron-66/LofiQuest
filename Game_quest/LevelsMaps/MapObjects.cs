using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_quest.Entities
{
    /// <summary>
    /// Параметры объектов, расположенных на карте
    /// </summary>
    public class MapObjects
    {
        public PointF position;
        public Size size;

        /// <summary>
        /// Конструктор объектов класса MapObjects
        /// </summary>
        /// <param name="pos"> Позиция объекта </param>
        /// <param name="size"> Размер объекта </param>
        public MapObjects(PointF pos, Size size)
        {
            this.position = pos;
            this.size = size;
        }
    }
}
