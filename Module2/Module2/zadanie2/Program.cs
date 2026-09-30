using System;

// Базовый класс фигуры
class Shape
{
    public virtual double Area() => 0;
    public virtual double Perimeter() => 0;
}

// Круг
class Circle : Shape
{
    double r;
    public Circle(double r) { this.r = r; }
    public override double Area() => Math.PI * r * r;
    public override double Perimeter() => 2 * Math.PI * r;
}

// Прямоугольник
class Rectangle : Shape
{
    double w, h;
    public Rectangle(double w, double h) { this.w = w; this.h = h; }
    public override double Area() => w * h;
    public override double Perimeter() => 2 * (w + h);
}
class Program
{
    // Ввод положительного числа
    static double Read(string text)
    {
        Console.Write(text);
        double x;
        while (!double.TryParse(Console.ReadLine(), out x) || x <= 0)
            Console.Write("Нужно число больше 0: ");
        return x;
    }

    // Вывод площади и периметра
    static void Print(string name, Shape s)
    {
        Console.WriteLine(name + ": площадь " + s.Area().ToString("F2") + ", периметр " + s.Perimeter().ToString("F2"));
    }

    static void Main()
    {
        // Ввод данных и создание объектов
        Circle circle = new Circle(Read("Радиус круга: "));
        Rectangle rect = new Rectangle(Read("Ширина: "), Read("Высота: "));

        // Вывод результатов
        Print("Фигура", new Shape());
        Print("Круг", circle);
        Print("Прямоугольник", rect);
    }
}