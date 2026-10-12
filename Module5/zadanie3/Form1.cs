using System;
using System.Windows.Forms;

namespace zadanie3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Добавление задачи
        private void addButton_Click(object sender, EventArgs e)
        {
            string text = taskTextBox.Text.Trim();
            if (text == "")
            {
                return;
            }

            tasksCheckedListBox.Items.Add(text);
            taskTextBox.Clear();
            UpdateCounter();
        }

        // Удаление выбранной задачи
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (tasksCheckedListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите задачу.");
                return;
            }

            tasksCheckedListBox.Items.RemoveAt(tasksCheckedListBox.SelectedIndex);
            UpdateCounter();
        }

        // Изменение галочки: счетчик обновляется после изменения
        private void tasksCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            BeginInvoke(new Action(UpdateCounter));
        }

        // Счетчик выполненных задач
        void UpdateCounter()
        {
            counterLabel.Text = "Выполнено: " + tasksCheckedListBox.CheckedItems.Count + " из " + tasksCheckedListBox.Items.Count;
        }
    }
}