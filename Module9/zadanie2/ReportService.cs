#nullable disable
using System;
using System.Data;
using System.Linq;
using System.Text;
using zadanie2;

namespace zadanie2
{
    // Отчеты о статусе проектов и задач
    public class ReportService
    {
        Database db;
        TaskRepository tasks;

        public ReportService(Database db, TaskRepository tasks)
        {
            this.db = db;
            this.tasks = tasks;
        }

        // Проекты: количество задач, выполненные задачи и готовность проекта
        public DataTable ProjectsReport()
        {
            return db.Run("SELECT p.Name AS Проект, COUNT(t.Id) AS Задач, " +
                          "SUM(CASE WHEN t.Progress = 100 THEN 1 ELSE 0 END) AS Выполнено, " +
                          "IFNULL(CAST(ROUND(AVG(t.Progress)) AS INTEGER), 0) AS Готовность " +
                          "FROM Projects p LEFT JOIN Tasks t ON t.ProjectId = p.Id GROUP BY p.Id ORDER BY p.Id");
        }

        // Задачи по статусам
        public DataTable StatusReport()
        {
            return db.Run("SELECT CASE WHEN Progress = 0 THEN 'Новая' WHEN Progress = 100 THEN 'Выполнена' " +
                          "ELSE 'В работе' END AS Статус, COUNT(*) AS Задач FROM Tasks GROUP BY 1 ORDER BY 2 DESC");
        }

        // Текст отчета для сохранения в файл
        public string ToText()
        {
            StringBuilder sb = new StringBuilder("ОТЧЕТ О СТАТУСЕ ПРОЕКТОВ И ЗАДАЧ (" +
                                                 DateTime.Now.ToString("dd.MM.yyyy HH:mm") + ")\n\n");
            Append(sb, "Проекты", ProjectsReport());
            Append(sb, "Задачи по статусам", StatusReport());
            Append(sb, "Все задачи", tasks.GetAll());
            return sb.ToString();
        }

        // Добавление таблицы в текст отчета
        static void Append(StringBuilder sb, string title, DataTable table)
        {
            sb.AppendLine(title + ":");
            sb.AppendLine(string.Join(" | ", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
            foreach (DataRow row in table.Rows)
            {
                sb.AppendLine(string.Join(" | ", row.ItemArray));
            }
            sb.AppendLine();
        }
    }
}