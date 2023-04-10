using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace WpfApp5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Close_window_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Start_game_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 settings_game = new Window1();
            settings_game.Show();
        }

        private void Rules_button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Есть три стержня A, B, и C. На стержень A надето N дисков, наверху самый маленький, каждый следующий диск больше предыдущего, а внизу самый большой. На другие стержни дисков не надето. Hеобходимо перенести диски со стержня A на стержень C, пользуясь стержнем B, как вспомогательным, так, чтобы диски на стержне C располагались в том же порядке, в каком они располагаются на диске A перед перемещением. При перемещении никогда нельзя класть больший диск на меньший.", "Правила");
        }

        private void Mediaelement_fon_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaelement_fon.Position = new TimeSpan(0, 0, 1);
            mediaelement_fon.Play();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://github.com/dan1183/HanoiTowers/blob/master/README.md");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
