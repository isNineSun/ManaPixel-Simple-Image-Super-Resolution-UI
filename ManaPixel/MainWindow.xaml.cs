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
using System.IO;
using System.Security.Policy;
using Microsoft.Win32;

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

        // 设置 Border 的背景为指定图片
        private void SetBorderBackground(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                ImageBrush imageBrush = new ImageBrush(bitmap);
                imageBrush.Stretch = Stretch.UniformToFill;
                InputBox.Background = imageBrush;
            }
            else
            {
                BitmapImage bitmap = new BitmapImage(new Uri(@imagePath));
                ImageBrush imageBrush = new ImageBrush(bitmap);
                imageBrush.Stretch = Stretch.UniformToFill;
                InputBox.Background = imageBrush;
            }
        }

        private void input_Image(string Path, bool isFloder)
        {
            string DefaultImagePath = "pack://application:,,,/Images/FloderImg.png";//相对路径

            if (isFloder)
            {
                InputTextBox.Text = Path;
                // 设置 Border 的背景为默认文件夹图片
                SetBorderBackground(DefaultImagePath);
            }
            else
            {
                InputTextBox.Text = Path;
                SetBorderBackground(Path);
            }
        }

        private void Images_File_DragEnter(object sender, DragEventArgs e)
        {
            Debug.WriteLine("拖拽图片进来了");
        }

        private void Images_File_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    // 获取拖拽的第一个文件路径
                    string imagePath = files[0];

                    if (File.Exists(imagePath))//是文件
                    {
                        input_Image(imagePath, false);
                    }
                    else if (Directory.Exists(imagePath))// 是文件夹
                    {
                        input_Image(imagePath, true);
                    }
                }
            }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp;*.ico";

            if (openFileDialog.ShowDialog() == true)
            {
                // 获取所选文件的路径
                string selectedFilePath = openFileDialog.FileName;

                input_Image(selectedFilePath, false);
            }
        }
    }
}
