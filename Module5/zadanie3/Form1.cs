using System;
using System.Windows.Forms;

namespace zadanie3
{
    public partial class Form1 : Form
    {
        // Логика учета задач находится в классе TaskManager
        TaskManager manager = new TaskManager();

        bool updating = false;   // Идет заполнение списка (галочки пока не обрабатываются)

        public Form1()
        {
            InitializeComponent();
            ShowTasks();
        }

        // Вывод задач из менеджера в список
        void ShowTasks()
        {
            updating = true;
            tasksCheckedListBox.Items.Clear();
            foreach (TaskItem task in manager.Tasks)
            {
                tasksCheckedListBox.Items.Add(task.Title, task.Done);
            }
            updating = false;

            counterLabel.Text = manager.Counter();
        }

        // Добавление задачи
        private void addButton_Click(object sender, EventArgs e)
        {
            if (manager.Add(taskTextBox.Text))
            {
                taskTextBox.Clear();
                ShowTasks();
            }
        }

        // Удаление выбранной задачи
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (tasksCheckedListBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите задачу.");
                return;
            }

            manager.Delete(tasksCheckedListBox.SelectedIndex);
            ShowTasks();
        }

        // Изменение галочки: задача отмечается выполненной или нет
        private void tasksCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (updating)
            {
                return;
            }

            manager.SetDone(e.Index, e.NewValue == CheckState.Checked);
            counterLabel.Text = manager.Counter();
        }
    }
}