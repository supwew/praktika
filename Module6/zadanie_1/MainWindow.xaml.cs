#nullable disable
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;

namespace zadanie_1
{
    public partial class MainWindow : Window
    {
        // Файл базы данных рядом с программой
        string db = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "employees.db");

        public MainWindow()
        {
            InitializeComponent();
            Run("CREATE TABLE IF NOT EXISTS Employees (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "FullName TEXT, Position TEXT, Salary REAL, HireDate TEXT)");
            datePicker.SelectedDate = DateTime.Today;
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

        // Вывод сотрудников в таблицу
        void Load()
        {
            grid.ItemsSource = Run("SELECT * FROM Employees").DefaultView;
        }

        // Выбор строки: данные сотрудника попадают в поля
        void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid.SelectedItem is not DataRowView row)
            {
                return;
            }
            nameBox.Text = row["FullName"].ToString();
            positionBox.Text = row["Position"].ToString();
            salaryBox.Text = row["Salary"].ToString();
            datePicker.SelectedDate = DateTime.Parse(row["HireDate"].ToString());
        }

        // Добавление (isNew = true) или изменение выбранного сотрудника
        void Save(bool isNew)
        {
            if (!isNew && grid.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника в таблице.");
                return;
            }
            if (nameBox.Text.Trim() == "" || !double.TryParse(salaryBox.Text, out double salary) || datePicker.SelectedDate == null)
            {
                MessageBox.Show("Введите ФИО, зарплату (число) и дату.");
                return;
            }

            string date = datePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            if (isNew)
            {
                Run("INSERT INTO Employees (FullName, Position, Salary, HireDate) VALUES (@p0, @p1, @p2, @p3)",
                    nameBox.Text.Trim(), positionBox.Text.Trim(), salary, date);
            }
            else
            {
                Run("UPDATE Employees SET FullName = @p0, Position = @p1, Salary = @p2, HireDate = @p3 WHERE Id = @p4",
                    nameBox.Text.Trim(), positionBox.Text.Trim(), salary, date, ((DataRowView)grid.SelectedItem)["Id"]);
            }

            Load();
            nameBox.Text = positionBox.Text = salaryBox.Text = "";
        }

        void Add_Click(object sender, RoutedEventArgs e)
        {
            Save(true);
        }

        void Update_Click(object sender, RoutedEventArgs e)
        {
            Save(false);
        }

        // Удаление выбранного сотрудника
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem == null)
            {
                MessageBox.Show("Выберите сотрудника в таблице.");
                return;
            }
            if (MessageBox.Show("Удалить сотрудника?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Run("DELETE FROM Employees WHERE Id = @p0", ((DataRowView)grid.SelectedItem)["Id"]);
                Load();
            }
        }
    }
}