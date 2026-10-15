using System;
using System.Collections.Generic;

namespace zadanie3
{
    // Задача: название и признак выполнения
    internal class TaskItem
    {
        public string Title = "";
        public bool Done = false;
    }

    // Логика учета задач (без элементов формы)
    internal class TaskManager
    {
        public List<TaskItem> Tasks = new List<TaskItem>();

        // Добавление задачи (false, если название пустое)
        public bool Add(string title)
        {
            title = title.Trim();
            if (title == "")
            {
                return false;
            }

            Tasks.Add(new TaskItem { Title = title });
            return true;
        }

        // Удаление задачи по номеру
        public void Delete(int index)
        {
            Tasks.RemoveAt(index);
        }

        // Отметка задачи как выполненной или невыполненной
        public void SetDone(int index, bool done)
        {
            Tasks[index].Done = done;
        }

        // Текст счетчика выполненных задач
        public string Counter()
        {
            int done = 0;
            foreach (TaskItem task in Tasks)
            {
                if (task.Done)
                {
                    done++;
                }
            }
            return "Выполнено: " + done + " из " + Tasks.Count;
        }
    }
}