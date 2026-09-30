using System;

class Program
{
    static void Main()
    {
        // Создание генератора случайных чисел
        Random random = new Random();

        // Определение массива из 10 элементов
        int[] array = new int[10];

        // Заполнение массива случайными числами от 1 до 50
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = random.Next(1, 51);
        }

        // Вывод исходного массива
        Console.WriteLine("Исходный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();

        // Ввод числа
        Console.Write("Введите целое число: ");
        int number = int.Parse(Console.ReadLine());

        // Поиск индекса максимального элемента
        int maxIndex = 0;
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] > array[maxIndex])
            {
                maxIndex = i;
            }
        }

        // Замена максимального элемента введенным числом
        array[maxIndex] = number;

        // Вывод измененного массива
        Console.WriteLine("Измененный массив:");
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
    }
}