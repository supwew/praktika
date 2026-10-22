using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using zadanie3;

namespace zadanie3
{
    // Управление задачами: изменение, хранение в файле, уведомления
    public class TaskManager
    {
        List<TaskItem> tasks = new List<TaskItem>();   // Закрытый список
        string path = Path.Combine(AppContext.BaseDirectory, "tasks.json");

        public TaskManager()
        {
            Load();
        }

        // Все задачи: сначала с ближайшим сроком, при равном сроке сначала важные
        public List<TaskItem> GetAll()
        {
            return tasks.OrderBy(t => t.DueDate).ThenByDescending(t => t.Priority).ToList();
        }

        // Добавление задачи
        public void Add(string title, Priority priority, DateTime dueDate, int progress)
        {
            TaskItem task = new TaskItem();
            Fill(task, title, priority, dueDate, progress);
            tasks.Add(task);
            Save();
        }

        // Изменение задачи
        public void Update(TaskItem task, string title, Priority priority, DateTime dueDate, int progress)
        {
            Fill(task, title, priority, dueDate, progress);
            Save();
        }

        // Удаление задачи
        public void Delete(TaskItem task)
        {
            tasks.Remove(task);
            Save();
        }

        // Проверка и запись значений в задачу
        void Fill(TaskItem task, string title, Priority priority, DateTime dueDate, int progress)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Название задачи не может быть пустым.");
            }

            task.Title = title.Trim();
            task.Priority = priority;
            task.DueDate = dueDate.Date;
            task.Progress = Math.Max(0, Math.Min(100, progress));
        }

        // Уведомления: просроченные задачи и задачи со сроком в ближайшие days дней
        public List<string> GetWarnings(int days)
        {
            List<string> warnings = new List<string>();
            foreach (TaskItem task in GetAll())
            {
                int left = (task.DueDate.Date - DateTime.Today).Days;
                if (task.Progress >= 100 || left > days)
                {
                    continue;
                }

                if (left < 0)
                {
                    warnings.Add("Просрочена: «" + task.Title + "» (срок был " + task.DueText + ")");
                }
                else if (left == 0)
                {
                    warnings.Add("Срок сегодня: «" + task.Title + "»");
                }
                else
                {
                    warnings.Add("Через " + left + " дн.: «" + task.Title + "» (" + task.DueText + ")");
                }
            }
            return warnings;
        }

        // Чтение задач из файла (поврежденный файл дает пустой список)
        void Load()
        {
            if (!File.Exists(path))
            {
                return;
            }

            try
            {
                tasks = JsonSerializer.Deserialize<List<TaskItem>>(File.ReadAllText(path)) ?? new List<TaskItem>();
            }
            catch (JsonException)
            {
                tasks = new List<TaskItem>();
            }
        }

        // Запись задач в файл
        void Save()
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping   // Русские буквы без кодов
            };
            File.WriteAllText(path, JsonSerializer.Serialize(tasks, options));
        }
    }
}