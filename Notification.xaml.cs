using System;
using System.Windows;

namespace Mod_3
{
    public partial class Notification : Window
    {
        public Notification()
        {
            InitializeComponent();
        }

        // Обработчик кнопки для ввода
        private void input_Click(object sender, RoutedEventArgs e)
        {
            Notifications notifications = new Notifications(); // Создание объекта класса Notifications

            // Подписка на события
            notifications.OnMessageSent += Notifications_OnMessageSent;
            notifications.OnCallMade += Notifications_OnCallMade;
            notifications.OnEmailSent += Notifications_OnEmailSent;

            if (string.IsNullOrEmpty(text_input.Text)) // Проверка на пустоту строки
            {
                MessageBox.Show("Строка не должна быть пустой");
                return;
            }

            if (int.TryParse(text_input.Text, out int inputInt)) // Проверка на целое число
            {
                notifications.Calling(inputInt); // Вызов метода звонка
            }
            else if (double.TryParse(text_input.Text, out double inputDouble)) // Проверка на дробное число
            {
                notifications.Email(inputDouble); // Вызов метода отправки email
            }
            else // Обработка текста
            {
                string message = text_input.Text;
                notifications.Message(message); // Вызов метода отправки сообщения
            }
        }

        // Обработчики событий
        private void Notifications_OnMessageSent(string message)
        {
            MessageBox.Show($"Обработчик события: Сообщение отправлено - {message}");
        }

        private void Notifications_OnCallMade(int callNumber)
        {
            MessageBox.Show($"Обработчик события: Звонок совершен на номер - {callNumber}");
        }

        private void Notifications_OnEmailSent(double emailAddress)
        {
            MessageBox.Show($"Обработчик события: Письмо отправлено на адрес - {emailAddress}");
        }
    }

    // Класс для уведомлений с событиями
    class Notifications
    {
        // Объявление событий
        public event Action<string> OnMessageSent;
        public event Action<int> OnCallMade;
        public event Action<double> OnEmailSent;

        // Свойства
        public string Messagee { get; set; }
        public int Call { get; set; }
        public double Emaill { get; set; }

        // Метод для отправки сообщения
        public void Message(string message)
        {
            Messagee = message;
            MessageBox.Show($"Сообщение: {Messagee}");
            OnMessageSent?.Invoke(Messagee); // Вызов события, если есть подписчики
        }

        // Метод для звонка
        public void Calling(int callNumber)
        {
            Call = callNumber;
            MessageBox.Show($"Звонок на номер: {Call}");
            OnCallMade?.Invoke(Call); // Вызов события, если есть подписчики
        }

        // Метод для отправки электронного письма
        public void Email(double emailAddress)
        {
            Emaill = emailAddress;
            MessageBox.Show($"Отправка письма на адрес: {Emaill}");
            OnEmailSent?.Invoke(Emaill); // Вызов события, если есть подписчики
        }
    }
}
