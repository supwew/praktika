using System;

class Program
{
    static void Main()
    {
        // Ввод количества простых чисел
        Console.Write("Введите K: ");
        int k = int.Parse(Console.ReadLine());

        // Количество найденных простых чисел
        int count = 0;

        // Текущее проверяемое число
        int number = 2;

        // Поиск, пока не найдено K простых чисел
        while (count < k)
        {
            // Проверка на простоту
            bool isPrime = true;
            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }

            // Вывод простого числа
            if (isPrime)
            {
                Console.Write(number + "\t");
                count++;

                // Переход на новую строку после каждых 10 чисел
                if (count % 10 == 0)
                {
                    Console.WriteLine();
                }
            }

            number++;
        }

        Console.WriteLine();
    }
}
