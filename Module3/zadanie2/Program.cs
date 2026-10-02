using System;

// Уведомление: события для разных типов уведомлений
class Notification
{
    public event Action<string>? MessageReceived;   // Сообщение
    public event Action<string>? CallReceived;      // Звонок
    public event Action<string>? EmailReceived;     // Электронное письмо

    // Отправка уведомлений (вызов событий)
    public void SendMessage(string text) { MessageReceived?.Invoke(text); }
    public void SendCall(string text) { CallReceived?.Invoke(text); }
    public void SendEmail(string text) { EmailReceived?.Invoke(text); }
}

class Program
{
    // Обработчики событий
    static void OnMessage(string text)
    {
        Console.WriteLine("Сообщение: " + text);
    }

    static void OnCall(string text)
    {
        Console.WriteLine("Входящий звонок от: " + text);
    }

    static void OnEmail(string text)
    {
        Console.WriteLine("Новое письмо: " + text);
    }

    // Второй обработчик для сообщений (звуковой сигнал)
    static void OnMessageSound(string text)
    {
        Console.WriteLine("Звуковой сигнал: новое сообщение");
    }

    static void Main()
    {
        Notification notification = new Notification();

        // Регистрация обработчиков событий
        notification.MessageReceived += OnMessage;
        notification.MessageReceived += OnMessageSound;
        notification.CallReceived += OnCall;
        notification.EmailReceived += OnEmail;

        // Меню отправки уведомлений
        while (true)
        {
            Console.WriteLine("\n1 - сообщение");
            Console.WriteLine("2 - звонок");
            Console.WriteLine("3 - электронное письмо");
            Console.WriteLine("0 - выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine() ?? "";

            if (choice == "0")
            {
                break;
            }

            if (choice != "1" && choice != "2" && choice != "3")
            {
                Console.WriteLine("Нет такого пункта меню.");
                continue;
            }

            // Ввод текста уведомления
            Console.Write("Текст (от кого / тема): ");
            string text = Console.ReadLine() ?? "";

            // Отправка уведомления выбранного типа
            if (choice == "1")
            {
                notification.SendMessage(text);
            }
            else if (choice == "2")
            {
                notification.SendCall(text);
            }
            else
            {
                notification.SendEmail(text);
            }
        }
    }
}