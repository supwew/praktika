using System;
using System.Text.RegularExpressions;

namespace Contacts
{
    public abstract class Contact
    {
        string name = "";
        string phone = "";
        string email = "";
        bool isFavorite = false;

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя контакта не может быть пустым.");
                }
                name = value.Trim();
            }
        }

        // Проверка телефона: разрешены цифры, плюсы, скобки, дефисы и пробелы. Буквы запрещены!
        public string Phone
        {
            get { return phone; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Телефон не может быть пустым.");
                }
                string trimmed = value.Trim();
                if (!Regex.IsMatch(trimmed, @"^[0-9\+\-\(\)\s]+$"))
                {
                    throw new ArgumentException("Номер телефона может содержать только цифры, знаки +, -, скобки и пробелы.");
                }
                phone = trimmed;
            }
        }

        // Проверка email на корректность
        public string Email
        {
            get { return email; }
            set
            {
                string trimmed = value.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    if (!Regex.IsMatch(trimmed, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    {
                        throw new ArgumentException("У введенного email некорректный формат.");
                    }
                }
                email = trimmed;
            }
        }

        public bool IsFavorite
        {
            get { return isFavorite; }
            set { isFavorite = value; }
        }

        public abstract string Type { get; }
        public abstract string Details { get; set; }

        protected Contact(string name, string phone, string email, bool isFavorite = false)
        {
            Name = name;
            Phone = phone;
            Email = email;
            IsFavorite = isFavorite;
        }

        public virtual string GetInfo()
        {
            string favStr = IsFavorite ? "⭐ [Избранное]" : "";
            return Name + " " + favStr + ", тел.: " + Phone + ", email: " + (string.IsNullOrEmpty(Email) ? "нет" : Email);
        }
    }

    public class PersonalContact : Contact
    {
        string note;

        public PersonalContact(string name, string phone, string email, string note, bool isFavorite = false)
            : base(name, phone, email, isFavorite)
        {
            this.note = note != null ? note.Trim() : "";
        }

        public override string Type => "Личный";

        public override string Details
        {
            get => note;
            set => note = value != null ? value.Trim() : "";
        }

        public override string GetInfo()
        {
            return base.GetInfo() + ", заметка: " + note;
        }
    }

    public class WorkContact : Contact
    {
        string company;

        public WorkContact(string name, string phone, string email, string company, bool isFavorite = false)
            : base(name, phone, email, isFavorite)
        {
            this.company = company != null ? company.Trim() : "";
        }

        public override string Type => "Рабочий";

        public override string Details
        {
            get => company;
            set => company = value != null ? value.Trim() : "";
        }

        public override string GetInfo()
        {
            return base.GetInfo() + ", компания: " + company;
        }
    }
}