using System;
using System.Collections.Generic;
using System.IO;

namespace Contacts
{
    // Интерфейс хранилища контактов
    public interface IContactStorage
    {
        List<Contact> Load();
        void Save(List<Contact> contacts);
    }

    // Хранение в текстовом файле: одна строка - один контакт
    public class TextFileStorage : IContactStorage
    {
        string path = Path.Combine(AppContext.BaseDirectory, "contacts.txt");

        // Чтение контактов из файла
        public List<Contact> Load()
        {
            List<Contact> list = new List<Contact>();
            if (!File.Exists(path))
            {
                return list;
            }

            foreach (string line in File.ReadAllLines(path))
            {
                string[] parts = line.Split('|');
                if (parts.Length != 5)
                {
                    continue;
                }

                try
                {
                    list.Add(ContactBook.Create(parts[0], parts[1], parts[2], parts[3], parts[4]));
                }
                catch (ArgumentException)
                {
                    // Поврежденная строка пропускается
                }
            }
            return list;
        }

        // Запись контактов в файл
        public void Save(List<Contact> contacts)
        {
            List<string> lines = new List<string>();
            foreach (Contact c in contacts)
            {
                lines.Add(Clean(c.Type) + "|" + Clean(c.Name) + "|" + Clean(c.Phone) + "|" +
                          Clean(c.Email) + "|" + Clean(c.Details));
            }
            File.WriteAllLines(path, lines);
        }

        // Удаление символов, которые ломают формат файла
        static string Clean(string text)
        {
            return text.Replace('|', ' ').Replace('\r', ' ').Replace('\n', ' ');
        }
    }
}