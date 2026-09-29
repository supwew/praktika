using System;

class Program
{
    static void Main()
    {
        // Ввод размера матрицы
        Console.Write("Размер матрицы N: ");
        int n = int.Parse(Console.ReadLine());

        // Создание матрицы и заполнение случайными числами от -50 до 50
        Random random = new Random();
        int[,] matrix = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                matrix[i, j] = random.Next(-50, 51);
            }
        }

        // Вывод исходной матрицы
        Console.WriteLine("Исходная матрица:");
        Print(matrix, n);

        // Массив сумм строк
        int[] sums = new int[n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                sums[i] += matrix[i, j];
            }
        }

        // Сортировка строк пузырьком по суммам
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - 1 - i; j++)
            {
                if (sums[j] > sums[j + 1])
                {
                    // Обмен сумм
                    int tempSum = sums[j];
                    sums[j] = sums[j + 1];
                    sums[j + 1] = tempSum;

                    // Обмен строк матрицы
                    for (int k = 0; k < n; k++)
                    {
                        int temp = matrix[j, k];
                        matrix[j, k] = matrix[j + 1, k];
                        matrix[j + 1, k] = temp;
                    }
                }
            }
        }

        // Вывод упорядоченной матрицы вместе с суммами строк
        Console.WriteLine("Упорядоченная матрица (справа сумма строки):");
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(5));
            }
            Console.WriteLine("  | " + sums[i]);
        }
    }

    // Метод вывода матрицы
    static void Print(int[,] matrix, int n)
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(matrix[i, j].ToString().PadLeft(5));
            }
            Console.WriteLine();
        }
    }
}