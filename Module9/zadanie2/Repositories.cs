#nullable disable
using System.Data;
using zadanie2;

namespace zadanie2
{
    // Базовый класс для работы с таблицей
    public abstract class Repository
    {
        protected Database db;

        protected Repository(Database db)
        {
            this.db = db;
        }

        // Название таблицы (задается в наследниках)
        protected abstract string TableName { get; }

        // Все записи таблицы для вывода
        public abstract DataTable GetAll();

        // Удаление записи по номеру
        public void Delete(int id)
        {
            db.Run("DELETE FROM " + TableName + " WHERE Id = @p0", id);
        }
    }

    // Сотрудники
    public class EmployeeRepository : Repository
    {
        public EmployeeRepository(Database db) : base(db) { }

        protected override string TableName
        {
            get { return "Employees"; }
        }

        public override DataTable GetAll()
        {
            return db.Run("SELECT Id AS Номер, Name AS Имя, Position AS Должность FROM Employees ORDER BY Id");
        }

        public void Add(string name, string position)
        {
            db.Run("INSERT INTO Employees (Name, Position) VALUES (@p0, @p1)", name, position);
        }

        public void Update(int id, string name, string position)
        {
            db.Run("UPDATE Employees SET Name = @p0, Position = @p1 WHERE Id = @p2", name, position, id);
        }
    }

    // Проекты
    public class ProjectRepository : Repository
    {
        public ProjectRepository(Database db) : base(db) { }

        protected override string TableName
        {
            get { return "Projects"; }
        }

        public override DataTable GetAll()
        {
            return db.Run("SELECT Id AS Номер, Name AS Проект, Description AS Описание FROM Projects ORDER BY Id");
        }

        public void Add(string name, string description)
        {
            db.Run("INSERT INTO Projects (Name, Description) VALUES (@p0, @p1)", name, description);
        }

        public void Update(int id, string name, string description)
        {
            db.Run("UPDATE Projects SET Name = @p0, Description = @p1 WHERE Id = @p2", name, description, id);
        }
    }

    // Задачи
    public class TaskRepository : Repository
    {
        public TaskRepository(Database db) : base(db) { }

        protected override string TableName
        {
            get { return "Tasks"; }
        }

        // Задачи с проектом, исполнителем, прогрессом и статусом
        public override DataTable GetAll()
        {
            return db.Run("SELECT t.Id AS Номер, t.Title AS Задача, p.Name AS Проект, " +
                          "IFNULL(e.Name, '-') AS Исполнитель, t.Progress AS Прогресс, " +
                          "CASE WHEN t.Progress = 0 THEN 'Новая' WHEN t.Progress = 100 THEN 'Выполнена' " +
                          "ELSE 'В работе' END AS Статус " +
                          "FROM Tasks t JOIN Projects p ON p.Id = t.ProjectId " +
                          "LEFT JOIN Employees e ON e.Id = t.EmployeeId ORDER BY t.Id");
        }

        public void Add(string title, int projectId)
        {
            db.Run("INSERT INTO Tasks (Title, ProjectId) VALUES (@p0, @p1)", title, projectId);
        }

        public void Update(int id, string title)
        {
            db.Run("UPDATE Tasks SET Title = @p0 WHERE Id = @p1", title, id);
        }

        // Назначение исполнителя (null - снять назначение)
        public void Assign(int taskId, int? employeeId)
        {
            db.Run("UPDATE Tasks SET EmployeeId = @p0 WHERE Id = @p1", employeeId, taskId);
        }

        // Изменение прогресса выполнения (0-100)
        public void SetProgress(int taskId, int progress)
        {
            db.Run("UPDATE Tasks SET Progress = @p0 WHERE Id = @p1", progress, taskId);
        }
    }
}