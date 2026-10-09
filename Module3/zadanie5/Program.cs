using System;

// Делегат: метод сортировки массива
delegate void SortMethod(int[] array);

class Program
{
    // Сортировка пузырьком
    static void BubbleSort(int[] a)
    {
        for (int i = 0; i < a.Length - 1; i++)
        {
            for (int j = 0; j < a.Length - 1 - i; j++)
            {
                if (a[j] > a[j + 1])
                {
                    int temp = a[j];
                    a[j] = a[j + 1];
                    a[j + 1] = temp;
                }
            }
        }
    }

    // Быстрая сортировка
    static void QuickSort(int[] a)
    {
        Quick(a, 0, a.Length - 1);
    }

    static void Quick(int[] a, int left, int right)
    {
        int i = left;
        int j = right;
        int pivot = a[(left + right) / 2];   // Опорный элемент

        while (i <= j)
        {
            while (a[i] < pivot) i++;
            while (a[j] > pivot) j--;
            if (i <= j)
            {
                int temp = a[i];
                a[i] = a[j];
                a[j] = temp;
                i++;
                j--;
            }
        }

        // Сортировка левой и правой частей
        if (left < j) Quick(a, left, j);
        if (i < right) Quick(a, i, right);
    }

    // Вывод массива
    static void Print(int[] a)
    {
        foreach (int x in a)
        {
            Console.Write(x + " ");
        }
        Console.WriteLine();
    }

    static void Main()
    {
        // Ввод количества чисел: целое число больше 0
        Console.Write("Сколько чисел: ");
        int n;
        while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
        {
            Console.Write("Нужно целое число больше 0. Повторите ввод: ");
        }

        // Заполнение массива случайными числами от 1 до 100
        Random random = new Random();
        int[] numbers = new int[n];
        for (int i = 0; i < n; i++)
        {
            numbers[i] = random.Next(1, 101);
        }

        // Вывод исходного массива
        Console.Write("Исходный массив: ");
        Print(numbers);

        while (true)
        {
            // Меню
            Console.WriteLine("\n1 - сортировка пузырьком");
            Console.WriteLine("2 - быстрая сортировка");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? "";

            // Переменная для хранения выбранного делегата
            SortMethod sort;

            switch (choice)
            {
                case "0":
                    return; // Полный выход из программы (завершение метода Main)

                case "1":
                    sort = BubbleSort;
                    break;

                case "2":
                    sort = QuickSort;
                    break;

                default:
                    Console.WriteLine("Нет такого пункта меню.");
                    continue; // Пропуск текущей итерации цикла while и возврат к началу меню
            }

            // Сортировка копии массива и вывод результата
            int[] copy = (int[])numbers.Clone();
            sort(copy);
            Console.Write("Результат: ");
            Print(copy);
        }
    }
}
