using System;
using System.Collections.Generic;
using System.Linq;

namespace Contacts
{
    public class ContactBook
    {
        List<Contact> contacts;
        IContactStorage storage;

        public ContactBook(IContactStorage storage)
        {
            this.storage = storage;
            contacts = storage.Load();
        }

        public static Contact Create(string type, string name, string phone, string email, string details, bool isFavorite)
        {
            switch (type)
            {
                case "Личный":
                    return new PersonalContact(name, phone, email, details, isFavorite);
                case "Рабочий":
                    return new WorkContact(name, phone, email, details, isFavorite);
                default:
                    throw new ArgumentException("Неизвестный тип контакта.");
            }
        }

        public void Add(Contact contact)
        {
            contacts.Add(contact);
            storage.Save(contacts);
        }

        public void Update(Contact oldContact, Contact newContact)
        {
            int index = contacts.IndexOf(oldContact);
            if (index != -1)
            {
                contacts[index] = newContact;
                storage.Save(contacts);
            }
        }

        public void Delete(Contact contact)
        {
            contacts.Remove(contact);
            storage.Save(contacts);
        }

        public List<Contact> Search(string name)
        {
            return contacts
                .Where(c => c.Name.Contains(name.Trim(), StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(c => c.IsFavorite) // Избранные всегда сверху
                .ThenBy(c => c.Name)
                .ToList();
        }
    }
}