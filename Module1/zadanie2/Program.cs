using System;

class Program
{
    static void Main()
    {
        // Бесконечный цикл
        while (true)
        {
            // Запрос первого числа
            Console.Write("Введите первое число (q для выхода): ");
            string? input1 = Console.ReadLine();

            // Проверка на выход
            if (input1 == "q")
            {
                break;
            }

            // Попытка преобразования ввода в число, при неудаче вывод сообщения об ошибке
            if (!int.TryParse(input1, out int a))
            {
                Console.WriteLine("Нужно ввести целое число.");
                continue;
            }

            // Запрос второго числа
            Console.Write("Введите второе число: ");
            if (!int.TryParse(Console.ReadLine(), out int b))
            {
                Console.WriteLine("Нужно ввести целое число.");
                continue;
            }

            // Вычисление суммы
            int sum = a + b;

            // Вывод результата
            Console.WriteLine($"{a} + {b} = {sum}");
        }
    }
}