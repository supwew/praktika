using System;
using System.Windows;
using System.Windows.Controls;

namespace Contacts
{
    public partial class MainWindow : Window
    {
        // Вся логика находится в классе ContactBook
        ContactBook book = new ContactBook(new TextFileStorage());

        public MainWindow()
        {
            InitializeComponent();
            ShowContacts();
        }

        // Вывод контактов (с учетом поиска) в таблицу
        void ShowContacts()
        {
            grid.ItemsSource = book.Search(searchBox.Text);
        }

        // Тип контакта из списка
        string SelectedType()
        {
            return typeBox.SelectedIndex == 0 ? "Личный" : "Рабочий";
        }

        // Создание контакта из полей формы (при пустом имени сообщение об ошибке)
        Contact ReadContact()
        {
            try
            {
                return ContactBook.Create(SelectedType(), nameBox.Text, phoneBox.Text, emailBox.Text, detailsBox.Text);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        // Очистка полей ввода
        void ClearFields()
        {
            nameBox.Text = phoneBox.Text = emailBox.Text = detailsBox.Text = "";
            infoText.Text = "";
        }

        // Смена типа: меняется подпись дополнительного поля
        void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (detailsLabel != null)
            {
                detailsLabel.Text = typeBox.SelectedIndex == 0 ? "Заметка" : "Компания";
            }
        }

        // Поиск по имени при вводе текста
        void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowContacts();
        }

        // Выбор строки: данные контакта попадают в поля
        void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid.SelectedItem is not Contact contact)
            {
                return;
            }
            typeBox.SelectedIndex = contact.Type == "Личный" ? 0 : 1;
            nameBox.Text = contact.Name;
            phoneBox.Text = contact.Phone;
            emailBox.Text = contact.Email;
            detailsBox.Text = contact.Details;
            infoText.Text = contact.GetInfo();
        }

        // Добавление контакта
        void Add_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = ReadContact();
            if (contact == null)
            {
                return;
            }
            book.Add(contact);
            ShowContacts();
            ClearFields();
        }

        // Изменение выбранного контакта
        void Update_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is not Contact selected)
            {
                MessageBox.Show("Выберите контакт в таблице.");
                return;
            }
            Contact contact = ReadContact();
            if (contact == null)
            {
                return;
            }
            book.Update(selected, contact);
            ShowContacts();
            ClearFields();
        }

        // Удаление выбранного контакта
        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is not Contact selected)
            {
                MessageBox.Show("Выберите контакт в таблице.");
                return;
            }
            if (MessageBox.Show("Удалить контакт?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                book.Delete(selected);
                ShowContacts();
                ClearFields();
            }
        }
    }
}