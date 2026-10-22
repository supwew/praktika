#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using zadanie1;

namespace zadanie1
{
    class Program
    {
        static StudentList list = new StudentList();
        static string file = "students.txt";   // Файл со списком

        static void Main()
        {
            // Загрузка списка при запуске
            if (File.Exists(file))
            {
                list.Load(file);
            }

            while (true)
            {
                // Меню
                Console.WriteLine("\n1 - показать всех");
                Console.WriteLine("2 - добавить студента");
                Console.WriteLine("3 - редактировать студента");
                Console.WriteLine("4 - удалить студента");
                Console.WriteLine("5 - поиск по имени");
                Console.WriteLine("6 - поиск по группе");
                Console.WriteLine("7 - сортировка");
                Console.WriteLine("8 - сохранить в файл");
                Console.WriteLine("9 - загрузить из файла");
                Console.WriteLine("0 - выход (список сохраняется)");
                Console.Write("Выбор: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Show(list.GetAll(), true);
                        break;
                    case "2":
                        list.Add(ReadStudent());
                        break;
                    case "3":
                        Show(list.GetAll(), true);
                        if (list.Count > 0)
                        {
                            list.Update(AskInt("Номер студента", 1, list.Count), ReadStudent());
                        }
                        break;
                    case "4":
                        Show(list.GetAll(), true);
                        if (list.Count > 0)
                        {
                            list.Delete(AskInt("Номер студента", 1, list.Count));
                        }
                        break;
                    case "5":
                        Show(list.FindByName(AskText("Часть имени")), false);
                        break;
                    case "6":
                        Show(list.FindByGroup(AskText("Группа")), false);
                        break;
                    case "7":
                        Console.WriteLine("1 - по имени, 2 - по группе, 3 - по возрасту, 4 - по баллу");
                        list.Sort(AskInt("Критерий", 1, 4));
                        Show(list.GetAll(), true);
                        break;
                    case "8":
                        list.Save(AskText("Имя файла"));
                        break;
                    case "9":
                        string path = AskText("Имя файла");
                        if (File.Exists(path))
                        {
                            list.Load(path);
                            Console.WriteLine("Загружено студентов: " + list.Count);
                        }
                        else
                        {
                            Console.WriteLine("Файл не найден.");
                        }
                        break;
                    case "0":
                        list.Save(file);
                        return;
                    default:
                        Console.WriteLine("Нет такого пункта меню.");
                        break;
                }
            }
        }

        // Ввод данных студента
        static Student ReadStudent()
        {
            string name = AskText("Имя Фамилия");
            int age = AskInt("Возраст", 14, 100);
            string group = AskText("Группа");
            double grade = AskNumber("Средний балл (0-10)", 0, 10);
            return new Student(name, age, group, grade);
        }

        // Вывод студентов в виде таблицы
        static void Show(List<Student> students, bool numbered)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Студенты не найдены.");
                return;
            }

            Console.WriteLine((numbered ? "№   " : "") + "Имя".PadRight(25) + "Группа".PadRight(10) + "Возраст".PadRight(9) + "Балл");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine((numbered ? (i + 1).ToString().PadRight(4) : "") + students[i]);
            }
        }

        // Ввод непустой строки
        static string AskText(string text)
        {
            string s;
            do
            {
                Console.Write(text + ": ");
                s = (Console.ReadLine() ?? "").Trim();
            }
            while (s == "");
            return s;
        }

        // Ввод целого числа в диапазоне
        static int AskInt(string text, int min, int max)
        {
            int value;
            Console.Write(text + ": ");
            while (!int.TryParse(Console.ReadLine(), out value) || value < min || value > max)
            {
                Console.Write("Нужно целое число от " + min + " до " + max + ": ");
            }
            return value;
        }

        // Ввод числа в диапазоне
        static double AskNumber(string text, double min, double max)
        {
            double value;
            Console.Write(text + ": ");
            while (!double.TryParse(Console.ReadLine(), out value) || value < min || value > max)
            {
                Console.Write("Нужно число от " + min + " до " + max + ": ");
            }
            return value;
        }
    }
}