using System;
using System.Drawing;
using System.Windows.Forms;

namespace zadanie1
{
    public partial class Form1 : Form
    {
        // Логика рисования находится в классе Painter
        Painter painter = new Painter(1200, 800);

        public Form1()
        {
            InitializeComponent();
            canvasPictureBox.Image = painter.Canvas;
        }

        // Выбор цвета
        private void colorButton_Click(object sender, EventArgs e)
        {
            using ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                painter.PenColor = dialog.Color;
            }
        }

        // Очистка холста
        private void clearButton_Click(object sender, EventArgs e)
        {
            painter.Clear();
            canvasPictureBox.Invalidate();
        }

        // Нажатие кнопки мыши: выбор фигуры и начало рисования
        private void canvasPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (lineRadioButton.Checked)
            {
                painter.Shape = ShapeType.Line;
            }
            else if (circleRadioButton.Checked)
            {
                painter.Shape = ShapeType.Circle;
            }
            else
            {
                painter.Shape = ShapeType.Square;
            }
            painter.Begin(e.Location);
        }

        // Движение мыши: показ фигуры до отпускания кнопки
        private void canvasPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (painter.IsDrawing)
            {
                painter.Move(e.Location);
                canvasPictureBox.Invalidate();
            }
        }

        // Отпускание кнопки мыши: фигура остается на рисунке
        private void canvasPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            painter.End(e.Location);
            canvasPictureBox.Invalidate();
        }

        // Вывод фигуры во время рисования
        private void canvasPictureBox_Paint(object sender, PaintEventArgs e)
        {
            painter.DrawPreview(e.Graphics);
        }
    }
}