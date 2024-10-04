using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Mod_3
{
    public partial class tasks1 : Window
    {        
        private List<string> tasks = new List<string>(); // Список для хранения задач
        // Делегаты для выполнения задач
        public delegate void TaskAction(string task);
        public TaskAction taskDelegate;

        public tasks1()
        {
            InitializeComponent();
        }
        // Обработчик кнопки добавления задачи
        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string newTask = taskInput.Text;

            if (!string.IsNullOrWhiteSpace(newTask))
            {
                tasks.Add(newTask); // Добавление задачи в список
                taskList.Items.Add(newTask); // Обновление ListBox
                taskInput.Clear(); // Очистка поля ввода
            }
            else
            {
                MessageBox.Show("Введите задачу"); // Предупреждение, если строка пуста
            }
        }
        // Обработчик кнопки выполнения задачи
        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            if (taskList.SelectedItem == null)
            {
                MessageBox.Show("Выберите задачу для выполнения."); // Предупреждение, если не выбрана задача
                return;
            }
            string selectedTask = taskList.SelectedItem.ToString();
            // Определение делегата на основе выбранного действия
            if (delegateSelector.SelectedItem != null)
            {
                var selectedAction = (delegateSelector.SelectedItem as ComboBoxItem).Content.ToString();

                if (selectedAction == "Отправить уведомление")
                {
                    taskDelegate = NotifyUser; // Привязка делегата для уведомления
                }
                else if (selectedAction == "Записать в журнал")
                {
                    taskDelegate = LogTask; // Привязка делегата для записи в журнал
                }
                else
                {
                    MessageBox.Show("Выберите действие для задачи.");
                    return;
                }
                // Вызов делегата для выполнения действия
                taskDelegate?.Invoke(selectedTask);
            }
            else
            {
                MessageBox.Show("Выберите действие для задачи."); // Предупреждение, если не выбрано действие
            }
        }
        // Делегат для отправки уведомления
        public void NotifyUser(string task)
        {
            MessageBox.Show($"Уведомление: Задача '{task}' выполнена.");
        }
        // Делегат для записи в журнал
        public void LogTask(string task)
        {
            // Здесь можно добавить запись в файл или лог
            MessageBox.Show($"Задача '{task}' записана в журнал.");
        }
    }
}
