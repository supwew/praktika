using System;

// Интерфейс книги
interface IBook
{
    string Title { get; }
    bool IsAvailable();   // Проверка доступности
    bool Issue();         // Выдача книги (true, если выдана)
}

// Бумажная книга (есть несколько экземпляров)
class PaperBook : IBook
{
    int copies;

    public PaperBook(string title, int copies)
    {
        Title = title;
        this.copies = copies;
    }

    public string Title { get; }

    public bool IsAvailable()
    {
        return copies > 0;
    }

    public bool Issue()
    {
        if (copies == 0)
        {
            return false;
        }
        copies--;
        return true;
    }
}

// Электронная книга (всегда доступна)
class EBook : IBook
{
    public EBook(string title)
    {
        Title = title;
    }

    public string Title { get; }

    public bool IsAvailable()
    {
        return true;
    }

    public bool Issue()
    {
        return true;
    }
}

class Program
{
    static void Main()
    {
        // Книги библиотеки
        IBook[] books =
        {
            new PaperBook("Война и мир", 2),
            new PaperBook("Евгений Онегин", 1),
            new EBook("Идиот")
        };

        while (true)
        {
            // Меню
            Console.WriteLine("\n1 - список книг");
            Console.WriteLine("2 - выдать книгу");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? "";

            // Выбор пункта меню
            switch (choice)
            {
                case "1":
                    // Вывод списка книг и их доступности
                    for (int i = 0; i < books.Length; i++)
                    {
                        string status = books[i].IsAvailable() ? "доступна" : "нет в наличии";
                        Console.WriteLine((i + 1) + ". " + books[i].Title + " - " + status);
                    }
                    break;

                case "2":
                    // Ввод номера книги с проверкой
                    Console.Write("Номер книги: ");
                    if (!int.TryParse(Console.ReadLine(), out int number) || number < 1 || number > books.Length)
                    {
                        Console.WriteLine("Нет книги с таким номером.");
                        break;
                    }

                    // Выдача книги
                    IBook book = books[number - 1];
                    if (book.Issue())
                    {
                        Console.WriteLine("Книга выдана: " + book.Title);
                    }
                    else
                    {
                        Console.WriteLine("Книга недоступна: " + book.Title);
                    }
                    break;

                case "0":
                    // Выход из программы
                    return;

                default:
                    Console.WriteLine("Нет такого пункта меню.");
                    break;
            }
        }
    }
}