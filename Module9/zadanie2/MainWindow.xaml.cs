#nullable disable
using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using zadanie2;

namespace zadanie2
{
    public partial class MainWindow : Window
    {
        Database db = new Database("projects.db");
        EmployeeRepository employees;
        ProjectRepository projects;
        TaskRepository tasks;
        ReportService reports;

        public MainWindow()
        {
            InitializeComponent();
            employees = new EmployeeRepository(db);
            projects = new ProjectRepository(db);
            tasks = new TaskRepository(db);
            reports = new ReportService(db, tasks);
            ShowAll();
        }

        // Обновление всех таблиц и списков
        void ShowAll()
        {
            empGrid.ItemsSource = employees.GetAll().DefaultView;
            projGrid.ItemsSource = projects.GetAll().DefaultView;
            taskGrid.ItemsSource = tasks.GetAll().DefaultView;
            projectBox.ItemsSource = projects.GetAll().DefaultView;
            employeeBox.ItemsSource = employees.GetAll().DefaultView;
            reportProjectsGrid.ItemsSource = reports.ProjectsReport().DefaultView;
            reportStatusGrid.ItemsSource = reports.StatusReport().DefaultView;
        }

        // Номер выбранной строки таблицы (-1, если ничего не выбрано)
        int SelectedId(DataGrid grid)
        {
            if (grid.SelectedItem is DataRowView row)
            {
                return Convert.ToInt32(row["Номер"]);
            }
            MessageBox.Show("Выберите строку в таблице.");
            return -1;
        }

        // Проверка: поле заполнено
        bool Filled(TextBox box)
        {
            if (box.Text.Trim() != "")
            {
                return true;
            }
            MessageBox.Show("Заполните название (имя).");
            return false;
        }

        // Удаление выбранной записи с подтверждением
        void Delete(DataGrid grid, Repository repository)
        {
            int id = SelectedId(grid);
            if (id != -1 && MessageBox.Show("Удалить запись?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                repository.Delete(id);
                ShowAll();
            }
        }

        // ----- Сотрудники -----
        void EmpGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (empGrid.SelectedItem is DataRowView row)
            {
                empNameBox.Text = row["Имя"].ToString();
                empPositionBox.Text = row["Должность"].ToString();
            }
        }

        void EmpAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Filled(empNameBox))
            {
                employees.Add(empNameBox.Text.Trim(), empPositionBox.Text.Trim());
                ShowAll();
            }
        }

        void EmpUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = SelectedId(empGrid);
            if (id != -1 && Filled(empNameBox))
            {
                employees.Update(id, empNameBox.Text.Trim(), empPositionBox.Text.Trim());
                ShowAll();
            }
        }

        void EmpDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete(empGrid, employees);
        }

        // ----- Проекты -----
        void ProjGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (projGrid.SelectedItem is DataRowView row)
            {
                projNameBox.Text = row["Проект"].ToString();
                projDescBox.Text = row["Описание"].ToString();
            }
        }

        void ProjAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Filled(projNameBox))
            {
                projects.Add(projNameBox.Text.Trim(), projDescBox.Text.Trim());
                ShowAll();
            }
        }

        void ProjUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = SelectedId(projGrid);
            if (id != -1 && Filled(projNameBox))
            {
                projects.Update(id, projNameBox.Text.Trim(), projDescBox.Text.Trim());
                ShowAll();
            }
        }

        void ProjDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete(projGrid, projects);
        }

        // ----- Задачи -----
        void TaskGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (taskGrid.SelectedItem is DataRowView row)
            {
                taskTitleBox.Text = row["Задача"].ToString();
                progressSlider.Value = Convert.ToDouble(row["Прогресс"]);
            }
        }

        void TaskAdd_Click(object sender, RoutedEventArgs e)
        {
            if (projectBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите проект.");
            }
            else if (Filled(taskTitleBox))
            {
                tasks.Add(taskTitleBox.Text.Trim(), Convert.ToInt32(projectBox.SelectedValue));
                ShowAll();
            }
        }

        void TaskUpdate_Click(object sender, RoutedEventArgs e)
        {
            int id = SelectedId(taskGrid);
            if (id != -1 && Filled(taskTitleBox))
            {
                tasks.Update(id, taskTitleBox.Text.Trim());
                ShowAll();
            }
        }

        void TaskDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete(taskGrid, tasks);
        }

        // Назначение исполнителя выбранной задаче
        void Assign_Click(object sender, RoutedEventArgs e)
        {
            int id = SelectedId(taskGrid);
            if (id == -1)
            {
                return;
            }
            if (employeeBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя.");
                return;
            }
            tasks.Assign(id, Convert.ToInt32(employeeBox.SelectedValue));
            ShowAll();
        }

        void Unassign_Click(object sender, RoutedEventArgs e)
        {
            int id = SelectedId(taskGrid);
            if (id != -1)
            {
                tasks.Assign(id, null);
                ShowAll();
            }
        }

        // Установка прогресса выполнения
        void Progress_Click(object sender, RoutedEventArgs e)
        {
            int id = SelectedId(taskGrid);
            if (id != -1)
            {
                tasks.SetProgress(id, (int)progressSlider.Value);
                ShowAll();
            }
        }

        // ----- Отчет -----
        void Report_Click(object sender, RoutedEventArgs e)
        {
            ShowAll();
            string path = Path.Combine(AppContext.BaseDirectory, "report.txt");
            File.WriteAllText(path, reports.ToText());
            MessageBox.Show("Отчет сохранен: " + path);
        }
    }
}