#nullable disable
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace zadanie4
{
    public partial class MainWindow : Window
    {
        FinanceDb db = new FinanceDb();

        public MainWindow()
        {
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Today;
            Load();
        }

         //Вывод записей в таблицу и итогов
        void Load()
        {
            grid.ItemsSource = db.GetAll().DefaultView;
        
            double income = db.Sum("Доход");
            double expense = db.Sum("Расход");
            summaryText.Text = "Доходы: " + income + "   Расходы: " + expense + "   Баланс: " + (income - expense);
        }
       
        // Номер выбранной записи, -1 если ничего не выбрано
        int SelectedId()
        {
            if (grid.SelectedItem is DataRowView row)
            {
                return Convert.ToInt32(row["Id"]);
            }
            return -1;
        }

        // Проверка полей ввода
        bool CheckFields(out double amount)
        {
            amount = 0;
            if (categoryBox.Text.Trim() == "" || !double.TryParse(amountBox.Text, out amount)
                || amount <= 0 || datePicker.SelectedDate == null)
            {
                MessageBox.Show("Введите категорию, сумму (число больше 0) и дату.");
                return false;
            }
            return true;
        }

        // Тип записи из списка
        string RecordType()
        {
            return typeBox.SelectedIndex == 0 ? "Доход" : "Расход";
        }

        // Очистка полей ввода
        void ClearFields()
        {
            categoryBox.Text = amountBox.Text = "";
        }

        // Выбор строки: данные записи попадают в поля
        void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid.SelectedItem is not DataRowView row)
            {
                return;
            }
            typeBox.SelectedIndex = row["Type"].ToString() == "Доход" ? 0 : 1;
            categoryBox.Text = row["Category"].ToString();
            amountBox.Text = row["Amount"].ToString();
            datePicker.SelectedDate = DateTime.Parse(row["RecordDate"].ToString());
        }

        void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFields(out double amount))
            {
                return;
            }
            db.Add(RecordType(), categoryBox.Text.Trim(), amount, datePicker.SelectedDate.Value.ToString("yyyy-MM-dd"));
            Load();
            ClearFields();
        }

        void Update_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedId() == -1)
            {
                MessageBox.Show("Выберите запись в таблице.");
                return;
            }
            if (!CheckFields(out double amount))
            {
                return;
            }
            db.Update(SelectedId(), RecordType(), categoryBox.Text.Trim(), amount, datePicker.SelectedDate.Value.ToString("yyyy-MM-dd"));
            Load();
            ClearFields();
        }

        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedId() == -1)
            {
                MessageBox.Show("Выберите запись в таблице.");
                return;
            }
            if (MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.Delete(SelectedId());
                Load();
                ClearFields();
            }
        }
    }
}