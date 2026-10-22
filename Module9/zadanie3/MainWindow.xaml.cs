using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using zadanie3;

namespace zadanie3
{
    public partial class MainWindow : Window
    {
        // Вся логика находится в классе TaskManager
        TaskManager manager = new TaskManager();
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            ClearFields();
            ShowTasks();

            // Проверка сроков каждую минуту
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += (s, e) => ShowWarnings(false);
            timer.Start();
        }

        // Уведомление о сроках при запуске
        void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowWarnings(true);
        }

        // Вывод задач в таблицу и обновление уведомлений
        void ShowTasks()
        {
            grid.ItemsSource = manager.GetAll();
            ShowWarnings(false);
        }

        // Уведомления о просроченных задачах и сроках в ближайшие 3 дня
        void ShowWarnings(bool showMessage)
        {
            var warnings = manager.GetWarnings(3);
            warningBorder.Visibility = warnings.Count > 0 ? Visibility.Visible : Visibility.Collapsed;
            warningText.Text = string.Join("\n", warnings);

            if (showMessage && warnings.Count > 0)
            {
                MessageBox.Show(string.Join("\n", warnings), "Приближаются сроки выполнения");
            }
        }

        // Очистка полей ввода
        void ClearFields()
        {
            titleBox.Text = "";
            priorityBox.SelectedIndex = 1;
            datePicker.SelectedDate = DateTime.Today;
            progressSlider.Value = 0;
        }

        // Выбор строки: данные задачи попадают в поля
        void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid.SelectedItem is TaskItem task)
            {
                titleBox.Text = task.Title;
                priorityBox.SelectedIndex = (int)task.Priority;
                datePicker.SelectedDate = task.DueDate;
                progressSlider.Value = task.Progress;
            }
        }

        // Добавление (task = null) или изменение задачи
        void Save(TaskItem task)
        {
            if (datePicker.SelectedDate == null)
            {
                MessageBox.Show("Выберите срок выполнения.");
                return;
            }

            try
            {
                Priority priority = (Priority)priorityBox.SelectedIndex;
                DateTime due = datePicker.SelectedDate.Value;
                if (task == null)
                {
                    manager.Add(titleBox.Text, priority, due, (int)progressSlider.Value);
                }
                else
                {
                    manager.Update(task, titleBox.Text, priority, due, (int)progressSlider.Value);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            ShowTasks();
            ClearFields();
        }

        void Add_Click(object sender, RoutedEventArgs e)
        {
            Save(null);
        }

        void Update_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is TaskItem task)
            {
                Save(task);
            }
            else
            {
                MessageBox.Show("Выберите задачу в таблице.");
            }
        }

        // Удаление выбранной задачи
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is not TaskItem task)
            {
                MessageBox.Show("Выберите задачу в таблице.");
                return;
            }
            if (MessageBox.Show("Удалить задачу?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                manager.Delete(task);
                ShowTasks();
                ClearFields();
            }
        }
    }
}