using System;
using System.Drawing;
using System.Windows.Forms;

namespace zadanie4
{
    public partial class Form1 : Form
    {
        double zoom = 1.0;   // Масштаб: 1 = 100%

        public Form1()
        {
            InitializeComponent();
        }

        // Выбор и открытие изображения
        private void openButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                Image image = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = image;
                SetZoom(1.0);
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть изображение.");
            }
        }

        // Увеличение и уменьшение масштаба
        private void zoomInButton_Click(object sender, EventArgs e)
        {
            SetZoom(zoom * 1.25);
        }

        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            SetZoom(zoom / 1.25);
        }

        // Масштаб, при котором изображение целиком помещается в окно
        private void fitButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }

            double byWidth = (double)scrollPanel.ClientSize.Width / pictureBox1.Image.Width;
            double byHeight = (double)scrollPanel.ClientSize.Height / pictureBox1.Image.Height;
            SetZoom(Math.Min(byWidth, byHeight));
        }

        // Установка масштаба (от 10% до 1000%)
        void SetZoom(double value)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }

            zoom = Math.Max(0.1, Math.Min(10, value));
            pictureBox1.Size = new Size((int)(pictureBox1.Image.Width * zoom), (int)(pictureBox1.Image.Height * zoom));
            zoomLabel.Text = (int)(zoom * 100) + "%";
        }

        private void zoomLabel_Click(object sender, EventArgs e)
        {

        }
    }
}