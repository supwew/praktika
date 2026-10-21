using System;

namespace Contacts
{
    // Базовый абстрактный класс контакта
    public abstract class Contact
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
                    throw new ArgumentException("Имя контакта не может быть пустым.");
                }
                name = value.Trim();
            }
        }

        public string Phone { get; set; }
        public string Email { get; set; }

        // Тип контакта и дополнительные данные (у каждого вида свои)
        public abstract string Type { get; }
        public abstract string Details { get; set; }

        protected Contact(string name, string phone, string email)
        {
            Name = name;
            Phone = phone.Trim();
            Email = email.Trim();
        }

        // Краткая информация о контакте (наследники дополняют ее)
        public virtual string GetInfo()
        {
            return Name + ", тел.: " + Phone + ", email: " + Email;
        }
    }

    // Личный контакт (наследник)
    public class PersonalContact : Contact
    {
        string note;

        public PersonalContact(string name, string phone, string email, string note)
            : base(name, phone, email)
        {
            this.note = note.Trim();
        }

        public override string Type
        {
            get { return "Личный"; }
        }

        // Дополнительные данные: заметка
        public override string Details
        {
            get { return note; }
            set { note = value.Trim(); }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + ", заметка: " + note;
        }
    }

    // Рабочий контакт (наследник)
    public class WorkContact : Contact
    {
        string company;

        public WorkContact(string name, string phone, string email, string company)
            : base(name, phone, email)
        {
            this.company = company.Trim();
        }

        public override string Type
        {
            get { return "Рабочий"; }
        }

        // Дополнительные данные: компания
        public override string Details
        {
            get { return company; }
            set { company = value.Trim(); }
        }

        public override string GetInfo()
        {
            return base.GetInfo() + ", компания: " + company;
        }
    }
}