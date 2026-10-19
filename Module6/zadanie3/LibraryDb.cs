#nullable disable
using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace zadanie3
{
    // Работа с базой данных библиотеки
    public class LibraryDb
    {
        // Файл базы данных рядом с программой
        string db = "Data Source=" + Path.Combine(AppContext.BaseDirectory, "library.db");

        public LibraryDb()
        {
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

        // Все книги (статус словами)
        public DataTable GetAll()
        {
            return Run("SELECT *, CASE WHEN Available = 1 THEN 'В наличии' ELSE 'Выдана' END AS Status " +
                       "FROM Books ORDER BY Title");
        }

        // Аренда книги
        public void Rent(int id, string reader)
        {
            Run("UPDATE Books SET Available = 0, Reader = @p0, RentDate = @p1 WHERE Id = @p2",
                reader, DateTime.Today.ToString("yyyy-MM-dd"), id);
        }

        // Возврат книги
        public void Return(int id)
        {
            Run("UPDATE Books SET Available = 1, Reader = NULL, RentDate = NULL WHERE Id = @p0", id);
        }
    }
}