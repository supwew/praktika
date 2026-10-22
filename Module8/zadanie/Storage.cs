using System;
using System.Collections.Generic;
using System.IO;

namespace Contacts
{
    public interface IContactStorage
    {
        List<Contact> Load();
        void Save(List<Contact> contacts);
    }

    public class TextFileStorage : IContactStorage
    {
        string path = Path.Combine(AppContext.BaseDirectory, "contacts.txt");

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
                if (parts.Length != 6)
                {
                    continue;
                }

                try
                {
                    bool isFav = bool.TryParse(parts[5], out bool res) && res;
                    list.Add(ContactBook.Create(parts[0], parts[1], parts[2], parts[3], parts[4], isFav));
                }
                catch (ArgumentException)
                {
                    // Пропускаем поврежденные строки
                }
            }
            return list;
        }

        public void Save(List<Contact> contacts)
        {
            List<string> lines = new List<string>();
            foreach (Contact c in contacts)
            {
                lines.Add(Clean(c.Type) + "|" + Clean(c.Name) + "|" + Clean(c.Phone) + "|" +
                          Clean(c.Email) + "|" + Clean(c.Details) + "|" + c.IsFavorite);
            }
            File.WriteAllLines(path, lines);
        }

        static string Clean(string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            return text.Replace('|', ' ').Replace('\r', ' ').Replace('\n', ' ');
        }
    }
}