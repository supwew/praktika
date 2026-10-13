#nullable disable
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;

namespace zadanie2
{
    public partial class MainWindow : Window
    {
        // Файл базы данных рядом с программой
        string db = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "tasks.db");

        public MainWindow()
        {
            InitializeComponent();
            Run("CREATE TABLE IF NOT EXISTS Tasks (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Title TEXT, Description TEXT, Done INTEGER)");
            Load();
        }

        // Выполнение SQL-запроса (параметры @p0, @p1 ...), возврат таблицы с результатом
        DataTable Run(string sql, params object[] args)
        {
            using var connection = new SqliteConnection(db);
            connection.Open();
            using var command = new SqliteCommand(sql, connection);
            for (int i = 0; i < args.Length; i++)
            {
                command.Parameters.AddWithValue("@p" + i, args[i]);
            }
            using var reader = command.ExecuteReader();
            var table = new DataTable();
            if (reader.FieldCount > 0)
            {
                table.Load(reader);
            }
            return table;
        }

        // Вывод задач в таблицу (статус словами "Да" и "Нет")
        void Load()
        {
            grid.ItemsSource = Run("SELECT *, CASE WHEN Done = 1 THEN 'Да' ELSE 'Нет' END AS Status FROM Tasks").DefaultView;
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

        // Добавление (isNew = true) или обновление выбранной задачи
        void Save(bool isNew)
        {
            if (!isNew && grid.SelectedItem == null)
            {
                MessageBox.Show("Выберите задачу в таблице.");
                return;
            }
            if (titleBox.Text.Trim() == "")
            {
                MessageBox.Show("Введите название задачи.");
                return;
            }

            int done = doneBox.IsChecked == true ? 1 : 0;
            if (isNew)
            {
                Run("INSERT INTO Tasks (Title, Description, Done) VALUES (@p0, @p1, @p2)",
                    titleBox.Text.Trim(), descBox.Text.Trim(), done);
            }
            else
            {
                Run("UPDATE Tasks SET Title = @p0, Description = @p1, Done = @p2 WHERE Id = @p3",
                    titleBox.Text.Trim(), descBox.Text.Trim(), done, ((DataRowView)grid.SelectedItem)["Id"]);
            }

            Load();
            titleBox.Text = descBox.Text = "";
            doneBox.IsChecked = false;
        }

        void Add_Click(object sender, RoutedEventArgs e)
        {
            Save(true);
        }

        void Update_Click(object sender, RoutedEventArgs e)
        {
            Save(false);
        }

        // Удаление выбранной задачи
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem == null)
            {
                MessageBox.Show("Выберите задачу в таблице.");
                return;
            }
            if (MessageBox.Show("Удалить задачу?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Run("DELETE FROM Tasks WHERE Id = @p0", ((DataRowView)grid.SelectedItem)["Id"]);
                Load();
            }
        }
    }
}