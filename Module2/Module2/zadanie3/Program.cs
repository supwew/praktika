using System;
// Автор
class Author
{
    public string Name;
    public int BirthYear;

    public Author(string name, int birthYear)
    {
        Name = name;
        BirthYear = birthYear;
    }
}
// Книга (автор входит в книгу как поле: композиция)
class Book
{
    public string Title;
    public int Year;
    public Author Author;

    public Book(string title, int year, Author author)
    {
        Title = title;
        Year = year;
        Author = author;
    }
        // Вывод информации о книге
    public void Print()
    {
        Console.WriteLine("Книга: " + Title + " (" + Year + "), автор: " + Author.Name + ", год рождения: " + Author.BirthYear);
    }
}

class Program
{
    static void Main()
    {
        // Создание авторов
        Author pushkin = new Author("А. С. Пушкин", 1799);
        Author tolstoy = new Author("Л. Н. Толстой", 1828);
        Author dostoevsky = new Author("Ф. М. Достоевский", 1821);
        Author chekhov = new Author("А. П. Чехов", 1860);
        Author gogol = new Author("Н. В. Гоголь", 1809);

        // Создание книг
        Book[] books =
        {
            new Book("Идиот", 1869, dostoevsky),
            new Book("Евгений Онегин", 1833, pushkin),
            new Book("Капитанская дочка", 1836, pushkin),
            new Book("Палата № 6", 1892, chekhov),
            new Book("Анна Каренина", 1877, tolstoy),
            new Book("Преступление и наказание", 1866, dostoevsky),
            new Book("Вишневый сад", 1904, chekhov),
            new Book("Мертвые души", 1842, gogol),
            new Book("Война и мир", 1869, tolstoy),
            new Book("Ревизор", 1836, gogol)
        };

        // Вывод информации
        foreach (Book book in books)
        {
            book.Print();
        }
    }
}