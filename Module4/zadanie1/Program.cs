using System;

// Интерфейс фигуры
interface IFigure
{
    double Area();
    double Perimeter();
}

// Круг
class Circle : IFigure
{
    double r;

    public Circle(double r)
    {
        this.r = r;
    }

    public double Area()
    {
        return Math.PI * r * r;
    }

    public double Perimeter()
    {
        return 2 * Math.PI * r;
    }
}

// Прямоугольник
class Rectangle : IFigure
{
    double w, h;

    public Rectangle(double w, double h)
    {
        this.w = w;
        this.h = h;
    }

    public double Area()
    {
        return w * h;
    }

    public double Perimeter()
    {
        return 2 * (w + h);
    }
}

// Треугольник (по трем сторонам)
class Triangle : IFigure
{
    double a, b, c;

    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public double Area()
    {
        // Формула Герона
        double p = (a + b + c) / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public double Perimeter()
    {
        return a + b + c;
    }
}

class Program
{
    // Вывод площади и периметра
    static void Print(string name, IFigure figure)
    {
        Console.WriteLine(name + ": площадь = " + figure.Area().ToString("F2") + ", периметр = " + figure.Perimeter().ToString("F2"));
    }

    static void Main()
    {
        Print("Круг", new Circle(5));
        Print("Прямоугольник", new Rectangle(4, 6));
        Print("Треугольник", new Triangle(3, 4, 5));
    }
}