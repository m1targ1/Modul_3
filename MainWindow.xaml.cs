using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mod_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Figur_Click(object sender, RoutedEventArgs e)
        {
            Figure figure1 = new Figure();
            figure1.Show();
        }

        private void Notific_Click(object sender, RoutedEventArgs e)
        {
            Notification notification1 = new Notification();
            notification1.Show();
        }

        private void task1_Click(object sender, RoutedEventArgs e)
        {
            tasks1 tasks11 = new tasks1();
            tasks11.Show();
        }

        private void filt_Click(object sender, RoutedEventArgs e)
        {
            filter filter1 = new filter();
            filter1.Show();
        }

        private void sort_Click(object sender, RoutedEventArgs e)
        {
            sort sort1 = new sort();
            sort1.Show();
        }
    }
}
