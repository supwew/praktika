using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie1
{
    public enum ShapeType { Line, Circle, Square }
    internal class Painter
    {
        public Bitmap Canvas;                       // Рисунок
        public Color PenColor = Color.Black;        // Цвет линий
        public ShapeType Shape = ShapeType.Line;    // Выбранная фигура
        public bool IsDrawing = false;              // Идет ли рисование фигуры

        Point start;     // Начало фигуры
        Point current;   // Текущая точка (пока кнопка мыши зажата)

        public Painter(int width, int height)
        {
            Canvas = new Bitmap(width, height);
            Clear();
        }

        // Очистка холста (белый цвет)
        public void Clear()
        {
            using Graphics g = Graphics.FromImage(Canvas);
            g.Clear(Color.White);
        }

        // Начало фигуры
        public void Begin(Point point)
        {
            start = point;
            current = point;
            IsDrawing = true;
        }

        // Движение мыши
        public void Move(Point point)
        {
            current = point;
        }

        // Конец фигуры: она остается на рисунке
        public void End(Point point)
        {
            if (!IsDrawing)
            {
                return;
            }
            IsDrawing = false;
            using Graphics g = Graphics.FromImage(Canvas);
            DrawShape(g, point);
        }

        // Показ фигуры во время рисования
        public void DrawPreview(Graphics g)
        {
            if (IsDrawing)
            {
                DrawShape(g, current);
            }
        }

        // Рисование выбранной фигуры от начальной точки до точки end
        void DrawShape(Graphics g, Point end)
        {
            using Pen pen = new Pen(PenColor, 2);

            switch (Shape)
            {
                case ShapeType.Line:
                    g.DrawLine(pen, start, end);
                    break;

                case ShapeType.Circle:
                    // Центр в начальной точке, радиус до точки end
                    int r = (int)Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
                    g.DrawEllipse(pen, start.X - r, start.Y - r, 2 * r, 2 * r);
                    break;

                case ShapeType.Square:
                    // Квадрат: сторона равна большему из смещений по x и y
                    int side = Math.Max(Math.Abs(end.X - start.X), Math.Abs(end.Y - start.Y));
                    int x = end.X >= start.X ? start.X : start.X - side;
                    int y = end.Y >= start.Y ? start.Y : start.Y - side;
                    g.DrawRectangle(pen, x, y, side, side);
                    break;
            }
        }
    }
}