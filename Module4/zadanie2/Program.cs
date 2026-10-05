using System;

// Интерфейс товара
interface IProduct
{
    string Name { get; }
    double GetCost(double amount);   // Стоимость покупки заданного количества
    double GetStock();               // Остаток на складе
}

// Молоко (цена за штуку)
class Milk : IProduct
{
    double price;
    double stock;

    public Milk(double price, double stock)
    {
        this.price = price;
        this.stock = stock;
    }

    public string Name => "Молоко";

    public double GetCost(double amount)
    {
        return price * amount;
    }

    public double GetStock()
    {
        return stock;
    }
}

// Хлеб (цена за штуку, скидка 10% от 5 штук)
class Bread : IProduct
{
    double price;
    double stock;

    public Bread(double price, double stock)
    {
        this.price = price;
        this.stock = stock;
    }

    public string Name => "Хлеб";

    public double GetCost(double amount)
    {
        double cost = price * amount;
        if (amount >= 5)
        {
            cost = cost * 0.9;
        }
        return cost;
    }

    public double GetStock()
    {
        return stock;
    }
}

// Яблоки (цена за килограмм)
class Apples : IProduct
{
    double price;
    double stock;

    public Apples(double price, double stock)
    {
        this.price = price;
        this.stock = stock;
    }

    public string Name => "Яблоки";

    public double GetCost(double amount)
    {
        return price * amount;
    }

    public double GetStock()
    {
        return stock;
    }
}

class Program
{
    static void Main()
    {
        // Товары: цена и остаток на складе
        IProduct[] products = { new Milk(3.2, 40), new Bread(1.5, 25), new Apples(4.8, 120.5) };

        // Ввод количества для покупки
        Console.Write("Количество для покупки (шт. или кг): ");
        double amount;
        while (!double.TryParse(Console.ReadLine(), out amount) || amount <= 0)
        {
            Console.Write("Нужно число больше 0. Повторите ввод: ");
        }

        // Вывод остатка и стоимости по каждому товару
        foreach (IProduct p in products)
        {
            Console.Write(p.Name + ": остаток " + p.GetStock());
            if (amount > p.GetStock())
            {
                Console.WriteLine(", на складе недостаточно");
            }
            else
            {
                Console.WriteLine(", стоимость покупки " + p.GetCost(amount).ToString("F2") + " руб.");
            }
        }
    }
}