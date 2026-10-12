#nullable disable
using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace zadanie1
{
    public partial class Form1 : Form
    {
        // Файл базы данных рядом с программой
        string connectionString = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "employees.db");

        public Form1()
        {
            InitializeComponent();

            // Создание таблицы (если ее нет) и вывод данных
            Execute("CREATE TABLE IF NOT EXISTS Employees (" +
                    "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "FullName TEXT NOT NULL, Position TEXT, Salary REAL, HireDate TEXT)");
            LoadData();
        }

        // Выполнение запроса без результата (создание, добавление, изменение, удаление)
        void Execute(string sql, params SqliteParameter[] parameters)
        {
            using SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();
            using SqliteCommand command = new SqliteCommand(sql, connection);
            command.Parameters.AddRange(parameters);
            command.ExecuteNonQuery();
        }

        // Загрузка сотрудников в таблицу на форме
        void LoadData()
        {
            using SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();
            using SqliteCommand command = new SqliteCommand(
                "SELECT Id, FullName, Position, Salary, HireDate FROM Employees ORDER BY Id", connection);
            using SqliteDataReader reader = command.ExecuteReader();

            DataTable table = new DataTable();
            table.Load(reader);
            employeesGridView.DataSource = table;

            // Заголовки столбцов
            employeesGridView.Columns["Id"].Visible = false;
            employeesGridView.Columns["FullName"].HeaderText = "ФИО";
            employeesGridView.Columns["Position"].HeaderText = "Должность";
            employeesGridView.Columns["Salary"].HeaderText = "Зарплата";
            employeesGridView.Columns["HireDate"].HeaderText = "Дата приема";

            // Снятие выделения
            employeesGridView.CurrentCell = null;
            employeesGridView.ClearSelection();
        }

        // Номер (Id) выбранного сотрудника, -1 если ничего не выбрано
        int GetSelectedId()
        {
            DataGridViewRow row = employeesGridView.CurrentRow;
            if (row == null)
            {
                return -1;
            }
            return Convert.ToInt32(row.Cells["Id"].Value);
        }

        // Проверка и чтение полей формы
        bool ReadFields(out string name, out string position, out double salary)
        {
            name = nameTextBox.Text.Trim();
            position = positionTextBox.Text.Trim();
            salary = 0;

            if (name == "")
            {
                MessageBox.Show("Введите ФИО.");
                return false;
            }
            if (!double.TryParse(salaryTextBox.Text, out salary) || salary < 0)
            {
                MessageBox.Show("Зарплата должна быть числом не меньше 0.");
                return false;
            }
            return true;
        }

        // Очистка полей ввода
        void ClearFields()
        {
            nameTextBox.Clear();
            positionTextBox.Clear();
            salaryTextBox.Clear();
            hireDatePicker.Value = DateTime.Today;
        }

        // Выбор строки в таблице: данные сотрудника попадают в поля
        private void employeesGridView_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow row = employeesGridView.CurrentRow;
            if (row == null)
            {
                return;
            }

            nameTextBox.Text = row.Cells["FullName"].Value.ToString();
            positionTextBox.Text = row.Cells["Position"].Value.ToString();
            salaryTextBox.Text = row.Cells["Salary"].Value.ToString();

            DateTime date;
            if (DateTime.TryParseExact(row.Cells["HireDate"].Value.ToString(), "yyyy-MM-dd",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                hireDatePicker.Value = date;
            }
        }

        // Добавление сотрудника
        private void addButton_Click(object sender, EventArgs e)
        {
            if (!ReadFields(out string name, out string position, out double salary))
            {
                return;
            }

            Execute("INSERT INTO Employees (FullName, Position, Salary, HireDate) " +
                    "VALUES (@name, @position, @salary, @date)",
                new SqliteParameter("@name", name),
                new SqliteParameter("@position", position),
                new SqliteParameter("@salary", salary),
                new SqliteParameter("@date", hireDatePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));

            LoadData();
            ClearFields();
        }

        // Изменение выбранного сотрудника
        private void updateButton_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();
            if (id == -1)
            {
                MessageBox.Show("Выберите сотрудника в таблице.");
                return;
            }
            if (!ReadFields(out string name, out string position, out double salary))
            {
                return;
            }

            Execute("UPDATE Employees SET FullName = @name, Position = @position, " +
                    "Salary = @salary, HireDate = @date WHERE Id = @id",
                new SqliteParameter("@name", name),
                new SqliteParameter("@position", position),
                new SqliteParameter("@salary", salary),
                new SqliteParameter("@date", hireDatePicker.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                new SqliteParameter("@id", id));

            LoadData();
            ClearFields();
        }

        // Удаление выбранного сотрудника
        private void deleteButton_Click(object sender, EventArgs e)
        {
            int id = GetSelectedId();
            if (id == -1)
            {
                MessageBox.Show("Выберите сотрудника в таблице.");
                return;
            }
            if (MessageBox.Show("Удалить выбранного сотрудника?", "Удаление", MessageBoxButtons.YesNo) != DialogResult.Yes)
            {
                return;
            }

            Execute("DELETE FROM Employees WHERE Id = @id", new SqliteParameter("@id", id));

            LoadData();
            ClearFields();
        }
    }
}