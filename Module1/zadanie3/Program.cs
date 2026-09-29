using System;

class Program
{
    static void Main()
    {
        // Бесконечный цикл, программа работает до ввода "q"
        while (true)
        {
            // Запрос строки
            Console.Write("Введите строку (q для выхода): ");
            string? input = Console.ReadLine();

            // Проверка на выход
            if (input == null || input == "q")
            {
                break;
            }

            // Проверка на пустую строку
            if (input == "")
            {
                Console.WriteLine("Строка пустая.");
                continue;
            }

            // Вывод строки в обратном порядке (цикл с конца к началу)
            Console.Write("Результат: ");
            for (int i = input.Length - 1; i >= 0; i--)
            {
                Console.Write(input[i]);
            }
            Console.WriteLine();
        }
    }
}