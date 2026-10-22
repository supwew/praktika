using System;
using System.Windows;
using System.Windows.Controls;

namespace Contacts
{
    public partial class MainWindow : Window
    {
        ContactBook book = new ContactBook(new TextFileStorage());

        public MainWindow()
        {
            InitializeComponent();
            ShowContacts();
        }

        void ShowContacts()
        {
            grid.ItemsSource = book.Search(searchBox.Text);
        }

        string SelectedType()
        {
            return typeBox.SelectedIndex == 0 ? "Личный" : "Рабочий";
        }

        Contact ReadContact()
        {
            try
            {
                bool isFav = favoriteCheck.IsChecked ?? false;
                return ContactBook.Create(SelectedType(), nameBox.Text, phoneBox.Text, emailBox.Text, detailsBox.Text, isFav);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }
        }

        void ClearFields()
        {
            nameBox.Text = phoneBox.Text = emailBox.Text = detailsBox.Text = "";
            favoriteCheck.IsChecked = false;
            infoText.Text = "";
        }

        void Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (detailsLabel != null)
            {
                detailsLabel.Text = typeBox.SelectedIndex == 0 ? "Заметка" : "Компания";
            }
        }

        void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            ShowContacts();
        }

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
            favoriteCheck.IsChecked = contact.IsFavorite;
            infoText.Text = contact.GetInfo();
        }

        void Add_Click(object sender, RoutedEventArgs e)
        {
            Contact contact = ReadContact();
            if (contact == null) return;
            book.Add(contact);
            ShowContacts();
            ClearFields();
        }

        void Update_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is not Contact selected)
            {
                MessageBox.Show("Выберите контакт в таблице.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Contact contact = ReadContact();
            if (contact == null) return;
            book.Update(selected, contact);
            ShowContacts();
            ClearFields();
        }

        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (grid.SelectedItem is not Contact selected)
            {
                MessageBox.Show("Выберите контакт в таблице.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show("Удалить контакт?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                book.Delete(selected);
                ShowContacts();
                ClearFields();
            }
        }
    }
}