using System;
using System.Windows.Forms;

namespace zadanie5
{
    public partial class Form1 : Form
    {
        Calculator calculator = new Calculator();

        public Form1()
        {
            InitializeComponent();
            commaButton.Text = calculator.Separator;
        }

        // Нажатие любой кнопки калькулятора
        private void button_Click(object sender, EventArgs e)
        {
            displayTextBox.Text = calculator.Press(((Button)sender).Text);
        }
    }
}