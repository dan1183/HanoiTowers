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
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        readonly Window4 all_steps_window;

        public Window3(int seconds, List<Tuple<int, int>> all_steps, int count_disks, int pin_code, string player_name)
        {
            InitializeComponent();

            time.Content += " " + seconds % 3600 / 60 + " минут " + seconds % 3600 % 60 + " секунд";

            string seconds_strings = "0";
            if ((seconds % 3600 / 60 < 10) && (seconds % 3600 % 60 < 10))
            {
                seconds_strings = String.Format("0{0}:0{1}", seconds % 3600 / 60, seconds % 3600 % 60);
            }

            else if ((seconds % 3600 / 60 > 10) && (seconds % 3600 % 60 > 10))
            {
                seconds_strings = String.Format("{0}:{1}", seconds % 3600 / 60, seconds % 3600 % 60);
            }

            else if ((seconds % 3600 / 60 < 10) && (seconds % 3600 % 60 > 10))
            {
                seconds_strings = String.Format("0{0}:{1}", seconds % 3600 / 60, seconds % 3600 % 60);
            }

            else if ((seconds % 3600 / 60 > 10) && (seconds % 3600 % 60 < 10))
            {
                seconds_strings = String.Format("{0}:0{1}", seconds % 3600 / 60, seconds % 3600 % 60);
            }

            bool overwrite = false;
            bool nowrite = false;
            string[] lines = File.ReadAllLines("TOP_TIME.txt");
            for (int i = 0; i < lines.Count(); ++i)
            {
                string[] split = lines[i].Split(' ');
                if (split[0] == player_name)
                {
                    if (string.Compare(split[2], seconds_strings) > 0)
                    {
                        split[2] = seconds_strings;
                        lines[i] = split[0] + " " + split[1] + " " + split[2];
                        File.WriteAllText("TOP_TIME.txt", string.Empty);
                        overwrite = true;
                    }
                    else { nowrite = true;  }
                    break;
                }
            }

            if (overwrite)
            {
                foreach (string str in lines)
                {
                    File.AppendAllText("TOP_TIME.txt", str + Environment.NewLine);
                }
            }
            
            else if (!overwrite && !nowrite)
            {
                File.AppendAllText("TOP_TIME.txt", Environment.NewLine + player_name + " " + pin_code + " " + seconds_strings);
            }

            count_steps.Content += " " + all_steps.Count;
            all_steps_window = new Window4(all_steps, count_disks);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        private void Close_window_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Full_steps_Unchecked(object sender, RoutedEventArgs e)
        {
            all_steps_window.Hide();
        }

        private void Full_steps_Checked(object sender, RoutedEventArgs e)
        {
            all_steps_window.Show();
        }

        readonly Top_time top_time_window = new Top_time();

        private void Check_top_time_Checked(object sender, RoutedEventArgs e)
        {
            top_time_window.Show();
        }

        private void Check_top_time_Unchecked(object sender, RoutedEventArgs e)
        {
            top_time_window.Hide();
        }

        private void Mediaelement_fon_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaelement_fon.Position = new TimeSpan(0, 0, 1);
            mediaelement_fon.Play();
        }
    }
}
