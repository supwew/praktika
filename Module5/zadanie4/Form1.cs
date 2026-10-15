using System;
using System.Drawing;
using System.Windows.Forms;

namespace zadanie4
{
    public partial class Form1 : Form
    {
        // Логика масштабирования находится в классе ImageZoom
        ImageZoom zoom = new ImageZoom();

        public Form1()
        {
            InitializeComponent();
        }

        // Применение масштаба к изображению
        void ApplyZoom()
        {
            if (pictureBox1.Image == null)
            {
                return;
            }

            pictureBox1.Size = zoom.GetSize(pictureBox1.Image.Size);
            zoomLabel.Text = zoom.GetText();
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
                zoom.Reset();
                ApplyZoom();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть изображение.");
            }
        }

        // Увеличение масштаба
        private void zoomInButton_Click(object sender, EventArgs e)
        {
            zoom.ZoomIn();
            ApplyZoom();
        }

        // Уменьшение масштаба
        private void zoomOutButton_Click(object sender, EventArgs e)
        {
            zoom.ZoomOut();
            ApplyZoom();
        }

        // Масштаб по размеру окна
        private void fitButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                return;
            }

            zoom.Fit(pictureBox1.Image.Size, scrollPanel.ClientSize);
            ApplyZoom();
        }

        private void zoomLabel_Click(object sender, EventArgs e)
        {

        }
    }
}