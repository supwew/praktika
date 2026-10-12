#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace zadanie3
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
            this.topPanel = new Panel();
            this.taskTextBox = new TextBox();
            this.addButton = new Button();
            this.bottomPanel = new Panel();
            this.deleteButton = new Button();
            this.counterLabel = new Label();
            this.tasksCheckedListBox = new CheckedListBox();
            this.topPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // topPanel
            // 
            this.topPanel.Controls.Add(this.taskTextBox);
            this.topPanel.Controls.Add(this.addButton);
            this.topPanel.Dock = DockStyle.Top;
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new Size(420, 40);
            this.topPanel.TabIndex = 0;
            // 
            // taskTextBox
            // 
            this.taskTextBox.Location = new Point(10, 10);
            this.taskTextBox.Name = "taskTextBox";
            this.taskTextBox.Size = new Size(280, 23);
            this.taskTextBox.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Location = new Point(300, 9);
            this.addButton.Name = "addButton";
            this.addButton.Size = new Size(100, 25);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Добавить";
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.deleteButton);
            this.bottomPanel.Controls.Add(this.counterLabel);
            this.bottomPanel.Dock = DockStyle.Bottom;
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new Size(420, 40);
            this.bottomPanel.TabIndex = 2;
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new Point(10, 8);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new Size(100, 25);
            this.deleteButton.TabIndex = 0;
            this.deleteButton.Text = "Удалить";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // counterLabel
            // 
            this.counterLabel.AutoSize = true;
            this.counterLabel.Location = new Point(120, 12);
            this.counterLabel.Name = "counterLabel";
            this.counterLabel.Text = "Выполнено: 0 из 0";
            // 
            // tasksCheckedListBox
            // 
            this.tasksCheckedListBox.CheckOnClick = true;
            this.tasksCheckedListBox.Dock = DockStyle.Fill;
            this.tasksCheckedListBox.Font = new Font("Segoe UI", 11F);
            this.tasksCheckedListBox.Name = "tasksCheckedListBox";
            this.tasksCheckedListBox.TabIndex = 1;
            this.tasksCheckedListBox.ItemCheck += new ItemCheckEventHandler(this.tasksCheckedListBox_ItemCheck);
            // 
            // Form1
            // 
            this.AcceptButton = this.addButton;
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(420, 400);
            this.Controls.Add(this.tasksCheckedListBox);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.bottomPanel);
            this.Name = "Form1";
            this.Text = "Задачи";
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.bottomPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel topPanel;
        private TextBox taskTextBox;
        private Button addButton;
        private Panel bottomPanel;
        private Button deleteButton;
        private Label counterLabel;
        private CheckedListBox tasksCheckedListBox;
    }
}