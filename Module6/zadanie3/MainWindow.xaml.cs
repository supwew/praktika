#nullable disable
using System;
using System.Data;
using System.Windows;

namespace zadanie3
{
    public partial class MainWindow : Window
    {
        LibraryDb db = new LibraryDb();

        DataTable table;   // Таблица книг (поиск выполняется ее фильтром)

        public MainWindow()
        {
            InitializeComponent();
            Load();
        }

        // Вывод книг в таблицу
        void Load()
        {
            table = db.GetAll();
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

            db.Rent(Convert.ToInt32(row["Id"]), readerBox.Text.Trim());
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

            db.Return(Convert.ToInt32(row["Id"]));
            Load();
        }
    }
}