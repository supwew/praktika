using System;

class Program
{
    static void Main()
    {
        // Ввод данных
        Console.Write("Размер массива K: ");
        int k = int.Parse(Console.ReadLine());
        Console.Write("Начало диапазона A: ");
        int a = int.Parse(Console.ReadLine());
        Console.Write("Конец диапазона B: ");
        int b = int.Parse(Console.ReadLine());

        // Создание массива и заполнение случайными числами из [A, B)
        Random random = new Random();
        int[] array = new int[k];
        for (int i = 0; i < k; i++)
        {
            array[i] = random.Next(a, b);
        }

        // Вывод массива
        Console.WriteLine("Массив:");
        for (int i = 0; i < k; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();

        // Поиск индексов минимального и максимального элементов
        int iMin = 0;
        int iMax = 0;
        for (int i = 0; i < k; i++)
        {
            if (array[i] < array[iMin])
            {
                iMin = i;
            }
            if (array[i] > array[iMax])
            {
                iMax = i;
            }
        }
        Console.WriteLine("Индекс минимального: " + iMin);
        Console.WriteLine("Индекс максимального: " + iMax);

        // Начало и конец участка: меньший индекс - начало, больший - конец
        int start = iMin;
        int end = iMax;
        if (start > end)
        {
            start = iMax;
            end = iMin;
        }

        // Вывод элементов от start до end включительно
        Console.WriteLine("Элементы между ними:");
        for (int i = start; i <= end; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }
}