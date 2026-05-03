using System;
using System.Windows;

namespace GeometryApp.Models.Geometry
{
    /// <summary>
    /// Класс линии (наследник Shape)
    /// </summary>
    public class Line : Shape
    {
        private Point endPoint;

        /// <summary>
        /// Конечная точка линии
        /// </summary>
        public Point EndPoint
        {
            get => endPoint;
            set => endPoint = value;
        }

        /// <summary>
        /// Длина линии (вычисляемое свойство)
        /// </summary>
        public double Length => Math.Sqrt(
            Math.Pow(EndPoint.X - Center.X, 2) +
            Math.Pow(EndPoint.Y - Center.Y, 2)
        );

        /// <summary>
        /// Конструктор линии
        /// </summary>
        /// <param name="startPoint">Начальная точка (центр)</param>
        /// <param name="endPoint">Конечная точка</param>
        public Line(Point startPoint, Point endPoint) : base(startPoint)
        {
            this.endPoint = endPoint;
        }

        /// <summary>
        /// Площадь линии равна 0
        /// </summary>
        public override double GetArea()
        {
            return 0;
        }

        /// <summary>
        /// Прямоугольник, описывающий линию
        /// </summary>
        public override Rect GetBoundingBox()
        {
            double minX = Math.Min(Center.X, EndPoint.X);
            double minY = Math.Min(Center.Y, EndPoint.Y);
            double maxX = Math.Max(Center.X, EndPoint.X);
            double maxY = Math.Max(Center.Y, EndPoint.Y);

            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// Переопределение названия фигуры
        /// </summary>
        public override string GetShapeType()
        {
            return "Линия";
        }

        /// <summary>
        /// Переопределение ToString для отображения дополнительной информации
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $", Длина = {Length:F2}";
        }
    }
}