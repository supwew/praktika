using System;
class Person// Класс человека
{
    // Поля
    private string name = "";
    private int age;
    private string address = "";

    // Установка и получение значений
    public void SetName(string value) { name = value; }
    public string GetName() { return name; }
    public void SetAge(int value) { age = value; }
    public int GetAge() { return age; }
    public void SetAddress(string value) { address = value; }
    public string GetAddress() { return address; }

    // Вывод информации о человеке
    public void Print()
    {
        Console.WriteLine("Имя: " + name + ", возраст: " + age + ", адрес: " + address);
    }
}

class Program
{
    // Ввод данных человека с клавиатуры
    static Person ReadPerson()
    {
        Person person = new Person();
        Console.Write("Имя: ");
        person.SetName(Console.ReadLine() ?? "");

        // Проверка возраста: целое число от 0 до 150
        Console.Write("Возраст: ");
        int age;
        while (!int.TryParse(Console.ReadLine(), out age) || age < 0 || age > 150)
        {
            Console.Write("Нужно число от 0 до 150. Повторите ввод: ");
        }
        person.SetAge(age);

        Console.Write("Адрес: ");
        person.SetAddress(Console.ReadLine() ?? "");

        return person;
    }
    static void Main()
    {
        // Ввод количества людей: целое число больше 0
        Console.Write("Сколько человек: ");
        int count;
        while (!int.TryParse(Console.ReadLine(), out count) || count <= 0)
        {
            Console.Write("Нужно число больше 0. Повторите ввод: ");
        }
        // Ввод данных всех людей в массив
        Person[] people = new Person[count];
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine("Человек " + (i + 1) + ":");
            people[i] = ReadPerson();
        }
        // Вывод информации
        Console.WriteLine();
        for (int i = 0; i < count; i++)
        {
            people[i].Print();
        }
    }
}