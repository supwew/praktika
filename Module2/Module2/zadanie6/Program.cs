using System;

// Класс студента
class Student
{
    // Свойства
    public string Name { get; set; } = "";
    public string Surname { get; set; } = "";
    public int Age { get; set; }
    public double AverageGrade { get; set; }

    // Вывод информации о студенте
    public void Print()
    {
        Console.WriteLine(Surname + " " + Name + ", возраст: " + Age + ", средний балл: " + AverageGrade);
    }
}

class Program
{
    static void Main()
    {
        // Создание нескольких студентов
        Student[] students =
        {
            new Student { Name = "Иван", Surname = "Петров", Age = 19, AverageGrade = 9.5 },
            new Student { Name = "Анна", Surname = "Сидорова", Age = 18, AverageGrade = 9.8 },
            new Student { Name = "Олег", Surname = "Смирнов", Age = 16, AverageGrade = 3.9 },
            new Student { Name = "Мария", Surname = "Кузнецова", Age = 17, AverageGrade = 4.2 }
        };

        // Вывод информации о студентах
        foreach (Student student in students)
        {
            student.Print();
        }
    }
}