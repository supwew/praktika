using System;

class Program
{
    static void Main()
    {
        // Бесконечный цикл
        while (true)
        {
            // Запрос числа
            Console.Write("Введите число (q для выхода): ");
            string? input = Console.ReadLine();

            // Проверка на выход
            if (input == null || input == "q")
            {
                break;
            }

            // Попытка преобразования ввода в число, при неудаче вывод сообщения об ошибке
            if (!int.TryParse(input, out int n))
            {
                Console.WriteLine("Нужно ввести целое число.");
                continue;
            }

            // Числа меньше 2 не являются простыми
            bool isPrime = n >= 2;

            // Проверка делителей от 2 до квадратного корня из n
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            // Вывод результата
            if (isPrime)
            {
                Console.WriteLine($"{n} - простое число.");
            }
            else
            {
                Console.WriteLine($"{n} - не простое число.");
            }
        }
    }
}