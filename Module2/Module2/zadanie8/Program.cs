using System;
abstract class Shape
{
    // Площадь и периметр (реализуются в производных классах)
    public abstract double Area();
    public abstract double Perimeter();

    // Вывод информации о фигуре
    public void Print(string name)
    {
        Console.WriteLine(name + ": площадь = " + Area().ToString("F2") + ", периметр = " + Perimeter().ToString("F2"));
    }
}

// Круг
class Circle : Shape
{
    double r;
    public Circle(double r)
    {
        this.r = r;
    }
    public override double Area()
    {
        return Math.PI * r * r;
    }
    public override double Perimeter()
    {
        return 2 * Math.PI * r;
    }
}
// Прямоугольник
class Rectangle : Shape
{
    double w, h;
    public Rectangle(double w, double h)
    {
        this.w = w;
        this.h = h;
    }
    public override double Area()
    {
        return w * h;
    }

    public override double Perimeter()
    {
        return 2 * (w + h);
    }
}
// Треугольник (по трем сторонам)
class Triangle : Shape
{
    double a, b, c;
    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }
    public override double Area()
    {
        // Формула Герона
        double p = (a + b + c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public override double Perimeter()
    {
        return a + b + c;
    }
}

class Program
{
    // Ввод положительного числа с проверкой
    static double Read(string text)
    {
        Console.Write(text);
        double x;
        while (!double.TryParse(Console.ReadLine(), out x) || x <= 0)
        {
            Console.Write("Нужно число больше 0. Повторите ввод: ");
        }
        return x;
    }

    static void Main()
    {
        // Ввод размеров круга
        Circle circle = new Circle(Read("Радиус круга: "));

        // Ввод размеров прямоугольника
        Rectangle rectangle = new Rectangle(Read("Ширина прямоугольника: "), Read("Высота прямоугольника: "));

        // Ввод сторон треугольника, пока такой треугольник не существует
        double a, b, c;
        while (true)
        {
            a = Read("Сторона треугольника a: ");
            b = Read("Сторона треугольника b: ");
            c = Read("Сторона треугольника c: ");

            // Сумма любых двух сторон должна быть больше третьей
            if (a + b > c && a + c > b && b + c > a)
            {
                break;
            }
            Console.WriteLine("Такого треугольника не существует. Повторите ввод.");
        }
        Triangle triangle = new Triangle(a, b, c);

        // Вывод площади и периметра
        Console.WriteLine();
        circle.Print("Круг");
        rectangle.Print("Прямоугольник");
        triangle.Print("Треугольник");
    }
}