#nullable disable
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace zadanie2
{
    public partial class MainWindow : Window
    {
        TaskDb db = new TaskDb();

        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        // Вывод задач в таблицу
        void Load()
        {
            grid.ItemsSource = db.GetAll().DefaultView;
        }

        // Номер выбранной задачи, -1 если ничего не выбрано
        int SelectedId()
        {
            if (grid.SelectedItem is DataRowView row)
            {
                return Convert.ToInt32(row["Id"]);
            }
            return -1;
        }

        // Очистка полей ввода
        void ClearFields()
        {
            titleBox.Text = descBox.Text = "";
            doneBox.IsChecked = false;
        }

        // Выбор строки: данные задачи попадают в поля
        void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid.SelectedItem is not DataRowView row)
            {
                return;
            }
            titleBox.Text = row["Title"].ToString();
            descBox.Text = row["Description"].ToString();
            doneBox.IsChecked = Convert.ToInt32(row["Done"]) == 1;
        }

        void Add_Click(object sender, RoutedEventArgs e)
        {
            if (titleBox.Text.Trim() == "")
            {
                MessageBox.Show("Введите название задачи.");
                return;
            }
            db.Add(titleBox.Text.Trim(), descBox.Text.Trim(), doneBox.IsChecked == true);
            Load();
            ClearFields();
        }

        void Update_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedId() == -1)
            {
                MessageBox.Show("Выберите задачу в таблице.");
                return;
            }
            if (titleBox.Text.Trim() == "")
            {
                MessageBox.Show("Введите название задачи.");
                return;
            }
            db.Update(SelectedId(), titleBox.Text.Trim(), descBox.Text.Trim(), doneBox.IsChecked == true);
            Load();
            ClearFields();
        }

        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedId() == -1)
            {
                MessageBox.Show("Выберите задачу в таблице.");
                return;
            }
            if (MessageBox.Show("Удалить задачу?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.Delete(SelectedId());
                Load();
                ClearFields();
            }
        }
    }
}