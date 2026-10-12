using System;
using System.Drawing;
using System.Windows.Forms;

namespace zadanie1
{
    public partial class Form1 : Form
    {
        // Рисунок, цвет и данные текущей фигуры
        Bitmap bitmap = new Bitmap(1200, 800);
        Color color = Color.Black;
        Point start;
        Point current;
        bool drawing = false;

        public Form1()
        {
            InitializeComponent();

            // Белый холст
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
            }
            canvasPictureBox.Image = bitmap;
        }

        // Выбор цвета
        private void colorButton_Click(object sender, EventArgs e)
        {
            using ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                color = dialog.Color;
            }
        }

        // Очистка холста
        private void clearButton_Click(object sender, EventArgs e)
        {
            using Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            canvasPictureBox.Invalidate();
        }

        // Нажатие кнопки мыши: начало фигуры
        private void canvasPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            start = e.Location;
            current = e.Location;
            drawing = true;
        }

        // Движение мыши: показ фигуры до отпускания кнопки
        private void canvasPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                current = e.Location;
                canvasPictureBox.Invalidate();
            }
        }

        // Отпускание кнопки мыши: фигура остается на рисунке
        private void canvasPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (!drawing)
            {
                return;
            }
            drawing = false;
            using Graphics g = Graphics.FromImage(bitmap);
            using Pen pen = new Pen(color, 2);
            DrawShape(g, pen, start, e.Location);
            canvasPictureBox.Invalidate();
        }

        // Вывод фигуры во время рисования
        private void canvasPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (drawing)
            {
                using Pen pen = new Pen(color, 2);
                DrawShape(e.Graphics, pen, start, current);
            }
        }

        // Рисование выбранной фигуры от точки a до точки b
        void DrawShape(Graphics g, Pen pen, Point a, Point b)
        {
            if (lineRadioButton.Checked)
            {
                g.DrawLine(pen, a, b);
            }
            else if (circleRadioButton.Checked)
            {
                // Центр в точке a, радиус до точки b
                int r = (int)Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
                g.DrawEllipse(pen, a.X - r, a.Y - r, 2 * r, 2 * r);
            }
            else
            {
                // Квадрат: сторона равна большему из смещений по x и y
                int side = Math.Max(Math.Abs(b.X - a.X), Math.Abs(b.Y - a.Y));
                int x = b.X >= a.X ? a.X : a.X - side;
                int y = b.Y >= a.Y ? a.Y : a.Y - side;
                g.DrawRectangle(pen, x, y, side, side);
            }
        }
    }
}