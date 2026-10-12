#nullable disable
using System.Drawing;
using System.Windows.Forms;

namespace zadanie5
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
            this.displayTextBox = new TextBox();
            this.tableLayoutPanel1 = new TableLayoutPanel();
            this.clearButton = new Button();
            this.button7 = new Button();
            this.button8 = new Button();
            this.button9 = new Button();
            this.divideButton = new Button();
            this.button4 = new Button();
            this.button5 = new Button();
            this.button6 = new Button();
            this.multiplyButton = new Button();
            this.button1 = new Button();
            this.button2 = new Button();
            this.button3 = new Button();
            this.minusButton = new Button();
            this.button0 = new Button();
            this.commaButton = new Button();
            this.equalsButton = new Button();
            this.plusButton = new Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // displayTextBox
            // 
            this.displayTextBox.Dock = DockStyle.Top;
            this.displayTextBox.Font = new Font("Segoe UI", 22F);
            this.displayTextBox.Name = "displayTextBox";
            this.displayTextBox.ReadOnly = true;
            this.displayTextBox.TabIndex = 1;
            this.displayTextBox.Text = "0";
            this.displayTextBox.TextAlign = HorizontalAlignment.Right;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.clearButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button7, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button8, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button9, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.divideButton, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.button4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button5, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.button6, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.multiplyButton, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.button3, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.minusButton, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.button0, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.commaButton, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.equalsButton, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.plusButton, 3, 4);
            this.tableLayoutPanel1.Dock = DockStyle.Fill;
            this.tableLayoutPanel1.Font = new Font("Segoe UI", 16F);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // Кнопки калькулятора (все с одним обработчиком button_Click)
            // 
            this.clearButton.Dock = DockStyle.Fill;
            this.clearButton.Name = "clearButton";
            this.clearButton.Text = "C";
            this.clearButton.Click += new System.EventHandler(this.button_Click);
            this.tableLayoutPanel1.SetColumnSpan(this.clearButton, 4);

            this.button7.Dock = DockStyle.Fill;
            this.button7.Name = "button7";
            this.button7.Text = "7";
            this.button7.Click += new System.EventHandler(this.button_Click);

            this.button8.Dock = DockStyle.Fill;
            this.button8.Name = "button8";
            this.button8.Text = "8";
            this.button8.Click += new System.EventHandler(this.button_Click);

            this.button9.Dock = DockStyle.Fill;
            this.button9.Name = "button9";
            this.button9.Text = "9";
            this.button9.Click += new System.EventHandler(this.button_Click);

            this.divideButton.Dock = DockStyle.Fill;
            this.divideButton.Name = "divideButton";
            this.divideButton.Text = "/";
            this.divideButton.Click += new System.EventHandler(this.button_Click);

            this.button4.Dock = DockStyle.Fill;
            this.button4.Name = "button4";
            this.button4.Text = "4";
            this.button4.Click += new System.EventHandler(this.button_Click);

            this.button5.Dock = DockStyle.Fill;
            this.button5.Name = "button5";
            this.button5.Text = "5";
            this.button5.Click += new System.EventHandler(this.button_Click);

            this.button6.Dock = DockStyle.Fill;
            this.button6.Name = "button6";
            this.button6.Text = "6";
            this.button6.Click += new System.EventHandler(this.button_Click);

            this.multiplyButton.Dock = DockStyle.Fill;
            this.multiplyButton.Name = "multiplyButton";
            this.multiplyButton.Text = "*";
            this.multiplyButton.Click += new System.EventHandler(this.button_Click);

            this.button1.Dock = DockStyle.Fill;
            this.button1.Name = "button1";
            this.button1.Text = "1";
            this.button1.Click += new System.EventHandler(this.button_Click);

            this.button2.Dock = DockStyle.Fill;
            this.button2.Name = "button2";
            this.button2.Text = "2";
            this.button2.Click += new System.EventHandler(this.button_Click);

            this.button3.Dock = DockStyle.Fill;
            this.button3.Name = "button3";
            this.button3.Text = "3";
            this.button3.Click += new System.EventHandler(this.button_Click);

            this.minusButton.Dock = DockStyle.Fill;
            this.minusButton.Name = "minusButton";
            this.minusButton.Text = "-";
            this.minusButton.Click += new System.EventHandler(this.button_Click);

            this.button0.Dock = DockStyle.Fill;
            this.button0.Name = "button0";
            this.button0.Text = "0";
            this.button0.Click += new System.EventHandler(this.button_Click);

            this.commaButton.Dock = DockStyle.Fill;
            this.commaButton.Name = "commaButton";
            this.commaButton.Text = ",";
            this.commaButton.Click += new System.EventHandler(this.button_Click);

            this.equalsButton.Dock = DockStyle.Fill;
            this.equalsButton.Name = "equalsButton";
            this.equalsButton.Text = "=";
            this.equalsButton.Click += new System.EventHandler(this.button_Click);

            this.plusButton.Dock = DockStyle.Fill;
            this.plusButton.Name = "plusButton";
            this.plusButton.Text = "+";
            this.plusButton.Click += new System.EventHandler(this.button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(300, 400);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.displayTextBox);
            this.Name = "Form1";
            this.Text = "Калькулятор";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TextBox displayTextBox;
        private TableLayoutPanel tableLayoutPanel1;
        private Button clearButton;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button divideButton;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button multiplyButton;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button minusButton;
        private Button button0;
        private Button commaButton;
        private Button equalsButton;
        private Button plusButton;
    }
}