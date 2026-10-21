using System;
using System.Collections.Generic;
using System.Linq;

namespace Contacts
{
    // Книга контактов: добавление, изменение, удаление и поиск
    public class ContactBook
    {
        List<Contact> contacts;       // Закрытый список
        IContactStorage storage;      // Хранилище (интерфейс)

        public ContactBook(IContactStorage storage)
        {
            this.storage = storage;
            contacts = storage.Load();
        }

        // Создание контакта нужного вида
        public static Contact Create(string type, string name, string phone, string email, string details)
        {
            switch (type)
            {
                case "Личный":
                    return new PersonalContact(name, phone, email, details);
                case "Рабочий":
                    return new WorkContact(name, phone, email, details);
                default:
                    throw new ArgumentException("Неизвестный тип контакта.");
            }
        }

        // Добавление контакта
        public void Add(Contact contact)
        {
            contacts.Add(contact);
            storage.Save(contacts);
        }

        // Замена контакта новым (редактирование)
        public void Update(Contact oldContact, Contact newContact)
        {
            int index = contacts.IndexOf(oldContact);
            if (index != -1)
            {
                contacts[index] = newContact;
                storage.Save(contacts);
            }
        }

        // Удаление контакта
        public void Delete(Contact contact)
        {
            contacts.Remove(contact);
            storage.Save(contacts);
        }

        // Поиск по имени (регистр не важен, пустой запрос возвращает все контакты)
        public List<Contact> Search(string name)
        {
            return contacts
                .Where(c => c.Name.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Name)
                .ToList();
        }
    }
}