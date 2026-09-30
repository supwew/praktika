using System;

// Интерфейс рисуемого объекта
interface IDrawable
{
    void Draw();
}

// Круг
class Circle : IDrawable
{
    double radius;
    public Circle(double radius)
    {
        this.radius = radius;
    }

    public void Draw()
    {
        Console.WriteLine("Рисование круга, радиус: " + radius);
    }
}

// Прямоугольник
class Rectangle : IDrawable
{
    double width;
    double height;

    public Rectangle(double width, double height)
    {
        this.width = width;
        this.height = height;
    }

    public void Draw()
    {
        Console.WriteLine("Рисование прямоугольника, ширина: " + width + ", высота: " + height);
    }
}

// Треугольник
class Triangle : IDrawable
{
    double a;
    double b;
    double c;

    public Triangle(double a, double b, double c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }

    public void Draw()
    {
        Console.WriteLine("Рисование треугольника, стороны: " + a + ", " + b + ", " + c);
    }
}

class Program
{
    static void Main()
    {
        // Массив объектов, реализующих интерфейс
        IDrawable[] shapes =
        {
            new Circle(5),
            new Rectangle(4, 6),
            new Triangle(3, 4, 5),
            new Circle(2),
            new Rectangle(10, 2)
        };

        // Вызов метода Draw() для каждого объекта
        foreach (IDrawable shape in shapes)
        {
            shape.Draw();
        }
    }
}