using System;

class Program
{
    static void Main()
    {
        // Запрос размера массива
        Console.Write("Введите размер массива N: ");

        // Проверка размера: целое число больше 0
        if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
        {
            Console.WriteLine("Нужно ввести целое число больше 0.");
            return;
        }

        // Создание массива
        double[] array = new double[n];

        // Ввод элементов массива
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Элемент {i + 1}: ");
            while (!double.TryParse(Console.ReadLine(), out array[i]))
            {
                Console.Write("Нужно ввести число. Повторите ввод: ");
            }
        }

        // Поиск максимального по модулю элемента
        double maxAbs = 0;
        foreach (double x in array)
        {
            if (Math.Abs(x) > maxAbs)
            {
                maxAbs = Math.Abs(x);
            }
        }

        // Проверка: деление на ноль невозможно, если все элементы равны 0
        if (maxAbs == 0)
        {
            Console.WriteLine("Все элементы равны 0, нормировка невозможна.");
            return;
        }

        // Нормировка: деление каждого элемента на максимальный по модулю
        for (int i = 0; i < n; i++)
        {
            array[i] /= maxAbs;
        }

        // Вывод измененного массива
        Console.WriteLine("Нормированный массив:");
        foreach (double x in array)
        {
            Console.WriteLine(x);
        }
        Console.WriteLine();
    }
}