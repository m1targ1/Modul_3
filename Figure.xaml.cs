using System;
using System.Windows;

namespace Mod_3
{
    public partial class Figure : Window
    {
        public Figure()
        {
            InitializeComponent();
        }
        // Включение и отключение компонентов для работы
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (circle.IsChecked == true)
            {
                radius.IsEnabled = true; width.IsEnabled = false; length.IsEnabled = false; tria1.IsEnabled = false; tria2.IsEnabled = false; tria3.IsEnabled = false;
            }
            else if (Rectangle.IsChecked == true)
            {
                radius.IsEnabled = false; width.IsEnabled = true; length.IsEnabled = true; tria1.IsEnabled = false; tria2.IsEnabled = false; tria3.IsEnabled = false;
            }
            else if (Triangle.IsChecked == true)
            {
                radius.IsEnabled = false; width.IsEnabled = false; length.IsEnabled = false; tria1.IsEnabled = true; tria2.IsEnabled = true; tria3.IsEnabled = true;
            }
        }
        private void Result_Click(object sender, RoutedEventArgs e)
        {
            if (circle.IsChecked == true) // Проверка какая из радиокнопок выбрана
            {
                if (double.TryParse(radius.Text, out double radiuss)) // Проверка на корректность ввода
                {
                    Figures circless = new Circles(radiuss); // Создание новой фигуры типа круг
                    AreaDelegate areaDelegate = circless.CalculateArea; // Вычисление площади
                    MessageBox.Show($"Площадь круга: {areaDelegate()}"); // Вывод результата
                }
                else
                {
                    MessageBox.Show("Введите корректное значение радиуса."); // Вывод ошибки
                }
            }
            else if (Rectangle.IsChecked == true)
            {
                if (double.TryParse(width.Text, out double widtth) && double.TryParse(length.Text, out double lengtth))
                {
                    Figures rectangles = new Rectangles(widtth, lengtth); AreaDelegate areaDelegate = rectangles.CalculateArea; MessageBox.Show($"Площадь прямоугольника: {areaDelegate()}");
                }
                else
                {
                    MessageBox.Show("Введите корректные значения для длины и ширины.");
                }
            }
            else if (Triangle.IsChecked == true)
            {
                if (double.TryParse(tria1.Text, out double side1) && double.TryParse(tria2.Text, out double side2) && double.TryParse(tria3.Text, out double side3))
                {
                    Figures triangle = new Triangles(side1, side2, side3); AreaDelegate areaDelegate = triangle.CalculateArea; MessageBox.Show($"Площадь треугольника: {areaDelegate()}");
                }
                else
                {
                    MessageBox.Show("Введите корректные значения для всех сторон треугольника.");
                }
            }
            else
            {
                MessageBox.Show("Выберите фигуру.");
            }
        }
    }
    public delegate double AreaDelegate(); // Создание делегата
    public class Figures // Класс фигуры
    {
        public virtual double CalculateArea() => 0; // переменная, хранящая значение площади
    }
    public class Circles : Figures // Класс круга, наслкдующий от класса фигуры переменную
    {
        public double Radius { get; set; } // Переменная круга (радиус)
        public Circles(double radius) => Radius = radius;  // Конструктор, принимающий переменные
        public override double CalculateArea() => Math.PI * Radius * Radius; // Вычисление площади круга
    }
    public class Rectangles : Figures // Класс прямоугольника, наслкдующий от класса фигуры переменную
    {
        public double Width { get; set; }
        public double Height { get; set; } // Пееременные прямоугольника (длина и ширина)
        // Конструктор, принимающий переменные
        public Rectangles(double width, double height)
        {
            Width = width; Height = height;
        }
        public override double CalculateArea() => Width * Height; // Вычисление площади
    }
    public class Triangles : Figures // Класс треугольника, наслкдующий от класса фигуры переменную
    {
        // Переменные треугольника (три стороны и полупериметр)
        public double Triangle1 { get; set; }
        public double Triangle2 { get; set; }
        public double Triangle3 { get; set; }
        public double halfperimetr { get; set; }
        // Конструктор, принимающий и вычисляющий значения
        public Triangles(double triangle1, double triangle2, double triangle3)
        {
            Triangle1 = triangle1; Triangle2 = triangle2; Triangle3 = triangle3; halfperimetr = (triangle1 + triangle2 + triangle3) / 2;
        }
        public override double CalculateArea() => Math.Sqrt(halfperimetr * (halfperimetr - Triangle1) * (halfperimetr - Triangle2) * (halfperimetr - Triangle3)); // Вычисление площади треугольника
    }

}
