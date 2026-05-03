using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace GeometryApp.Models.Geometry
{
    /// <summary>
    /// Класс многоугольника (наследник Shape)
    /// </summary>
    public class Polygon : Shape
    {
        private ObservableCollection<Point> vertices;

        /// <summary>
        /// Список вершин многоугольника
        /// </summary>
        public ObservableCollection<Point> Vertices
        {
            get => vertices;
            set => vertices = value ?? new ObservableCollection<Point>();
        }

        /// <summary>
        /// Количество вершин
        /// </summary>
        public int VertexCount => vertices?.Count ?? 0;

        /// <summary>
        /// Конструктор многоугольника
        /// </summary>
        /// <param name="center">Центр многоугольника (центроид)</param>
        /// <param name="vertices">Список вершин</param>
        public Polygon(Point center, IEnumerable<Point> vertices) : base(center)
        {
            this.vertices = new ObservableCollection<Point>(vertices ?? new List<Point>());
            UpdateCenter();
        }

        /// <summary>
        /// Обновление центра на основе центроида вершин
        /// </summary>
        private void UpdateCenter()
        {
            if (vertices == null || vertices.Count == 0)
                return;

            double sumX = vertices.Sum(v => v.X);
            double sumY = vertices.Sum(v => v.Y);
            Center = new Point(sumX / vertices.Count, sumY / vertices.Count);
        }

        /// <summary>
        /// Добавление вершины
        /// </summary>
        public void AddVertex(Point vertex)
        {
            vertices.Add(vertex);
            UpdateCenter();
        }

        /// <summary>
        /// Площадь многоугольника (по формуле Гаусса)
        /// </summary>
        public override double GetArea()
        {
            if (vertices == null || vertices.Count < 3)
                return 0;

            double area = 0;
            int n = vertices.Count;

            for (int i = 0; i < n; i++)
            {
                int j = (i + 1) % n;
                area += vertices[i].X * vertices[j].Y;
                area -= vertices[j].X * vertices[i].Y;
            }

            return Math.Abs(area) / 2;
        }

        /// <summary>
        /// Прямоугольник, описывающий многоугольник
        /// </summary>
        public override Rect GetBoundingBox()
        {
            if (vertices == null || vertices.Count == 0)
                return new Rect(Center, new Size(0, 0));

            double minX = vertices.Min(v => v.X);
            double minY = vertices.Min(v => v.Y);
            double maxX = vertices.Max(v => v.X);
            double maxY = vertices.Max(v => v.Y);

            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// Переопределение названия фигуры
        /// </summary>
        public override string GetShapeType()
        {
            if (vertices == null) return "Многоугольник";

            switch (vertices.Count)
            {
                case 3: return "Треугольник";
                case 4: return "Четырёхугольник";
                case 5: return "Пятиугольник";
                case 6: return "Шестиугольник";
                default: return $"{vertices.Count}-угольник";
            }
        }
    }
}