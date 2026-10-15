using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie5
{
    internal class Calculator
    {
        // Разделитель дробной части по настройкам Windows (запятая или точка)
        public string Separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        string display = "0";      // Текст на экране
        double first = 0;          // Первое число
        string op = "";            // Выбранная операция
        bool newNumber = true;     // Следующая цифра начинает новое число

        // Нажатие кнопки: возвращает новый текст для экрана
        public string Press(string key)
        {
            if (key == "C")
            {
                // Сброс
                display = "0";
                first = 0;
                op = "";
                newNumber = true;
            }
            else if (char.IsDigit(key[0]) || key == Separator)
            {
                // Ввод цифры или десятичного разделителя
                if (newNumber)
                {
                    display = "0";
                    newNumber = false;
                }

                if (key == Separator)
                {
                    if (!display.Contains(Separator))
                    {
                        display += Separator;
                    }
                }
                else if (display == "0")
                {
                    display = key;
                }
                else
                {
                    display += key;
                }
            }
            else if (key == "=")
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
                if (!double.TryParse(display, out first))
                {
                    first = 0;
                }
                op = key;
                newNumber = true;
            }

            return display;
        }

        // Выполнение операции над первым и вторым числом
        void Calculate()
        {
            if (!double.TryParse(display, out double second))
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
                        display = "Ошибка";
                        op = "";
                        newNumber = true;
                        return;
                    }
                    result = first / second;
                    break;
            }

            display = result.ToString();
            first = result;
        }
    }
}