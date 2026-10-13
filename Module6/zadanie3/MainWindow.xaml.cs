#nullable disable
using System;
using System.Data;
using System.IO;
using System.Windows;
using Microsoft.Data.Sqlite;

namespace zadanie3
{
    public partial class MainWindow : Window
    {
        // Файл базы данных рядом с программой
        string db = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "library.db");

        DataTable table;   // Таблица книг (поиск выполняется ее фильтром)

        public MainWindow()
        {
            InitializeComponent();
            Run("CREATE TABLE IF NOT EXISTS Books (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Title TEXT, Author TEXT, Year INTEGER, Available INTEGER DEFAULT 1, Reader TEXT, RentDate TEXT)");

            // Начальный список книг, если таблица пустая
            if (Run("SELECT * FROM Books").Rows.Count == 0)
            {
                Run("INSERT INTO Books (Title, Author, Year) VALUES " +
                    "('Война и мир', 'Л. Н. Толстой', 1869), ('Анна Каренина', 'Л. Н. Толстой', 1877), " +
                    "('Преступление и наказание', 'Ф. М. Достоевский', 1866), ('Идиот', 'Ф. М. Достоевский', 1869), " +
                    "('Евгений Онегин', 'А. С. Пушкин', 1833), ('Мастер и Маргарита', 'М. А. Булгаков', 1967), " +
                    "('Вишневый сад', 'А. П. Чехов', 1904), ('Мертвые души', 'Н. В. Гоголь', 1842)");
            }
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
            var result = new DataTable();
            if (reader.FieldCount > 0)
            {
                result.Load(reader);
            }
            return result;
        }

        // Вывод книг в таблицу (статус словами)
        void Load()
        {
            table = Run("SELECT *, CASE WHEN Available = 1 THEN 'В наличии' ELSE 'Выдана' END AS Status " +
                        "FROM Books ORDER BY Title");
            grid.ItemsSource = table.DefaultView;
            Search();
        }

        // Поиск по названию и автору (фильтр таблицы не зависит от регистра)
        void Search()
        {
            string word = searchBox.Text.Trim().Replace("'", "''");
            table.DefaultView.RowFilter = "Title LIKE '%" + word + "%' OR Author LIKE '%" + word + "%'";
        }

        void Search_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            searchBox.Clear();
            Search();
        }

        // Аренда выбранной книги
        void Rent_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите книгу в таблице.");
                return;
            }
            if (readerBox.Text.Trim() == "")
            {
                MessageBox.Show("Введите имя читателя.");
                return;
            }
            if (Convert.ToInt32(row["Available"]) == 0)
            {
                MessageBox.Show("Эта книга уже выдана.");
                return;
            }

            Run("UPDATE Books SET Available = 0, Reader = @p0, RentDate = @p1 WHERE Id = @p2",
                readerBox.Text.Trim(), DateTime.Today.ToString("yyyy-MM-dd"), row["Id"]);
            readerBox.Clear();
            Load();
        }

        // Возврат выбранной книги
        void Return_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is not DataRowView row)
            {
                MessageBox.Show("Выберите книгу в таблице.");
                return;
            }
            if (Convert.ToInt32(row["Available"]) == 1)
            {
                MessageBox.Show("Эта книга не выдавалась.");
                return;
            }

            Run("UPDATE Books SET Available = 1, Reader = NULL, RentDate = NULL WHERE Id = @p0", row["Id"]);
            Load();
        }
    }
}