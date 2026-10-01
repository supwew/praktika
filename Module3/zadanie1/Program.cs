using System;

namespace GeometryExample
{
    // Определение делегата для вычисления площади
    public delegate double CalculateAreaDelegate();

    // Базовый абстрактный класс "Фигура"
    public abstract class Figure
    {
        // Название фигуры
        public string Name { get; protected set; }

        protected Figure(string name)
        {
            Name = name;
        }

        // Абстрактный метод для вычисления площади
        public abstract double GetArea();
    }

    // Производный класс "Круг"
    public class Circle : Figure
    {
        // Радиус круга
        public double Radius { get; set; }

        public Circle(double radius) : base("Круг")
        {
            Radius = radius;
        }

        // Переопределение метода вычисления площади
        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    // Производный класс "Прямоугольник"
    public class Rectangle : Figure
    {
        // Ширина прямоугольника
        public double Width { get; set; }

        // Высота прямоугольника
        public double Height { get; set; }

        public Rectangle(double width, double height) : base("Прямоугольник")
        {
            Width = width;
            Height = height;
        }

        // Переопределение метода вычисления площади
        public override double GetArea()
        {
            return Width * Height;
        }
    }

    // Производный класс "Треугольник"
    public class Triangle : Figure
    {
        // Основание треугольника
        public double BaseLength { get; set; }

        // Высота треугольника
        public double Height { get; set; }

        public Triangle(double baseLength, double height) : base("Треугольник")
        {
            BaseLength = baseLength;
            Height = height;
        }

        // Переопределение метода вычисления площади
        public override double GetArea()
        {
            return 0.5 * BaseLength * Height;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создание экземпляров геометрических фигур
            Figure circle = new Circle(5.0);
            Figure rectangle = new Rectangle(4.0, 6.0);
            Figure triangle = new Triangle(3.0, 8.0);

            // Массив фигур для обхода
            Figure[] figures = new Figure[] { circle, rectangle, triangle };

            Console.WriteLine("--- Динамический вызов вычисления площади через делегат ---\n");

            foreach (var figure in figures)
            {
                // Привязка метода GetArea конкретного объекта к делегату
                CalculateAreaDelegate areaCalculator = figure.GetArea;

                // Вызов метода через делегат
                double area = areaCalculator();

                // Вывод результата в консоль
                Console.WriteLine($"Фигура: {figure.Name}, Площадь: {area:F2}");
            }
        }
    }
}