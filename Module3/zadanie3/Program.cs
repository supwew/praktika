using System;
using System.Collections.Generic;

// Делегат: действие над задачей
delegate void TaskAction(string title);

// Задача: название и выбранное действие
class TaskItem
{
    public string Title;
    public string ActionName;
    public TaskAction Action;

    public TaskItem(string title, string actionName, TaskAction action)
    {
        Title = title;
        ActionName = actionName;
        Action = action;
    }
}

class Program
{
    // Журнал
    static List<string> journal = new List<string>();

    // Действие 1: отправка уведомления
    static void Notify(string title)
    {
        Console.WriteLine("Уведомление отправлено: " + title);
    }

    // Действие 2: запись в журнал
    static void WriteToJournal(string title)
    {
        journal.Add(title);
        Console.WriteLine("Запись в журнал: " + title);
    }

    static void Main()
    {
        List<TaskItem> tasks = new List<TaskItem>();

        while (true)
        {
            // Меню
            Console.WriteLine("\n1 - добавить задачу");
            Console.WriteLine("2 - показать задачи");
            Console.WriteLine("3 - выполнить все задачи");
            Console.WriteLine("4 - показать журнал");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                // Ввод названия задачи
                Console.Write("Название задачи: ");
                string title = Console.ReadLine() ?? "";

                // Выбор делегата для задачи
                Console.WriteLine("Действие: 1 - уведомление, 2 - запись в журнал");
                Console.Write("Выбор: ");
                string action = Console.ReadLine() ?? "";
                while (action != "1" && action != "2")
                {
                    Console.Write("Нужно 1 или 2. Повторите ввод: ");
                    action = Console.ReadLine() ?? "";
                }

                if (action == "1")
                {
                    tasks.Add(new TaskItem(title, "уведомление", Notify));
                }
                else
                {
                    tasks.Add(new TaskItem(title, "запись в журнал", WriteToJournal));
                }
            }
            else if (choice == "2")
            {
                // Вывод списка задач
                if (tasks.Count == 0)
                {
                    Console.WriteLine("Задач нет.");
                }
                for (int i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + tasks[i].Title + " (" + tasks[i].ActionName + ")");
                }
            }
            else if (choice == "3")
            {
                // Вызов делегата каждой задачи
                if (tasks.Count == 0)
                {
                    Console.WriteLine("Задач нет.");
                }
                foreach (TaskItem task in tasks)
                {
                    task.Action(task.Title);
                }
            }
            else if (choice == "4")
            {
                // Вывод журнала
                if (journal.Count == 0)
                {
                    Console.WriteLine("Журнал пуст.");
                }
                foreach (string line in journal)
                {
                    Console.WriteLine(line);
                }
            }
            else if (choice == "0")
            {
                break;
            } else {
                Console.WriteLine("Нет такого пункта меню.");
            }
        }
    }
}