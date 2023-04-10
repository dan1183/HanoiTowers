using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Threading;

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для keyboard.xaml
    /// </summary>
    public partial class Keyboard : Window
    {
        private static readonly Dictionary<int, int> get_left = new Dictionary<int, int>
        {
            [210] = 0,
            [190] = 10,
            [170] = 20,
            [150] = 30,
            [130] = 40,
            [110] = 50,
            [90] = 60,
            [70] = 70
        };

        readonly List<System.Windows.Shapes.Rectangle> rectangles_1 = new List<System.Windows.Shapes.Rectangle>();
        private int timer_sec = 0;
        private void TimerTick(object sender, EventArgs e)
        {
            timer_sec += 1;
        }

        readonly int count_disks, pin_code;
        readonly string player_name;
        readonly DispatcherTimer timer = null;
        public Keyboard(int value, int pin, string name)
        {
            InitializeComponent();

            List_rectangle lr = new List_rectangle();
            lr.Fill_list(value);

            rectangles_1 = lr.Get_list();

            foreach (System.Windows.Shapes.Rectangle rect in rectangles_1)
            {
                canvas_main.Children.Add(rect);
            }

            timer = new DispatcherTimer();  // если надо, то в скобках указываем приоритет, например DispatcherPriority.Render
            timer.Tick += new EventHandler(TimerTick);
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Start();

            count_disks = value;
            pin_code = pin;
            player_name = name;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            App.Current.Shutdown();
        }

        void Check_EndGame()
        {
            if ((rectangles_1.Count == 0) && (rectangles_2.Count == 0))
            {
                timer.Stop();
                if (MessageBox.Show("Поздравляем с победой!", "Уведомление") == System.Windows.Forms.DialogResult.OK)
                {
                    this.Hide();
                    Window3 end_game = new Window3(timer_sec, full_steps, count_disks, pin_code, player_name);
                    end_game.Show();
                }
            }
        }

        private readonly List<Tuple<int, int>> full_steps = new List<Tuple<int, int>>();
        readonly List<System.Windows.Shapes.Rectangle> rectangles_2 = new List<System.Windows.Shapes.Rectangle>();
        readonly List<System.Windows.Shapes.Rectangle> rectangles_3 = new List<System.Windows.Shapes.Rectangle>();
        private Key lastKey;
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if ((lastKey == Key.F1) && (e.Key == Key.D2))
            {
                if (rectangles_1.Count == 0) { System.Windows.Forms.MessageBox.Show("Стержень пуст!", "Ошибка"); }

                else if (rectangles_2.Count == 0)
                {
                    rectangles_2.Add(rectangles_1[rectangles_1.Count - 1]);
                    rectangles_1.RemoveAt(rectangles_1.Count - 1);
                    int left = get_left[Convert.ToInt32(rectangles_2[rectangles_2.Count - 1].Width)];
                    Canvas.SetLeft(rectangles_2[rectangles_2.Count - 1], 277 + left);
                    Canvas.SetTop(rectangles_2[rectangles_2.Count - 1], 330);
                    full_steps.Add(Tuple.Create(1, 2));
                }

                else if (rectangles_1[rectangles_1.Count - 1].Width > rectangles_2[rectangles_2.Count - 1].Width) { System.Windows.Forms.MessageBox.Show("Нельзя ставить больший элемент на меньший!", "Ошибка"); }

                else
                {
                    rectangles_2.Add(rectangles_1[rectangles_1.Count - 1]);
                    int left = get_left[Convert.ToInt32(rectangles_2[rectangles_2.Count - 1].Width)];
                    rectangles_1.RemoveAt(rectangles_1.Count - 1);
                    Canvas.SetLeft(rectangles_2[rectangles_2.Count - 1], 277 + left);
                    Canvas.SetTop(rectangles_2[rectangles_2.Count - 1], 330 - 30 * (rectangles_2.Count - 1));
                    full_steps.Add(Tuple.Create(1, 2));
                }
            }

            if ((lastKey == Key.F1) && (e.Key == Key.D3))
            {
                if (rectangles_1.Count == 0) { System.Windows.Forms.MessageBox.Show("Стержень пуст!", "Ошибка"); }

                else if (rectangles_3.Count == 0)
                {
                    rectangles_3.Add(rectangles_1[rectangles_1.Count - 1]);
                    rectangles_1.RemoveAt(rectangles_1.Count - 1);
                    int left = get_left[Convert.ToInt32(rectangles_3[rectangles_3.Count - 1].Width)];
                    Canvas.SetLeft(rectangles_3[rectangles_3.Count - 1], 521 + left);
                    Canvas.SetTop(rectangles_3[rectangles_3.Count - 1], 330);
                    full_steps.Add(Tuple.Create(1, 3));
                }

                else if (rectangles_1[rectangles_1.Count - 1].Width > rectangles_3[rectangles_3.Count - 1].Width) { System.Windows.Forms.MessageBox.Show("Нельзя ставить больший элемент на меньший!", "Ошибка"); }

                else
                {
                    rectangles_3.Add(rectangles_1[rectangles_1.Count - 1]);
                    int left = get_left[Convert.ToInt32(rectangles_3[rectangles_3.Count - 1].Width)];
                    rectangles_1.RemoveAt(rectangles_1.Count - 1);
                    Canvas.SetLeft(rectangles_3[rectangles_3.Count - 1], 521 + left);
                    Canvas.SetTop(rectangles_3[rectangles_3.Count - 1], 330 - 30 * (rectangles_3.Count - 1));
                    full_steps.Add(Tuple.Create(1, 3));
                }
                Check_EndGame();
            }

            if ((lastKey == Key.F2) && (e.Key == Key.D1))
            {
                if (rectangles_2.Count == 0) { System.Windows.Forms.MessageBox.Show("Стержень пуст!", "Ошибка"); }

                else if (rectangles_1.Count == 0)
                {
                    rectangles_1.Add(rectangles_2[rectangles_2.Count - 1]);
                    rectangles_2.RemoveAt(rectangles_2.Count - 1);
                    int left = get_left[Convert.ToInt32(rectangles_1[rectangles_1.Count - 1].Width)];
                    Canvas.SetLeft(rectangles_1[rectangles_1.Count - 1], 35 + left);
                    Canvas.SetTop(rectangles_1[rectangles_1.Count - 1], 330);
                    full_steps.Add(Tuple.Create(2, 1));
                }

                else if (rectangles_2[rectangles_2.Count - 1].Width > rectangles_1[rectangles_1.Count - 1].Width) { System.Windows.Forms.MessageBox.Show("Нельзя ставить больший элемент на меньший!", "Ошибка"); }

                else
                {
                    rectangles_1.Add(rectangles_2[rectangles_2.Count - 1]);
                    int left = get_left[Convert.ToInt32(rectangles_1[rectangles_1.Count - 1].Width)];
                    rectangles_2.RemoveAt(rectangles_2.Count - 1);
                    Canvas.SetLeft(rectangles_1[rectangles_1.Count - 1], 35 + left);
                    Canvas.SetTop(rectangles_1[rectangles_1.Count - 1], 330 - 30 * (rectangles_1.Count - 1));
                    full_steps.Add(Tuple.Create(2, 1));
                }
            }

            if ((lastKey == Key.F2) && (e.Key == Key.D3))
            {
                if (rectangles_2.Count == 0) { System.Windows.Forms.MessageBox.Show("Стержень пуст!", "Ошибка"); }

                else if (rectangles_3.Count == 0)
                {
                    rectangles_3.Add(rectangles_2[rectangles_2.Count - 1]);
                    rectangles_2.RemoveAt(rectangles_2.Count - 1);
                    int left = get_left[Convert.ToInt32(rectangles_3[rectangles_3.Count - 1].Width)];
                    Canvas.SetLeft(rectangles_3[rectangles_3.Count - 1], 521 + left);
                    Canvas.SetTop(rectangles_3[rectangles_3.Count - 1], 330);
                    full_steps.Add(Tuple.Create(2, 3));
                }

                else if (rectangles_2[rectangles_2.Count - 1].Width > rectangles_3[rectangles_3.Count - 1].Width) { System.Windows.Forms.MessageBox.Show("Нельзя ставить больший элемент на меньший!", "Ошибка"); }

                else
                {
                    rectangles_3.Add(rectangles_2[rectangles_2.Count - 1]);
                    int left = get_left[Convert.ToInt32(rectangles_3[rectangles_3.Count - 1].Width)];
                    rectangles_2.RemoveAt(rectangles_2.Count - 1);
                    Canvas.SetLeft(rectangles_3[rectangles_3.Count - 1], 521 + left);
                    Canvas.SetTop(rectangles_3[rectangles_3.Count - 1], 330 - 30 * (rectangles_3.Count - 1));
                    full_steps.Add(Tuple.Create(2, 3));
                }
                Check_EndGame();
            }

            if ((lastKey == Key.F3) && (e.Key == Key.D1))
            {
                if (rectangles_3.Count == 0) { System.Windows.Forms.MessageBox.Show("Стержень пуст!", "Ошибка"); }

                else if (rectangles_1.Count == 0)
                {
                    rectangles_1.Add(rectangles_3[rectangles_3.Count - 1]);
                    rectangles_3.RemoveAt(rectangles_3.Count - 1);
                    int left = get_left[Convert.ToInt32(rectangles_1[rectangles_1.Count - 1].Width)];
                    Canvas.SetLeft(rectangles_1[rectangles_1.Count - 1], 35 + left);
                    Canvas.SetTop(rectangles_1[rectangles_1.Count - 1], 330);
                    full_steps.Add(Tuple.Create(3, 1));
                }

                else if (rectangles_3[rectangles_3.Count - 1].Width > rectangles_1[rectangles_1.Count - 1].Width) { System.Windows.Forms.MessageBox.Show("Нельзя ставить больший элемент на меньший!", "Ошибка"); }

                else
                {
                    rectangles_1.Add(rectangles_3[rectangles_3.Count - 1]);
                    int left = get_left[Convert.ToInt32(rectangles_1[rectangles_1.Count - 1].Width)];
                    rectangles_3.RemoveAt(rectangles_3.Count - 1);
                    Canvas.SetLeft(rectangles_1[rectangles_1.Count - 1], 35 + left);
                    Canvas.SetTop(rectangles_1[rectangles_1.Count - 1], 330 - 30 * (rectangles_1.Count - 1));
                    full_steps.Add(Tuple.Create(3, 1));
                }
            }

            if ((lastKey == Key.F3) && (e.Key == Key.D2))
            {
                if (rectangles_3.Count == 0) { System.Windows.Forms.MessageBox.Show("Стержень пуст!", "Ошибка"); }

                else if (rectangles_2.Count == 0)
                {
                    rectangles_2.Add(rectangles_3[rectangles_3.Count - 1]);
                    rectangles_3.RemoveAt(rectangles_3.Count - 1);
                    int left = get_left[Convert.ToInt32(rectangles_2[rectangles_2.Count - 1].Width)];
                    Canvas.SetLeft(rectangles_2[rectangles_2.Count - 1], 277 + left);
                    Canvas.SetTop(rectangles_2[rectangles_2.Count - 1], 330);
                    full_steps.Add(Tuple.Create(3, 2));
                }

                else if (rectangles_3[rectangles_3.Count - 1].Width > rectangles_2[rectangles_2.Count - 1].Width) { System.Windows.Forms.MessageBox.Show("Нельзя ставить больший элемент на меньший!", "Ошибка"); }

                else
                {
                    rectangles_2.Add(rectangles_3[rectangles_3.Count - 1]);
                    int left = get_left[Convert.ToInt32(rectangles_2[rectangles_2.Count - 1].Width)];
                    rectangles_3.RemoveAt(rectangles_3.Count - 1);
                    Canvas.SetLeft(rectangles_2[rectangles_2.Count - 1], 277 + left);
                    Canvas.SetTop(rectangles_2[rectangles_2.Count - 1], 330 - 30 * (rectangles_2.Count - 1));
                    full_steps.Add(Tuple.Create(3, 2));
                }
            }
            lastKey = e.Key;
        }

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("F1 + 2 - сдвиг с 1 башни на 2\n"
                            + "F1 + 3 - сдвиг с 1 башни на 3\n"
                            + "F2 + 1 - сдвиг с 2 башни на 1\n"
                            + "F2 + 3 - сдвиг с 2 башни на 3\n"
                            + "F3 + 1 - сдвиг с 3 башни на 1\n"
                            + "F3 + 2 - сдвиг с 3 башни на 2\n",
                            "Управление", (MessageBoxButtons)MessageBoxButton.OK, (MessageBoxIcon)MessageBoxImage.Information);
        }

        /*
        private void frmParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            //при попытке закрытия программы пользователем
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //устанавливает флаг отмены события в истину
                e.Cancel = true;
                //спрашивает стоит ли завершится
                if (System.Windows.Forms.MessageBox.Show("Вы уверены что хотите закрыть программу?", "Выйти?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    //и после этого только завершается работа приложения
                    System.Windows.Forms.Application.Exit();
                }
            }
        }
        */

        /*
        Point p;
        bool canmove = false;
        private bool _isMoving;
        private Point? _buttonPosition;
        private double deltaX;
        private double deltaY;
        private TranslateTransform _currentTT;

        private void Samplebutton_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_buttonPosition == null)
                _buttonPosition = rectangles[rectangles.Count - 1].TransformToAncestor(canvas_main).Transform(new Point(0, 0));
            var mousePosition = Mouse.GetPosition(canvas_main);
            deltaX = mousePosition.X - _buttonPosition.Value.X;
            deltaY = mousePosition.Y - _buttonPosition.Value.Y;
            _isMoving = true;
        }

        private void Samplebutton_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            _currentTT = rectangles[rectangles.Count - 1].RenderTransform as TranslateTransform;
            _isMoving = false;
        }

        private void Samplebutton_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMoving) return;

            var mousePoint = Mouse.GetPosition(canvas_main);

            var offsetX = (_currentTT == null ? _buttonPosition.Value.X : _buttonPosition.Value.X - _currentTT.X) + deltaX - mousePoint.X;
            var offsetY = (_currentTT == null ? _buttonPosition.Value.Y : _buttonPosition.Value.Y - _currentTT.Y) + deltaY - mousePoint.Y;

            this.rectangles[rectangles.Count - 1].RenderTransform = new TranslateTransform(-offsetX, -offsetY);
        }
        */
    }
}
