using System;

class Program
{
    static void Main()
    {
        Random random = new Random();
        double[] array = new double[10];
        int[] index = new int[10];

        // Заполнение массива случайными числами из [-10, 10)
        // и массива индексов числами 0, 1, 2 ... 9
        for (int i = 0; i < 10; i++)
        {
            array[i] = random.NextDouble() * 20 - 10;
            index[i] = i;
        }

        // Сортировка индексов по значениям элементов
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9 - i; j++)
            {
                // Если значение по текущему индексу больше следующего, обмен индексов
                if (array[index[j]] > array[index[j + 1]])
                {
                    int temp = index[j];
                    index[j] = index[j + 1];
                    index[j + 1] = temp;
                }
            }
        }

        // Вывод массива
        Console.WriteLine("Массив:");
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(i + ": " + array[i].ToString("F2"));
        }

        // Вывод индексов
        Console.WriteLine("Индексы по возрастанию значений:");
        for (int i = 0; i < 10; i++)
        {
            Console.Write(index[i] + " ");
        }
        Console.WriteLine();
    }
}