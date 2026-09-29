using System;

class Program
{
    // Метод вычисления НОД (алгоритм Евклида)
    static int Gcd(int a, int b)
    {
        // Пока b не равно 0: замена пары (a, b) на (b, остаток от a / b)
        while (b != 0)
        {
            int temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static void Main()
    {
        int numerator;
        int denominator;

        // Ввод числителя: целое число, не меньше 0
        Console.Write("Числитель: ");
        while (!int.TryParse(Console.ReadLine(), out numerator) || numerator < 0)
        {
            Console.Write("Нужно целое число не меньше 0. Повторите ввод: ");
        }

        // Ввод знаменателя: целое число, больше 0
        Console.Write("Знаменатель: ");
        while (!int.TryParse(Console.ReadLine(), out denominator) || denominator <= 0)
        {
            Console.Write("Нужно целое число больше 0. Повторите ввод: ");
        }

        // Нахождение НОД
        int gcd = Gcd(numerator, denominator);

        // Деление числителя и знаменателя на НОД
        numerator = numerator / gcd;
        denominator = denominator / gcd;

        // Вывод результата
        Console.WriteLine("Сокращенная дробь: " + numerator + "/" + denominator);
    }
}