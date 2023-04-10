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
using System.Windows.Shapes;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    /// 

    public partial class Window4 : Window
    {
        private readonly List<Tuple<int, int>> correct_steps = new List<Tuple<int, int>>();

        public void Movetower(int n, int from, int to, int other)
        {
            if (n > 0)
            {
                Movetower(n - 1, from, other, to);
                correct_steps.Add(Tuple.Create(from, to));
                Movetower(n - 1, other, to, from);
            }
        }

        public Window4(List<Tuple<int, int>> all_steps, int count_disks)
        {
            InitializeComponent();

            Movetower(count_disks, 1, 3, 2);

            int counter = 1;
            foreach (Tuple<int, int> step in all_steps)
            {
                count_disks_textbox.Text += Convert.ToString(counter) + ") " + Convert.ToString(step.Item2) + " -> " + Convert.ToString(step.Item1) + "\n";
                counter++;
            }

            counter = 1;
            foreach (Tuple<int, int> step in correct_steps)
            {
                correct_steps_box.Text += Convert.ToString(counter) + ") " + Convert.ToString(step.Item2) + " -> " + Convert.ToString(step.Item1) + "\n";
                counter++;
            }
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
    }
}
