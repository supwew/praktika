using System;

// Интерфейс рисунка
interface IDrawing
{
    void DrawLine(int x1, int y1, int x2, int y2);
    void DrawCircle(int cx, int cy, int r);
    void DrawRectangle(int x, int y, int w, int h);
}

// Холст (рисование символами в консоли)
class Canvas : IDrawing
{
    int width;
    int height;
    char[,] cells;

    public Canvas(int width, int height)
    {
        this.width = width;
        this.height = height;

        // Заполнение холста пробелами
        cells = new char[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                cells[y, x] = ' ';
            }
        }
    }

    // Установка точки (точки за границами холста пропускаются)
    void SetPoint(int x, int y)
    {
        if (x >= 0 && x < width && y >= 0 && y < height)
        {
            cells[y, x] = '*';
        }
    }

    // Линия: точки между началом и концом
    public void DrawLine(int x1, int y1, int x2, int y2)
    {
        int dx = x2 - x1;
        int dy = y2 - y1;
        int steps = Math.Max(Math.Abs(dx), Math.Abs(dy));

        // Линия из одной точки
        if (steps == 0)
        {
            SetPoint(x1, y1);
            return;
        }

        for (int i = 0; i <= steps; i++)
        {
            int x = x1 + (int)Math.Round((double)dx * i / steps);
            int y = y1 + (int)Math.Round((double)dy * i / steps);
            SetPoint(x, y);
        }
    }

    // Круг: точки окружности (по ширине в 2 раза больше, так как символы выше, чем шире)
    public void DrawCircle(int cx, int cy, int r)
    {
        for (int angle = 0; angle < 360; angle++)
        {
            double a = angle * Math.PI / 180;
            int x = cx + (int)Math.Round(2 * r * Math.Cos(a));
            int y = cy + (int)Math.Round(r * Math.Sin(a));
            SetPoint(x, y);
        }
    }

    // Прямоугольник: четыре линии
    public void DrawRectangle(int x, int y, int w, int h)
    {
        DrawLine(x, y, x + w, y);
        DrawLine(x + w, y, x + w, y + h);
        DrawLine(x + w, y + h, x, y + h);
        DrawLine(x, y + h, x, y);
    }

    // Вывод холста
    public void Print()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(cells[y, x]);
            }
            Console.WriteLine();
        }
    }
}

class Program
{
    static void Main()
    {
        Canvas canvas = new Canvas(50, 20);

        // Рисование фигур
        canvas.DrawRectangle(2, 2, 20, 8);
        canvas.DrawLine(26, 2, 48, 18);
        canvas.DrawCircle(12, 15, 4);

        // Вывод холста
        canvas.Print();
    }
}