using System;
using System.Text.Json.Serialization;

namespace zadanie3
{
    // Приоритет задачи
    public enum Priority { Low, Medium, High }

    // Задача
    public class TaskItem
    {
        public string Title { get; set; } = "";
        public Priority Priority { get; set; } = Priority.Medium;
        public DateTime DueDate { get; set; } = DateTime.Today;
        public int Progress { get; set; }   // Прогресс выполнения, %

        // Свойства для таблицы (в файл не сохраняются)
        [JsonIgnore]
        public string PriorityText
        {
            get { return new string[] { "Низкий", "Средний", "Высокий" }[(int)Priority]; }
        }

        [JsonIgnore]
        public string DueText
        {
            get { return DueDate.ToString("dd.MM.yyyy"); }
        }

        // Статус: выполнена, просрочена или в работе
        [JsonIgnore]
        public string Status
        {
            get
            {
                if (Progress >= 100)
                {
                    return "Выполнена";
                }
                return DueDate.Date < DateTime.Today ? "Просрочена" : "В работе";
            }
        }
    }
}