#nullable disable
using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace zadanie2
{
    // Подключение к базе данных SQLite и выполнение запросов
    public class Database
    {
        string connectionString;

        public Database(string fileName)
        {
            connectionString = "Data Source=" + Path.Combine(AppContext.BaseDirectory, fileName);

            // Создание таблиц сотрудников, проектов и задач
            Run("CREATE TABLE IF NOT EXISTS Employees (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Name TEXT NOT NULL, Position TEXT)");
            Run("CREATE TABLE IF NOT EXISTS Projects (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Name TEXT NOT NULL, Description TEXT)");
            Run("CREATE TABLE IF NOT EXISTS Tasks (Id INTEGER PRIMARY KEY AUTOINCREMENT, Title TEXT NOT NULL, " +
                "ProjectId INTEGER NOT NULL REFERENCES Projects(Id) ON DELETE CASCADE, " +
                "EmployeeId INTEGER REFERENCES Employees(Id) ON DELETE SET NULL, " +
                "Progress INTEGER NOT NULL DEFAULT 0)");
        }

        // Выполнение SQL-запроса (параметры @p0, @p1 ...), возврат таблицы с результатом
        public DataTable Run(string sql, params object[] args)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            // Включение внешних ключей (в SQLite они по умолчанию выключены)
            new SqliteCommand("PRAGMA foreign_keys = ON", connection).ExecuteNonQuery();

            using var command = new SqliteCommand(sql, connection);
            for (int i = 0; i < args.Length; i++)
            {
                command.Parameters.AddWithValue("@p" + i, args[i] ?? DBNull.Value);
            }
            using var reader = command.ExecuteReader();
            var table = new DataTable();
            if (reader.FieldCount > 0)
            {
                table.Load(reader);
            }
            return table;
        }
    }
}