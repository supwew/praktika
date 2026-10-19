#nullable disable
using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace zadanie2
{
    // Работа с базой данных задач
    public class TaskDb
    {
        // Файл базы данных рядом с программой
        string db = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "tasks.db");

        public TaskDb()
        {
            Run("CREATE TABLE IF NOT EXISTS Tasks (Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "Title TEXT, Description TEXT, Done INTEGER)");
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

        // Все задачи (статус словами "Да" и "Нет")
        public DataTable GetAll()
        {
            return Run("SELECT *, CASE WHEN Done = 1 THEN 'Да' ELSE 'Нет' END AS Status FROM Tasks");
        }

        // Добавление задачи
        public void Add(string title, string description, bool done)
        {
            Run("INSERT INTO Tasks (Title, Description, Done) VALUES (@p0, @p1, @p2)",
                title, description, done ? 1 : 0);
        }

        // Обновление задачи
        public void Update(int id, string title, string description, bool done)
        {
            Run("UPDATE Tasks SET Title = @p0, Description = @p1, Done = @p2 WHERE Id = @p3",
                title, description, done ? 1 : 0, id);
        }

        // Удаление задачи
        public void Delete(int id)
        {
            Run("DELETE FROM Tasks WHERE Id = @p0", id);
        }
    }
}