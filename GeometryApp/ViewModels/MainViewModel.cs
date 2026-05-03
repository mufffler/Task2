using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using GeometryApp.Models.Geometry;

namespace GeometryApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Shape> shapes;
        private Shape selectedShape;
        private string resultMessage;

        // Параметры для создания фигур
        private double centerX;
        private double centerY;
        private double param1;
        private double param2;
        private string selectedShapeType;

        public ObservableCollection<Shape> Shapes
        {
            get => shapes;
            set
            {
                shapes = value;
                OnPropertyChanged();
            }
        }

        public Shape SelectedShape
        {
            get => selectedShape;
            set
            {
                selectedShape = value;
                OnPropertyChanged();
                if (value != null)
                {
                    ResultMessage = value.ToString();
                }
            }
        }

        public string ResultMessage
        {
            get => resultMessage;
            set
            {
                resultMessage = value;
                OnPropertyChanged();
            }
        }

        public double CenterX
        {
            get => centerX;
            set
            {
                centerX = value;
                OnPropertyChanged();
            }
        }

        public double CenterY
        {
            get => centerY;
            set
            {
                centerY = value;
                OnPropertyChanged();
            }
        }

        public double Param1
        {
            get => param1;
            set
            {
                param1 = value;
                OnPropertyChanged();
            }
        }

        public double Param2
        {
            get => param2;
            set
            {
                param2 = value;
                OnPropertyChanged();
            }
        }

        public string SelectedShapeType
        {
            get => selectedShapeType;
            set
            {
                selectedShapeType = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsPointOrLine));
                OnPropertyChanged(nameof(IsEllipse));
                OnPropertyChanged(nameof(IsPolygon));
            }
        }

        // Свойства для видимости UI элементов
        public bool IsPointOrLine => SelectedShapeType == "Точка" || SelectedShapeType == "Линия";
        public bool IsEllipse => SelectedShapeType == "Эллипс";
        public bool IsPolygon => SelectedShapeType == "Многоугольник";

        public ICommand AddShapeCommand { get; }
        public ICommand ClearShapesCommand { get; }
        public ICommand ShowBoundingBoxCommand { get; }
        public ICommand ShowAreaCommand { get; }

        public MainViewModel()
        {
            Shapes = new ObservableCollection<Shape>();
            
            // Значения по умолчанию
            CenterX = 100;
            CenterY = 100;
            Param1 = 50;
            Param2 = 30;
            SelectedShapeType = "Точка";

            AddShapeCommand = new RelayCommand(AddShape, CanAddShape);
            ClearShapesCommand = new RelayCommand(ClearShapes, CanClearShapes);
            ShowBoundingBoxCommand = new RelayCommand(ShowBoundingBox, CanShowShapeInfo);
            ShowAreaCommand = new RelayCommand(ShowArea, CanShowShapeInfo);
        }

        private void AddShape()
        {
            try
            {
                Shape newShape = null;
                Point center = new Point(CenterX, CenterY);

                switch (SelectedShapeType)
                {
                    case "Точка":
                        newShape = new Point(center);
                        break;

                    case "Линия":
                        Point endPoint = new Point(CenterX + Param1, CenterY + Param2);
                        newShape = new Line(center, endPoint);
                        break;

                    case "Эллипс":
                        newShape = new Ellipse(center, Math.Abs(Param1), Math.Abs(Param2));
                        break;

                    case "Многоугольник":
                        var vertices = new System.Collections.Generic.List<Point>
                        {
                            new Point(CenterX - Param1, CenterY - Param2),
                            new Point(CenterX + Param1, CenterY - Param2),
                            new Point(CenterX + Param1, CenterY + Param2),
                            new Point(CenterX - Param1, CenterY + Param2)
                        };
                        newShape = new Polygon(center, vertices);
                        break;
                }

                if (newShape != null)
                {
                    Shapes.Add(newShape);
                    ResultMessage = $"Добавлена фигура: {newShape}";
                }
            }
            catch (Exception ex)
            {
                ResultMessage = $"Ошибка: {ex.Message}";
            }
        }

        private bool CanAddShape()
        {
            return true;
        }

        private void ClearShapes()
        {
            Shapes.Clear();
            ResultMessage = "Список фигур очищен";
        }

        private bool CanClearShapes()
        {
            return Shapes.Count > 0;
        }

        private void ShowBoundingBox()
        {
            if (SelectedShape != null)
            {
                var box = SelectedShape.GetBoundingBox();
                ResultMessage = $"Габаритный прямоугольник: X={box.X:F1}, Y={box.Y:F1}, " +
                               $"Ширина={box.Width:F1}, Высота={box.Height:F1}";
            }
        }

        private void ShowArea()
        {
            if (SelectedShape != null)
            {
                double area = SelectedShape.GetArea();
                ResultMessage = $"Площадь фигуры = {area:F2} кв. ед.";
            }
        }

        private bool CanShowShapeInfo()
        {
            return SelectedShape != null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    // Команда RelayCommand (та же, что в предыдущем проекте)
    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute();
        }

        public void Execute(object parameter)
        {
            execute();
        }
    }
}