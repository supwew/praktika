using System;
using System.IO;
using System.Windows.Forms;

namespace zadanie2
{
    public partial class Form1 : Form
    {
        string? currentFile = null;   // Путь к открытому файлу

        public Form1()
        {
            InitializeComponent();
        }

        // Пункт меню "Открыть"
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                textBox1.Select(0, 0);
                currentFile = openFileDialog1.FileName;
                Text = "Текстовый редактор - " + Path.GetFileName(currentFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось открыть файл: " + ex.Message);
            }
        }

        // Пункт меню "Сохранить"
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(false);
        }

        // Пункт меню "Сохранить как..."
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(true);
        }

        // Пункт меню "Выход"
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Сохранение файла (saveAs = true: всегда с выбором имени)
        void SaveFile(bool saveAs)
        {
            string? path = currentFile;

            if (path == null || saveAs)
            {
                if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                path = saveFileDialog1.FileName;
            }

            try
            {
                File.WriteAllText(path, textBox1.Text);
                currentFile = path;
                Text = "Текстовый редактор - " + Path.GetFileName(path);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить файл: " + ex.Message);
            }
        }
    }
}