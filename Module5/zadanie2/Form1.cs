using System;
using System.Windows.Forms;

namespace zadanie2
{
    public partial class Form1 : Form
    {
        // Логика работы с файлом находится в классе TextEditor
        TextEditor editor = new TextEditor();

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
                textBox1.Text = editor.Open(openFileDialog1.FileName);
                textBox1.Select(0, 0);
                Text = "Текстовый редактор - " + editor.FileName;
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
            string? path = editor.CurrentFile;

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
                editor.Save(path, textBox1.Text);
                Text = "Текстовый редактор - " + editor.FileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить файл: " + ex.Message);
            }
        }
    }
}