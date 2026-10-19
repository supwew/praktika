#nullable disable
using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace zadanie_1
{
    // Работа с базой данных сотрудников
    public class EmployeeDb
    {
        // Файл базы данных рядом с программой
        string db = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "employees.db");

        public EmployeeDb()
        {
            Run("CREATE TABLE IF NOT EXISTS Employees (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "FullName TEXT, Position TEXT, Salary REAL, HireDate TEXT)");
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

        // Все сотрудники
        public DataTable GetAll()
        {
            return Run("SELECT * FROM Employees");
        }

        // Добавление сотрудника
        public void Add(string name, string position, double salary, string date)
        {
            Run("INSERT INTO Employees (FullName, Position, Salary, HireDate) VALUES (@p0, @p1, @p2, @p3)",
                name, position, salary, date);
        }

        // Изменение сотрудника
        public void Update(int id, string name, string position, double salary, string date)
        {
            Run("UPDATE Employees SET FullName = @p0, Position = @p1, Salary = @p2, HireDate = @p3 WHERE Id = @p4",
                name, position, salary, date, id);
        }

        // Удаление сотрудника
        public void Delete(int id)
        {
            Run("DELETE FROM Employees WHERE Id = @p0", id);
        }
    }
}