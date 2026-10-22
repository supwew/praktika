#nullable disable
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using zadanie1;

namespace zadanie1
{
    // Список студентов: изменение, поиск, сортировка, работа с файлом
    public class StudentList
    {
        List<Student> students = new List<Student>();   // Закрытый список

        public int Count
        {
            get { return students.Count; }
        }

        // Копия списка для просмотра
        public List<Student> GetAll()
        {
            return new List<Student>(students);
        }

        public void Add(Student student)
        {
            students.Add(student);
        }

        // Удаление по номеру (с 1)
        public void Delete(int number)
        {
            students.RemoveAt(number - 1);
        }

        // Замена студента новым (редактирование)
        public void Update(int number, Student student)
        {
            students[number - 1] = student;
        }

        // Поиск по части имени (регистр не важен)
        public List<Student> FindByName(string text)
        {
            return students.Where(s => s.Name.Contains(text, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Поиск по группе
        public List<Student> FindByGroup(string group)
        {
            return students.Where(s => s.Group.Equals(group, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Сортировка: 1 - по имени, 2 - по группе, 3 - по возрасту, 4 - по среднему баллу (по убыванию)
        public void Sort(int criterion)
        {
            switch (criterion)
            {
                case 1:
                    students = students.OrderBy(s => s.Name).ToList();
                    break;
                case 2:
                    students = students.OrderBy(s => s.Group).ThenBy(s => s.Name).ToList();
                    break;
                case 3:
                    students = students.OrderBy(s => s.Age).ToList();
                    break;
                case 4:
                    students = students.OrderByDescending(s => s.AverageGrade).ToList();
                    break;
            }
        }

        // Сохранение списка в файл
        public void Save(string path)
        {
            File.WriteAllLines(path, students.Select(s => s.ToFileLine()));
        }

        // Загрузка списка из файла (поврежденные строки пропускаются)
        public void Load(string path)
        {
            students = new List<Student>();
            foreach (string line in File.ReadAllLines(path))
            {
                try
                {
                    students.Add(Student.FromFileLine(line));
                }
                catch (Exception)
                {
                    // Поврежденная строка пропускается
                }
            }
        }
    }
}