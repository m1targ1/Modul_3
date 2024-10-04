using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Mod_3
{
    public partial class sort : Window
    {
        // Делегат для сортировки
        delegate List<int> SortDelegate(List<int> data);
        public sort()
        {
            InitializeComponent();
        }
        // Метод сортировки пузырьком
        private List<int> BubbleSort(List<int> data)
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                for (int j = 0; j < data.Count - i - 1; j++)
                {
                    if (data[j] > data[j + 1])
                    {
                        int temp = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = temp;
                    }
                }
            }
            return data;
        }
        // Метод сортировки расчёской
        private List<int> CombSort(List<int> data)
        {
            int gap = data.Count;
            bool swapped = true;

            while (gap != 1 || swapped)
            {
                gap = (gap * 10) / 13;  // сокращаем шаг
                if (gap < 1) gap = 1;

                swapped = false;
                for (int i = 0; i < data.Count - gap; i++)
                {
                    if (data[i] > data[i + gap])
                    {
                        int temp = data[i];
                        data[i] = data[i + gap];
                        data[i + gap] = temp;
                        swapped = true;
                    }
                }
            }
            return data;
        }
        // Шейкерная сортировка
        private List<int> ShakerSort(List<int> data)
        {
            bool swapped = true;
            int start = 0;
            int end = data.Count;

            while (swapped)
            {
                swapped = false;
                // Проход справа налево
                for (int i = start; i < end - 1; ++i)
                {
                    if (data[i] > data[i + 1])
                    {
                        int temp = data[i];
                        data[i] = data[i + 1];
                        data[i + 1] = temp;
                        swapped = true;
                    }
                }
                // Если не было обменов, выходим
                if (!swapped) break;

                swapped = false;
                end--;
                // Проход слева направо
                for (int i = end - 1; i > start; --i)
                {
                    if (data[i] < data[i - 1])
                    {
                        int temp = data[i];
                        data[i] = data[i - 1];
                        data[i - 1] = temp;
                        swapped = true;
                    }
                }
                start++;
            }
            return data;
        }
        // Сортировка подсчётом
        private List<int> CountingSort(List<int> data)
        {
            if (data.Count == 0) return data;
            int max = data.Max();
            int min = data.Min();
            int range = max - min + 1;
            int[] count = new int[range];
            List<int> output = new List<int>(new int[data.Count]);
            // Подсчитываем количество элементов
            for (int i = 0; i < data.Count; i++)
                count[data[i] - min]++;
            // Модифицируем массив count
            for (int i = 1; i < count.Length; i++)
                count[i] += count[i - 1];
            // Строим отсортированный массив
            for (int i = data.Count - 1; i >= 0; i--)
            {
                output[count[data[i] - min] - 1] = data[i];
                count[data[i] - min]--;
            }
            return output;
        }
        // Глупая сортировка (BogoSort)
        private List<int> BogoSort(List<int> data)
        {
            Random rand = new Random();
            while (!IsSorted(data))
            {
                // Перемешиваем массив
                for (int i = 0; i < data.Count; i++)
                {
                    int randIndex = rand.Next(data.Count);
                    int temp = data[i];
                    data[i] = data[randIndex];
                    data[randIndex] = temp;
                }
            }
            return data;
        }
        // Проверка отсортирован ли массив
        private bool IsSorted(List<int> data)
        {
            for (int i = 0; i < data.Count - 1; i++)
            {
                if (data[i] > data[i + 1])
                    return false;
            }
            return true;
        }
        // Обработка нажатия кнопки "Заполнить случайными числами"
        private void RandomButton_Click(object sender, RoutedEventArgs e)
        {
            int count;
            if (int.TryParse(RandomCountTextBox.Text, out count) && count > 0)
            {
                Random rand = new Random();
                List<int> randomData = new List<int>();
                for (int i = 0; i < count; i++)
                {
                    randomData.Add(rand.Next(0, 100));  // Генерируем числа от 0 до 100
                }
                InputTextBox.Text = string.Join(",", randomData);  // Заполняем TextBox сгенерированными числами
            }
            else
            {
                MessageBox.Show("Введите корректное количество случайных чисел.");
            }
        }
        // Обработка нажатия кнопки "Сортировать"
        private void SortButton_Click(object sender, RoutedEventArgs e)
        {
            // Чтение введенных чисел
            List<int> numbers = InputTextBox.Text.Split(',').Select(int.Parse).ToList();

            // Определение выбранного метода сортировки
            SortDelegate sortMethod = null;
            switch (SortMethodComboBox.SelectedIndex)
            {
                case 0: // Сортировка пузырьком
                    sortMethod = BubbleSort;
                    break;
                case 1: // Сортировка расчёской
                    sortMethod = CombSort;
                    break;
                case 2: // Шейкерная сортировка
                    sortMethod = ShakerSort;
                    break;
                case 3: // Сортировка подсчётом
                    sortMethod = CountingSort;
                    break;
                case 4: // Глупая сортировка
                    sortMethod = BogoSort;
                    break;
                default:
                    MessageBox.Show("Выберите метод сортировки.");
                    return;
            }
            List<int> sortedNumbers = sortMethod(numbers); // Выполнение сортировки
            SortedTextBox.Text = string.Join(",", sortedNumbers); // Вывод отсортированных данных
        }
    }
}
