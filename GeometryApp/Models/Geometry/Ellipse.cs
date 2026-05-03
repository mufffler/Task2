using System;
using System.Windows;

namespace GeometryApp.Models.Geometry
{
    /// <summary>
    /// Класс эллипса (наследник Shape)
    /// </summary>
    public class Ellipse : Shape
    {
        private double radiusX;
        private double radiusY;

        /// <summary>
        /// Горизонтальный радиус эллипса
        /// </summary>
        public double RadiusX
        {
            get => radiusX;
            set => radiusX = value > 0 ? value : 0;
        }

        /// <summary>
        /// Вертикальный радиус эллипса
        /// </summary>
        public double RadiusY
        {
            get => radiusY;
            set => radiusY = value > 0 ? value : 0;
        }

        /// <summary>
        /// Диаметр по горизонтали
        /// </summary>
        public double DiameterX => RadiusX * 2;

        /// <summary>
        /// Диаметр по вертикали
        /// </summary>
        public double DiameterY => RadiusY * 2;

        /// <summary>
        /// Конструктор эллипса
        /// </summary>
        /// <param name="center">Центр эллипса</param>
        /// <param name="radiusX">Горизонтальный радиус</param>
        /// <param name="radiusY">Вертикальный радиус</param>
        public Ellipse(Point center, double radiusX, double radiusY) : base(center)
        {
            this.radiusX = radiusX > 0 ? radiusX : 0;
            this.radiusY = radiusY > 0 ? radiusY : 0;
        }

        /// <summary>
        /// Площадь эллипса: π * a * b
        /// </summary>
        public override double GetArea()
        {
            return Math.PI * RadiusX * RadiusY;
        }

        /// <summary>
        /// Прямоугольник, описывающий эллипс
        /// </summary>
        public override Rect GetBoundingBox()
        {
            return new Rect(
                Center.X - RadiusX,
                Center.Y - RadiusY,
                DiameterX,
                DiameterY
            );
        }

        /// <summary>
        /// Переопределение названия фигуры
        /// </summary>
        public override string GetShapeType()
        {
            if (Math.Abs(RadiusX - RadiusY) < 0.001)
                return "Окружность";
            return "Эллипс";
        }

        /// <summary>
        /// Переопределение ToString
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + $", Радиусы: {RadiusX:F1} × {RadiusY:F1}";
        }
    }
}