using System;

// Интерфейс студента
interface IStudent
{
    string Name { get; }
    double GetAverageGrade();
    string GetCourseInfo();
}

// Общая часть для всех студентов: имя и оценки
abstract class StudentBase : IStudent
{
    int[] grades;

    protected StudentBase(string name, int[] grades)
    {
        Name = name;
        this.grades = grades;
    }

    public string Name { get; }

    // Средний балл
    public double GetAverageGrade()
    {
        int sum = 0;
        foreach (int g in grades)
        {
            sum += g;
        }
        return (double)sum / grades.Length;
    }

    // Информация о курсе (у каждого курса своя)
    public abstract string GetCourseInfo();
}

// Студент 1 курса
class FirstCourseStudent : StudentBase
{
    public FirstCourseStudent(string name, int[] grades) : base(name, grades) { }

    public override string GetCourseInfo()
    {
        return "1 курс (математика, информатика, физика)";
    }
}

// Студент 2 курса
class SecondCourseStudent : StudentBase
{
    public SecondCourseStudent(string name, int[] grades) : base(name, grades) { }

    public override string GetCourseInfo()
    {
        return "2 курс (алгоритмы, базы данных, английский язык)";
    }
}

// Студент 3 курса
class ThirdCourseStudent : StudentBase
{
    public ThirdCourseStudent(string name, int[] grades) : base(name, grades) { }

    public override string GetCourseInfo()
    {
        return "3 курс (программная инженерия, сети, проектирование)";
    }
}

class Program
{
    static void Main()
    {
        // Студенты разных курсов
        IStudent[] students =
        {
            new FirstCourseStudent("Иван Петров", new int[] { 8, 9, 7, 10 }),
            new SecondCourseStudent("Анна Сидорова", new int[] { 9, 10, 9, 10 }),
            new ThirdCourseStudent("Олег Смирнов", new int[] { 6, 7, 8, 7 })
        };

        // Вывод среднего балла и информации о курсе
        foreach (IStudent s in students)
        {
            Console.WriteLine(s.Name + ": средний балл " + s.GetAverageGrade().ToString("F2") + ", " + s.GetCourseInfo());
        }
    }
}