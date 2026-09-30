using System;

class Program
{
    static void Main()
    {
        // генератор случайных чисел
        Random random = new Random();

        // массив из 15 элементов
        int[] numbers = new int[15];

        // Заполнение массива случайными числами от -50 до 50
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(-50, 51);
        }

        // Вывод массива
        Console.WriteLine("Массив:");
        foreach (int number in numbers)
        {
            Console.Write(number + " ");
        }
        Console.WriteLine();

        // Сумма и количество положительных чисел
        int sum = 0;
        int count = 0;

        // Перебор массива, учет только положительных чисел
        foreach (int number in numbers)
        {
            if (number > 0)
            {
                sum += number;
                count++;
            }
        }

        // Проверка на отсутствие положительных чисел
        if (count == 0)
        {
            Console.WriteLine("Положительных чисел нет.");
            return;
        }

        // Вычисление среднего значения
        double average = (double)sum / count;

        // Вывод результата
        Console.WriteLine($"Среднее значение положительных: {average}");
    }
}