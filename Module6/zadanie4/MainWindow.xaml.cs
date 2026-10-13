#nullable disable
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Data.Sqlite;

namespace zadanie4
{
    public partial class MainWindow : Window
    {
        // Файл базы данных рядом с программой
        string db = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "finance.db");

        public MainWindow()
        {
            InitializeComponent();
            Run("CREATE TABLE IF NOT EXISTS Records (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Type TEXT, Category TEXT, Amount REAL, RecordDate TEXT)");
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

        // Вывод записей в таблицу и подсчет итогов
        void Load()
        {
            DataTable table = Run("SELECT * FROM Records ORDER BY RecordDate DESC, Id DESC");
            grid.ItemsSource = table.DefaultView;

            double income = 0;
            double expense = 0;
            foreach (DataRow r in table.Rows)
            {
                if (r["Type"].ToString() == "Доход")
                {
                    income += Convert.ToDouble(r["Amount"]);
                }
                else
                {
                    expense += Convert.ToDouble(r["Amount"]);
                }
            }
            summaryText.Text = "Доходы: " + income + "   Расходы: " + expense + "   Баланс: " + (income - expense);
        }

        // Выбор строки: данные записи попадают в поля
        void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid.SelectedItem is not DataRowView row)
            {
                return;
            }
            typeBox.SelectedIndex = row["Type"].ToString() == "Доход" ? 0 : 1;
            categoryBox.Text = row["Category"].ToString();
            amountBox.Text = row["Amount"].ToString();
            datePicker.SelectedDate = DateTime.Parse(row["RecordDate"].ToString());
        }

        // Добавление (isNew = true) или изменение выбранной записи
        void Save(bool isNew)
        {
            if (!isNew && grid.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись в таблице.");
                return;
            }
            if (categoryBox.Text.Trim() == "" || !double.TryParse(amountBox.Text, out double amount)
                || amount <= 0 || datePicker.SelectedDate == null)
            {
                MessageBox.Show("Введите категорию, сумму (число больше 0) и дату.");
                return;
            }

            string type = typeBox.SelectedIndex == 0 ? "Доход" : "Расход";
            string date = datePicker.SelectedDate.Value.ToString("yyyy-MM-dd");
            if (isNew)
            {
                Run("INSERT INTO Records (Type, Category, Amount, RecordDate) VALUES (@p0, @p1, @p2, @p3)",
                    type, categoryBox.Text.Trim(), amount, date);
            }
            else
            {
                Run("UPDATE Records SET Type = @p0, Category = @p1, Amount = @p2, RecordDate = @p3 WHERE Id = @p4",
                    type, categoryBox.Text.Trim(), amount, date, ((DataRowView)grid.SelectedItem)["Id"]);
            }

            Load();
            categoryBox.Text = amountBox.Text = "";
        }

        void Add_Click(object sender, RoutedEventArgs e)
        {
            Save(true);
        }

        void Update_Click(object sender, RoutedEventArgs e)
        {
            Save(false);
        }

        // Удаление выбранной записи
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem == null)
            {
                MessageBox.Show("Выберите запись в таблице.");
                return;
            }
            if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Run("DELETE FROM Records WHERE Id = @p0", ((DataRowView)grid.SelectedItem)["Id"]);
                Load();
            }
        }
    }
}