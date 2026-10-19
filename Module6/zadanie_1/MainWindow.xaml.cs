#nullable disable
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace zadanie_1
{
    public partial class MainWindow : Window
    {
        EmployeeDb db = new EmployeeDb();

        public MainWindow()
        {
            InitializeComponent();
            datePicker.SelectedDate = DateTime.Today;
            Load();
        }

        // Вывод сотрудников в таблицу
        void Load()
        {
            grid.ItemsSource = db.GetAll().DefaultView;
        }

        // Номер выбранного сотрудника, -1 если ничего не выбрано
        int SelectedId()
        {
            if (grid.SelectedItem is DataRowView row)
            {
                return Convert.ToInt32(row["Id"]);
            }
            return -1;
        }

        // Проверка полей ввода
        bool CheckFields(out double salary)
        {
            salary = 0;
            if (nameBox.Text.Trim() == "" || !double.TryParse(salaryBox.Text, out salary) || datePicker.SelectedDate == null)
            {
                MessageBox.Show("Введите ФИО, зарплату (число) и дату.");
                return false;
            }
            return true;
        }

        // Очистка полей ввода
        void ClearFields()
        {
            nameBox.Text = positionBox.Text = salaryBox.Text = "";
        }

        // Выбор строки: данные сотрудника попадают в поля
        void Grid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (grid.SelectedItem is not DataRowView row)
            {
                return;
            }
            nameBox.Text = row["FullName"].ToString();
            positionBox.Text = row["Position"].ToString();
            salaryBox.Text = row["Salary"].ToString();
            datePicker.SelectedDate = DateTime.Parse(row["HireDate"].ToString());
        }

        void Add_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckFields(out double salary))
            {
                return;
            }
            db.Add(nameBox.Text.Trim(), positionBox.Text.Trim(), salary, datePicker.SelectedDate.Value.ToString("yyyy-MM-dd"));
            Load();
            ClearFields();
        }

        void Update_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedId() == -1)
            {
                MessageBox.Show("Выберите сотрудника в таблице.");
                return;
            }
            if (!CheckFields(out double salary))
            {
                return;
            }
            db.Update(SelectedId(), nameBox.Text.Trim(), positionBox.Text.Trim(), salary, datePicker.SelectedDate.Value.ToString("yyyy-MM-dd"));
            Load();
            ClearFields();
        }

        void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedId() == -1)
            {
                MessageBox.Show("Выберите сотрудника в таблице.");
                return;
            }
            if (MessageBox.Show("Удалить сотрудника?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.Delete(SelectedId());
                Load();
                ClearFields();
            }
        }
    }
}