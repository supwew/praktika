using System;

// Структура поезда
struct train
{
    public string destination;   // Пункт назначения
    public int number;           // Номер поезда
    public TimeSpan time;        // Время отправления
}

class Program
{
    // Вывод информации о поезде
    static void Print(train t)
    {
        Console.WriteLine("Поезд № " + t.number + ", пункт назначения: " + t.destination +
                          ", отправление: " + t.time.ToString(@"hh\:mm"));
    }

    // Вывод всех поездов
    static void PrintAll(train[] trains)
    {
        foreach (train t in trains)
        {
            Print(t);
        }
    }

    // Сортировка по номерам поездов (пузырьком)
    static void SortByNumber(train[] trains)
    {
        for (int i = 0; i < trains.Length - 1; i++)
        {
            for (int j = 0; j < trains.Length - 1 - i; j++)
            {
                if (trains[j].number > trains[j + 1].number)
                {
                    train temp = trains[j];
                    trains[j] = trains[j + 1];
                    trains[j + 1] = temp;
                }
            }
        }
    }

    // Сортировка по пункту назначения, при одинаковых пунктах по времени отправления
    static void SortByDestination(train[] trains)
    {
        for (int i = 0; i < trains.Length - 1; i++)
        {
            for (int j = 0; j < trains.Length - 1 - i; j++)
            {
                int compare = string.Compare(trains[j].destination, trains[j + 1].destination);

                // Обмен, если пункт больше или пункты равны, а время больше
                if (compare > 0 || (compare == 0 && trains[j].time > trains[j + 1].time))
                {
                    train temp = trains[j];
                    trains[j] = trains[j + 1];
                    trains[j + 1] = temp;
                }
            }
        }
    }

    static void Main()
    {
        train[] trains = new train[5];

        // Ввод данных о поездах
        for (int i = 0; i < trains.Length; i++)
        {
            Console.WriteLine("Поезд " + (i + 1) + ":");

            Console.Write("Пункт назначения: ");
            trains[i].destination = Console.ReadLine() ?? "";

            Console.Write("Номер поезда: ");
            while (!int.TryParse(Console.ReadLine(), out trains[i].number) || trains[i].number <= 0)
            {
                Console.Write("Нужно целое число больше 0. Повторите ввод: ");
            }

            Console.Write("Время отправления (чч:мм): ");
            while (!TimeSpan.TryParse(Console.ReadLine(), out trains[i].time)
                   || trains[i].time < TimeSpan.Zero || trains[i].time >= TimeSpan.FromHours(24))
            {
                Console.Write("Нужно время в формате чч:мм. Повторите ввод: ");
            }
        }

        // Упорядочивание по номерам поездов
        SortByNumber(trains);
        Console.WriteLine("\nПоезда по номерам:");
        PrintAll(trains);

        // Меню
        while (true)
        {
            Console.WriteLine("\n1 - информация о поезде по номеру");
            Console.WriteLine("2 - сортировка по пункту назначения и времени");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "1")
            {
                // Поиск поезда по введенному номеру
                Console.Write("Номер поезда: ");
                if (!int.TryParse(Console.ReadLine(), out int number))
                {
                    Console.WriteLine("Нужно ввести целое число.");
                    continue;
                }

                bool found = false;
                foreach (train t in trains)
                {
                    if (t.number == number)
                    {
                        Print(t);
                        found = true;
                    }
                }

                if (!found)
                {
                    Console.WriteLine("Поезд с таким номером не найден.");
                }
            }
            else if (choice == "2")
            {
                SortByDestination(trains);
                Console.WriteLine("Поезда по пункту назначения и времени:");
                PrintAll(trains);
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