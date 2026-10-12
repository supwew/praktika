using System;
using System.Globalization;
using System.Windows.Forms;

namespace zadanie5
{
    public partial class Form1 : Form
    {
        // Разделитель дробной части по настройкам Windows (запятая или точка)
        string sep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        double first = 0;          // Первое число
        string op = "";            // Выбранная операция
        bool newNumber = true;     // Следующая цифра начинает новое число

        public Form1()
        {
            InitializeComponent();
            commaButton.Text = sep;
        }

        // Нажатие любой кнопки калькулятора
        private void button_Click(object sender, EventArgs e)
        {
            string text = ((Button)sender).Text;

            if (text == "C")
            {
                // Сброс
                displayTextBox.Text = "0";
                first = 0;
                op = "";
                newNumber = true;
            }
            else if (char.IsDigit(text[0]) || text == sep)
            {
                // Ввод цифры или десятичного разделителя
                if (newNumber)
                {
                    displayTextBox.Text = "0";
                    newNumber = false;
                }

                if (text == sep)
                {
                    if (!displayTextBox.Text.Contains(sep))
                    {
                        displayTextBox.Text += sep;
                    }
                }
                else if (displayTextBox.Text == "0")
                {
                    displayTextBox.Text = text;
                }
                else
                {
                    displayTextBox.Text += text;
                }
            }
            else if (text == "=")
            {
                // Вычисление результата
                if (op != "")
                {
                    Calculate();
                    op = "";
                    newNumber = true;
                }
            }
            else
            {
                // Выбор операции (+ - * /)
                if (op != "" && !newNumber)
                {
                    Calculate();
                }
                if (!double.TryParse(displayTextBox.Text, out first))
                {
                    first = 0;
                }
                op = text;
                newNumber = true;
            }
        }

        // Выполнение операции над первым и вторым числом
        void Calculate()
        {
            if (!double.TryParse(displayTextBox.Text, out double second))
            {
                second = 0;
            }

            double result = 0;
            switch (op)
            {
                case "+":
                    result = first + second;
                    break;
                case "-":
                    result = first - second;
                    break;
                case "*":
                    result = first * second;
                    break;
                case "/":
                    if (second == 0)
                    {
                        displayTextBox.Text = "Ошибка";
                        op = "";
                        newNumber = true;
                        return;
                    }
                    result = first / second;
                    break;
            }

            displayTextBox.Text = result.ToString();
            first = result;
        }
    }
}