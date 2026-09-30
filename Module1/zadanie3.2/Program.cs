using System;

class Program
{
    static void Main()
    {
        // Ввод числа N
        Console.Write("Введите число N: ");
        int n = int.Parse(Console.ReadLine());

        Random random = new Random();
        int[] array = new int[n];
        int count = 0;  // Количество элементов
        int sum = 0;    // Сумма элементов

        // Пока можно добавить хотя бы число 1
        while (sum < n)
        {
            // Случайное число от 1 до 9
            int value = random.Next(1, 10);

            // Число добавляется, только если сумма не превысит N
            if (sum + value <= n)
            {
                array[count] = value;
                sum += value;
                count++;
            }
        }

        // Вывод элементов
        Console.WriteLine("Элементы массива:");
        for (int i = 0; i < count; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();

        // Вывод количества и суммы
        Console.WriteLine("Количество элементов: " + count);
        Console.WriteLine("Сумма: " + sum);
    }
}