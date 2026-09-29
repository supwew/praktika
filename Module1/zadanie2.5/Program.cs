using System;

class Program
{
    static void Main()
    {
        // Ввод размера массива
        Console.Write("Размер массива K: ");
        int k = int.Parse(Console.ReadLine());

        // Русские буквы и отдельно гласные
        string letters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string vowels = "аеёиоуыэюя";

        // Создание первого массива и заполнение случайными буквами
        Random random = new Random();
        char[] array1 = new char[k];
        for (int i = 0; i < k; i++)
        {
            array1[i] = letters[random.Next(letters.Length)];
        }

        // Подсчет согласных, чтобы знать размер второго массива
        int count = 0;
        for (int i = 0; i < k; i++)
        {
            if (vowels.IndexOf(array1[i]) == -1 && array1[i] != 'ъ' && array1[i] != 'ь')
            {
                count++;
            }
        }

        // Создание второго массива и заполнение согласными
        char[] array2 = new char[count];
        int j = 0;
        for (int i = 0; i < k; i++)
        {
            if (vowels.IndexOf(array1[i]) == -1 && array1[i] != 'ъ' && array1[i] != 'ь')
            {
                array2[j] = array1[i];
                j++;
            }
        }

        // Вывод первого массива
        Console.WriteLine("Первый массив:");
        for (int i = 0; i < array1.Length; i++)
        {
            Console.Write(array1[i] + " ");
        }
        Console.WriteLine();

        // Вывод второго массива
        Console.WriteLine("Массив согласных:");
        for (int i = 0; i < array2.Length; i++)
        {
            Console.Write(array2[i] + " ");
        }
        Console.WriteLine();
    }
}