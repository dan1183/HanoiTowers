using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для top_time.xaml
    /// </summary>
    public partial class Top_time : Window
    {
        public Top_time()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var dictionary = new Dictionary<string, string>();

            string[] lines = File.ReadAllLines("TOP_TIME.txt");
            foreach (string str in lines)
            {
                string[] split = str.Split(' ');
                dictionary.Add(split[0], split[2]);
            }

            var items = from pair in dictionary
                        orderby pair.Value ascending
                        select pair;

            int counter = 1;
            foreach (KeyValuePair<string, string> pair in items)
            {
                if (counter == 11) { break; }
                name_player.Text += counter + ") " + pair.Key + Environment.NewLine;
                time_player.Text += counter + ") " + pair.Value + Environment.NewLine;
                counter++;
            }
        }
    }
}