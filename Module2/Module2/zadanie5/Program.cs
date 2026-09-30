using System;

// Датчик температуры
class TemperatureSensor
{
    // Событие: температура изменилась (передается новое значение)
    public event Action<double>? TemperatureChanged;

    private double temperature = 20;

    // Установка температуры: событие вызывается, только если значение изменилось
    public void SetTemperature(double value)
    {
        if (value != temperature)
        {
            temperature = value;
            TemperatureChanged?.Invoke(value);
        }
        else
        {
            Console.WriteLine("Температура не изменилась.");
        }
    }
}

// Термостат
class Thermostat
{
    // Подписка на событие датчика
    public Thermostat(TemperatureSensor sensor)
    {
        sensor.TemperatureChanged += OnTemperatureChanged;
    }

    // Обработчик события: включение или выключение отопления
    private void OnTemperatureChanged(double temperature)
    {
        if (temperature < 20)
        {
            Console.WriteLine(temperature + " °C: холодно, отопление включено");
        }
        else
        {
            Console.WriteLine(temperature + " °C: тепло, отопление выключено");
        }
    }
}

class Program
{
    static void Main()
    {
        // Создание датчика и термостата
        TemperatureSensor sensor = new TemperatureSensor();
        Thermostat thermostat = new Thermostat(sensor);

        Console.WriteLine("Начальная температура датчика: 20 °C");

        // Ввод температуры, пока не введено "q"
        while (true)
        {
            Console.Write("Введите температуру (q для выхода): ");
            string? input = Console.ReadLine();

            // Проверка на выход
            if (input == null || input == "q")
            {
                break;
            }

            // Проверка ввода
            if (!double.TryParse(input, out double t))
            {
                Console.WriteLine("Нужно ввести число.");
                continue;
            }

            // Передача температуры датчику
            sensor.SetTemperature(t);
        }
    }
}