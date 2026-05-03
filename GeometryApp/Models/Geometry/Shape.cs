using System.Windows;

namespace GeometryApp.Models.Geometry
{
    /// <summary>
    /// Абстрактный базовый класс геометрической фигуры
    /// </summary>
    public abstract class Shape
    {
        private Point center;

        /// <summary>
        /// Координаты центра фигуры
        /// </summary>
        public Point Center
        {
            get => center;
            set => center = value;
        }

        /// <summary>
        /// Конструктор базового класса
        /// </summary>
        /// <param name="center">Координаты центра фигуры</param>
        protected Shape(Point center)
        {
            this.center = center;
        }

        /// <summary>
        /// Абстрактный метод: возвращает площадь фигуры
        /// </summary>
        public abstract double GetArea();

        /// <summary>
        /// Абстрактный метод: возвращает прямоугольник, в который заключена фигура
        /// </summary>
        public abstract Rect GetBoundingBox();

        /// <summary>
        /// Виртуальный метод: возвращает название фигуры (может быть переопределён)
        /// </summary>
        public virtual string GetShapeType()
        {
            return "Геометрическая фигура";
        }

        /// <summary>
        /// Переопределение ToString для отображения информации о фигуре
        /// </summary>
        public override string ToString()
        {
            return $"{GetShapeType()}: Центр({Center.X:F1}, {Center.Y:F1}), " +
                   $"Площадь = {GetArea():F2}, " +
                   $"Габариты = {GetBoundingBox().Width:F1}×{GetBoundingBox().Height:F1}";
        }
    }
}