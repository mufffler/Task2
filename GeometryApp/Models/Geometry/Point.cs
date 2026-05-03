using System.Windows;

namespace GeometryApp.Models.Geometry
{
    /// <summary>
    /// Класс точки (наследник Shape)
    /// </summary>
    public class Point : Shape
    {
        /// <summary>
        /// Конструктор точки
        /// </summary>
        /// <param name="center">Координаты точки</param>
        public Point(Point center) : base(center)
        {
        }

        /// <summary>
        /// Площадь точки равна 0
        /// </summary>
        public override double GetArea()
        {
            return 0;
        }

        /// <summary>
        /// Прямоугольник для точки имеет нулевые размеры
        /// </summary>
        public override Rect GetBoundingBox()
        {
            return new Rect(Center, new Size(0, 0));
        }

        /// <summary>
        /// Переопределение названия фигуры
        /// </summary>
        public override string GetShapeType()
        {
            return "Точка";
        }
    }
}