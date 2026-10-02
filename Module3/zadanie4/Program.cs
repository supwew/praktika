using System;
using System.Collections.Generic;

// Запись данных: текст и дата
class Record
{
    public string Text;
    public DateTime Date;

    public Record(string text, DateTime date)
    {
        Text = text;
        Date = date;
    }
}

class Program
{
    // Вывод записей, подходящих под фильтр (фильтр передается как делегат)
    static void Show(List<Record> list, Func<Record, bool> filter)
    {
        int count = 0;
        foreach (Record r in list)
        {
            if (filter(r))
            {
                Console.WriteLine(r.Date.ToString("dd.MM.yyyy") + " - " + r.Text);
                count++;
            }
        }
        if (count == 0)
        {
            Console.WriteLine("Ничего не найдено.");
        }
    }

    static void Main()
    {
        // Данные
        List<Record> list = new List<Record>
        {
            new Record("Сдать отчет по практике", new DateTime(2026, 10, 5)),
            new Record("Встреча с куратором", new DateTime(2026, 10, 12)),
            new Record("Купить учебник по C#", new DateTime(2026, 9, 28)),
            new Record("Сдать лабораторную работу", new DateTime(2026, 10, 20)),
            new Record("День рождения друга", new DateTime(2026, 11, 3)),
            new Record("Консультация по отчету", new DateTime(2026, 10, 8))
        };

        while (true)
        {
            // Меню
            Console.WriteLine("\n1 - фильтр по дате (начиная с даты)");
            Console.WriteLine("2 - фильтр по ключевому слову");
            Console.WriteLine("3 - без фильтра");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                // Ввод даты с проверкой
                Console.Write("Показать записи начиная с даты (дд.мм.гггг): ");
                DateTime from;
                while (!DateTime.TryParse(Console.ReadLine(), out from))
                {
                    Console.Write("Нужна дата в формате дд.мм.гггг. Повторите ввод: ");
                }

                // Фильтр по дате
                Show(list, r => r.Date >= from);
            }
            else if (choice == "2")
            {
                // Ввод ключевого слова
                Console.Write("Ключевое слово: ");
                string word = (Console.ReadLine() ?? "").ToLower();

                // Фильтр по ключевому слову
                Show(list, r => r.Text.ToLower().Contains(word));
            }
            else if (choice == "3")
            {
                // Фильтр, пропускающий все записи
                Show(list, r => true);
            }
            else if (choice == "0")
            {
                break;
            }
            else
            {
                Console.WriteLine("Нет такого пункта меню.");
            }
        }
    }
}