using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Mod_3
{
    public partial class filter : Window
    {
        public class DataItem // Класс для хранения данных, которые будут фильтроваться
        {
            public DateTime Date { get; set; } // Свойство для хранения даты элемента
            public string Keyword { get; set; } // Свойство для хранения ключевого слова элемента
            public DataItem(DateTime date, string keyword) // Конструктор класса DataItem для инициализации свойств
            {
                Date = date;
                Keyword = keyword;
            }
            // Переопределение метода ToString() для отображения данных в ListBox
            public override string ToString()
            {
                return $"{Date.ToShortDateString()} - {Keyword}";
            }
        }
        delegate bool FilterDelegate(DataItem item); // Делегат, который представляет метод фильтрации
        private List<DataItem> dataList; // Список данных, который будет отображаться и фильтроваться

        public filter()
        {
            InitializeComponent();

            // Инициализация списка данных с некоторыми элементами
            dataList = new List<DataItem>
            {
                new DataItem(new DateTime(2024, 10, 1), "project"), new DataItem(new DateTime(2024, 10, 2), "meeting"),
                new DataItem(new DateTime(2024, 10, 3), "deadline"), new DataItem(new DateTime(2024, 10, 4), "report")
            };

            UpdateDataList(dataList); // Отображение исходных данных в ListBox при запуске приложения
        }
        // Метод для обновления данных в ListBox.
        private void UpdateDataList(List<DataItem> filteredData)
        {
            DataListBox.ItemsSource = null; // Очистка источника данных, чтобы обновление отобразилось корректно
            DataListBox.ItemsSource = filteredData; // Устанавливаем новый источник данных для ListBox
        }
        // Обработчик события при изменении выбранного фильтра в ComboBox.
        private void FilterTypeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (FilterTypeComboBox.SelectedIndex == 0) // Если выбран фильтр по дате
            {
                DateFilterPanel.Visibility = Visibility.Visible; // Показ панели для фильтрации по дате
                KeywordFilterPanel.Visibility = Visibility.Collapsed; // Скрытие панели для фильтрации по ключевому слову
            }
            else if (FilterTypeComboBox.SelectedIndex == 1) // Если выбран фильтр по ключевому слову
            {
                DateFilterPanel.Visibility = Visibility.Collapsed; // Скрытие панели для фильтрации по дате
                KeywordFilterPanel.Visibility = Visibility.Visible; // Показ панели для фильтрации по ключевому слову
            }
        }
        // Обработчик события нажатия кнопки "Применить фильтр".
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            FilterDelegate filters = null; // Объявление переменной делегата фильтрации
            if (FilterTypeComboBox.SelectedIndex == 0) // Если выбран фильтр по дате
            {
                if (DatePicker.SelectedDate.HasValue) // Проверка, выбрана ли дата
                {
                    DateTime selectedDate = DatePicker.SelectedDate.Value; // Получение выбранной даты
                    filters = item => item.Date == selectedDate; // Фильтрация по дате
                }
            }
            else if (FilterTypeComboBox.SelectedIndex == 1) // Если выбран фильтр по ключевому слову.
            {
                string keyword = KeywordTextBox.Text; // Получение введенного ключевого слова
                filters = item => item.Keyword.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0; // Фильтрации по ключевому слову (без учета регистра).
            }
            // Применение фильтра и обновление отображаемых данных
            if (filters != null)
            {
                List<DataItem> filteredData = FilterData(dataList, filters); // Получение отфильтрованного списка данных
                UpdateDataList(filteredData); // Обновление ListBox с новыми данными
            }
        }
        // Метод для фильтрации данных с использованием делегата
        private List<DataItem> FilterData(List<DataItem> data, FilterDelegate filter)
        {
            // Используем LINQ для фильтрации списка на основе переданного делегата
            return data.Where(item => filter(item)).ToList();
        }
        // Обработчик события нажатия кнопки сброса фильтра.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Очистка полей поиска
            DatePicker.SelectedDate = null;
            KeywordTextBox.Text = string.Empty;

            // Скрытие панелей с фильтрами
            DateFilterPanel.Visibility = Visibility.Collapsed;
            KeywordFilterPanel.Visibility = Visibility.Collapsed;  
            
            FilterTypeComboBox.SelectedIndex = -1; // Сброс выбранного элемента в ComboBox
            UpdateDataList(dataList); // Отображение всех элементов списка без фильтрации
        }
    }
}
