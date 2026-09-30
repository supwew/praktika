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
            if (input == "q")
            {
                break;
            }

            // Попытка преобразования ввода в число, при неудаче вывод сообщения об ошибке
            if (!int.TryParse(input, out int n))
            {
                Console.WriteLine("Нужно ввести целое число.");
                continue;
            }

            // Накопление результата
            long result = 1;

            // Умножение результата на 2, 3, 4 ... до n
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }

            // Вывод ответа
            Console.WriteLine($"{n}! = {result}");
        }
    }
}