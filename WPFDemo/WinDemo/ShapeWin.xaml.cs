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

namespace WinDemo
{
    /// <summary>
    /// ShapeWin.xaml 的交互逻辑
    /// </summary>
    public partial class ShapeWin : Window
    {
        public ShapeWin()
        {
            InitializeComponent();

            using(StreamGeometryContext sgc = streamGeometry.Open())
            {
                sgc.BeginFigure(new Point(0, 0), true, true);

                sgc.LineTo(new Point(200, 50), true, true);

                sgc.ArcTo(new Point(100, 20), new Size(100, 40), 90, true, SweepDirection.Clockwise,
                    true, true);
            }
        }
    }
}
