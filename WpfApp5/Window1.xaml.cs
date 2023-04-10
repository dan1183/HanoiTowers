using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Linq;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        void All_lostFocus(int variable)
        {
            switch (variable)
            {
                case 0:
                    {
                        player_PinCode.Text = "ПИН-код...";
                        player_PinCode.Foreground = Brushes.Gray;
                        break;
                    }

                case 1:
                    {
                        count_disks_textbox.Text = "Количество дисков...";
                        count_disks_textbox.Foreground = Brushes.Gray;
                        break;
                    }

                case 2:
                    {
                        name_player.Text = "Имя игрока...";
                        name_player.Foreground = Brushes.Gray;
                        break;
                    }
            }
        }

        void All_gotFocus(int variable)
        {
            switch (variable)
            {
                case 0:
                    {
                        player_PinCode.Text = "";
                        player_PinCode.Foreground = Brushes.White;
                        break;
                    }

                case 1:
                    {
                        count_disks_textbox.Text = "";
                        count_disks_textbox.Foreground = Brushes.White;
                        break;
                    }

                case 2:
                    {
                        name_player.Text = "";
                        name_player.Foreground = Brushes.White;
                        break;
                    }
            }
        }

        private void Player_PinCode_GotFocus(object sender, RoutedEventArgs e)
        {
            if (player_PinCode.Text == "ПИН-код...") { All_gotFocus(0); }
        }

        private void Count_disks_textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (count_disks_textbox.Text == "Количество дисков...") { All_gotFocus(1); }
        }

        private void Name_player_GotFocus(object sender, RoutedEventArgs e)
        {
            if (name_player.Text == "Имя игрока...") { All_gotFocus(2); }
        }

        private void Player_PinCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (player_PinCode.Text == "") { All_lostFocus(0); }
        }

        private void Count_disks_textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (count_disks_textbox.Text == "") { All_lostFocus(1); }
        }

        private void Name_player_LostFocus(object sender, RoutedEventArgs e)
        {
            if (name_player.Text == "") { All_lostFocus(2); }
        }

        private void Start_button_game_Click(object sender, RoutedEventArgs e)
        {
            string player_name;
            bool name;
            try
            {
                player_name = Convert.ToString(name_player.Text);
                if ((player_name.Length < 1) || (player_name.Length > 15) || (player_name == "Имя игрока..."))
                {
                    MessageBox.Show("Введено неверное имя!", "Ошибка");
                    return;
                }

                else { name = true; }
            }
            finally { }

            int pin_code;
            bool pin;
            try
            {
                pin_code = Convert.ToInt32(player_PinCode.Text);
                if ((pin_code > 9999) || (pin_code < 1000))
                {
                    MessageBox.Show("Введен неверный ПИН-код!", "Ошибка");
                    return;
                }

                else { pin = true; }
            }

            catch (FormatException)
            {
                MessageBox.Show("Введено не число [ПИН-код]!", "Ошибка");
                return;
            }

            int count_disks;
            bool disks;
            try
            {
                count_disks = Convert.ToInt32(count_disks_textbox.Text);
                if ((count_disks > 8) || (count_disks < 3))
                {
                    MessageBox.Show("Введено неверное число [Количество дисков]!", "Ошибка");
                    return;
                }

                else { disks = true; }
            }

            catch (FormatException)
            {
                MessageBox.Show("Введено не число [Количество дисков]!", "Ошибка");
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines("TOP_TIME.txt");
                foreach (string str in lines)
                {
                    string[] split = str.Split(' ');
                    if (split[0] == player_name)
                    {
                        if (Convert.ToInt32(split[1]) == pin_code)
                        {
                            name = true;
                        }

                        else
                        {
                            MessageBox.Show("Имя уже существует!\nВведен неверный ПИН-код", "Ошибка");
                            name = false;
                            return;
                        }
                    }
                }
            }
            finally { }

            if (disks && name && pin)
            {
                if ((buttons_variable.IsChecked == false) && (keyboard_variable.IsChecked == false))
                {
                    MessageBox.Show("Не выбран тип управления!", "Ошибка");
                    return;
                }

                else if (buttons_variable.IsChecked == true)
                {
                    this.Hide();
                    Window2 settings_game = new Window2(count_disks, pin_code, player_name);
                    settings_game.Show();
                }
                
                else if (keyboard_variable.IsChecked == true)
                {
                    this.Hide();
                    Keyboard settings_game = new Keyboard(count_disks, pin_code, player_name);
                    settings_game.Show();
                }
            }
        }

        private void Mediaelement_fon_MediaEnded(object sender, RoutedEventArgs e)
        {
            mediaelement_fon.Position = new TimeSpan(0, 0, 1);
            mediaelement_fon.Play();
        }
    }
}
