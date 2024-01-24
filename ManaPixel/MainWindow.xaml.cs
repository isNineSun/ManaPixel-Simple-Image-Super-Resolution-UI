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

namespace ManaPixel
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_DragEnter(object sender, DragEventArgs e)
        {
            Debug.WriteLine("拖拽图片进来了");
        }

        private void Grid_Drop(object sender, DragEventArgs e)
        {
            Debug.WriteLine("放下图片了");
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("鼠标移动");
        }
    }
}
