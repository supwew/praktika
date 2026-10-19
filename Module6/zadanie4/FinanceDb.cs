#nullable disable
using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace zadanie4
{
    // Работа с базой данных финансов
    public class FinanceDb
    {
        // Файл базы данных рядом с программой
        string db = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "finance.db");

        public FinanceDb()
        {
            Run("CREATE TABLE IF NOT EXISTS Records (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Type TEXT, Category TEXT, Amount REAL, RecordDate TEXT)");
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

        // Все записи
        public DataTable GetAll()
        {
            return Run("SELECT * FROM Records ORDER BY RecordDate DESC, Id DESC");
        }

        // Сумма записей одного типа ("Доход" или "Расход")
        public double Sum(string type)
        {
            DataTable table = Run("SELECT IFNULL(SUM(Amount), 0) AS Total FROM Records WHERE Type = @p0", type);
            return Convert.ToDouble(table.Rows[0]["Total"]);
        }

        // Добавление записи
        public void Add(string type, string category, double amount, string date)
        {
            Run("INSERT INTO Records (Type, Category, Amount, RecordDate) VALUES (@p0, @p1, @p2, @p3)",
                type, category, amount, date);
        }

        // Изменение записи
        public void Update(int id, string type, string category, double amount, string date)
        {
            Run("UPDATE Records SET Type = @p0, Category = @p1, Amount = @p2, RecordDate = @p3 WHERE Id = @p4",
                type, category, amount, date, id);
        }

        // Удаление записи
        public void Delete(int id)
        {
            Run("DELETE FROM Records WHERE Id = @p0", id);
        }
    }
}