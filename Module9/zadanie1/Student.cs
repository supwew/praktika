#nullable disable
using System;
using System.Globalization;

namespace zadanie1
{
    // Человек (базовый класс)
    public abstract class Person
    {
        string name = "";   // Закрытое поле

        // Имя: пустое значение не допускается
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя не может быть пустым.");
                }
                name = value.Trim();
            }
        }

        public int Age { get; set; }

        protected Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    // Студент (наследник класса Person)
    public class Student : Person
    {
        public string Group { get; set; }
        public double AverageGrade { get; set; }

        public Student(string name, int age, string group, double averageGrade) : base(name, age)
        {
            Group = group;
            AverageGrade = averageGrade;
        }

        // Строка для вывода в таблицу
        public override string ToString()
        {
            return Name.PadRight(25) + Group.PadRight(10) + Age.ToString().PadRight(9) + AverageGrade.ToString("F1");
        }

        // Строка для сохранения в файл
        public string ToFileLine()
        {
            return Name.Replace('|', ' ') + "|" + Age + "|" + Group.Replace('|', ' ') + "|" +
                   AverageGrade.ToString(CultureInfo.InvariantCulture);
        }

        // Создание студента из строки файла
        public static Student FromFileLine(string line)
        {
            string[] p = line.Split('|');
            return new Student(p[0], int.Parse(p[1]), p[2], double.Parse(p[3], CultureInfo.InvariantCulture));
        }
    }
}