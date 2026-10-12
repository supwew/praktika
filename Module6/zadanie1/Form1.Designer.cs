#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace zadanie1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.employeesGridView = new DataGridView();
            this.bottomPanel = new Panel();
            this.nameLabel = new Label();
            this.nameTextBox = new TextBox();
            this.positionLabel = new Label();
            this.positionTextBox = new TextBox();
            this.salaryLabel = new Label();
            this.salaryTextBox = new TextBox();
            this.hireDateLabel = new Label();
            this.hireDatePicker = new DateTimePicker();
            this.addButton = new Button();
            this.updateButton = new Button();
            this.deleteButton = new Button();
            ((System.ComponentModel.ISupportInitialize)(this.employeesGridView)).BeginInit();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // employeesGridView
            // 
            this.employeesGridView.AllowUserToAddRows = false;
            this.employeesGridView.AllowUserToDeleteRows = false;
            this.employeesGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.employeesGridView.Dock = DockStyle.Fill;
            this.employeesGridView.MultiSelect = false;
            this.employeesGridView.Name = "employeesGridView";
            this.employeesGridView.ReadOnly = true;
            this.employeesGridView.RowHeadersVisible = false;
            this.employeesGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.employeesGridView.TabIndex = 0;
            this.employeesGridView.SelectionChanged += new System.EventHandler(this.employeesGridView_SelectionChanged);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.nameLabel);
            this.bottomPanel.Controls.Add(this.nameTextBox);
            this.bottomPanel.Controls.Add(this.positionLabel);
            this.bottomPanel.Controls.Add(this.positionTextBox);
            this.bottomPanel.Controls.Add(this.salaryLabel);
            this.bottomPanel.Controls.Add(this.salaryTextBox);
            this.bottomPanel.Controls.Add(this.hireDateLabel);
            this.bottomPanel.Controls.Add(this.hireDatePicker);
            this.bottomPanel.Controls.Add(this.addButton);
            this.bottomPanel.Controls.Add(this.updateButton);
            this.bottomPanel.Controls.Add(this.deleteButton);
            this.bottomPanel.Dock = DockStyle.Bottom;
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new Size(700, 130);
            this.bottomPanel.TabIndex = 1;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new Point(10, 18);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Text = "ФИО";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new Point(100, 15);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new Size(250, 23);
            this.nameTextBox.TabIndex = 0;
            // 
            // positionLabel
            // 
            this.positionLabel.AutoSize = true;
            this.positionLabel.Location = new Point(370, 18);
            this.positionLabel.Name = "positionLabel";
            this.positionLabel.Text = "Должность";
            // 
            // positionTextBox
            // 
            this.positionTextBox.Location = new Point(450, 15);
            this.positionTextBox.Name = "positionTextBox";
            this.positionTextBox.Size = new Size(230, 23);
            this.positionTextBox.TabIndex = 1;
            // 
            // salaryLabel
            // 
            this.salaryLabel.AutoSize = true;
            this.salaryLabel.Location = new Point(10, 53);
            this.salaryLabel.Name = "salaryLabel";
            this.salaryLabel.Text = "Зарплата";
            // 
            // salaryTextBox
            // 
            this.salaryTextBox.Location = new Point(100, 50);
            this.salaryTextBox.Name = "salaryTextBox";
            this.salaryTextBox.Size = new Size(120, 23);
            this.salaryTextBox.TabIndex = 2;
            // 
            // hireDateLabel
            // 
            this.hireDateLabel.AutoSize = true;
            this.hireDateLabel.Location = new Point(370, 53);
            this.hireDateLabel.Name = "hireDateLabel";
            this.hireDateLabel.Text = "Дата приема";
            // 
            // hireDatePicker
            // 
            this.hireDatePicker.Format = DateTimePickerFormat.Short;
            this.hireDatePicker.Location = new Point(450, 50);
            this.hireDatePicker.Name = "hireDatePicker";
            this.hireDatePicker.Size = new Size(120, 23);
            this.hireDatePicker.TabIndex = 3;
            // 
            // addButton
            // 
            this.addButton.Location = new Point(10, 90);
            this.addButton.Name = "addButton";
            this.addButton.Size = new Size(110, 28);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Добавить";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new Point(130, 90);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new Size(110, 28);
            this.updateButton.TabIndex = 5;
            this.updateButton.Text = "Изменить";
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new Point(250, 90);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new Size(110, 28);
            this.deleteButton.TabIndex = 6;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(700, 480);
            this.Controls.Add(this.employeesGridView);
            this.Controls.Add(this.bottomPanel);
            this.Name = "Form1";
            this.Text = "Сотрудники";
            ((System.ComponentModel.ISupportInitialize)(this.employeesGridView)).EndInit();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private DataGridView employeesGridView;
        private Panel bottomPanel;
        private Label nameLabel;
        private TextBox nameTextBox;
        private Label positionLabel;
        private TextBox positionTextBox;
        private Label salaryLabel;
        private TextBox salaryTextBox;
        private Label hireDateLabel;
        private DateTimePicker hireDatePicker;
        private Button addButton;
        private Button updateButton;
        private Button deleteButton;
    }
}