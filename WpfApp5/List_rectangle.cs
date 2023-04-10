using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp5
{
    class List_rectangle
    {
        private static readonly Dictionary<int, System.Windows.Media.Color> colors = new Dictionary<int, System.Windows.Media.Color>
        {
            [0] = Colors.Red,
            [1] = Colors.Orange,
            [2] = Colors.Yellow,
            [3] = Colors.Green,
            [4] = Colors.Blue,
            [5] = Colors.SkyBlue,
            [6] = Colors.Purple,
            [7] = Colors.Pink
        };

        public int start_width = 210;
        public int step_width = 20;
        public int start_top = 330;
        public int step_top = 30;
        public int start_left = 35;
        public int step_left = 10;

        private readonly List<System.Windows.Shapes.Rectangle> rectangles = new List<System.Windows.Shapes.Rectangle>();

        public List<System.Windows.Shapes.Rectangle> Get_list()
        {
            return rectangles;
        }
        public void Fill_list(int value)
        {
            int width = start_width - step_width * (8 - value);
            int left = start_left + step_left * (8 - value);
            int top = start_top;

            for (int i = 8 - value; i < 8; ++i)
            {
                System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle
                {
                    StrokeThickness = 1,
                    Stroke = new SolidColorBrush(Colors.Black),
                    Fill = new SolidColorBrush(colors[i]),
                    Height = 30,
                    Width = width
                };
                Canvas.SetTop(rect, top);
                Canvas.SetLeft(rect, left);

                rectangles.Add(rect);

                width -= step_width;
                top -= step_top;
                left += step_left;
            }
        }
    }
}
