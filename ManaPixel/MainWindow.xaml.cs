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
using System.Windows.Forms;

using DragEventArgs = System.Windows.DragEventArgs;
using DataFormats = System.Windows.DataFormats;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using MaterialDesignThemes.Wpf;

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

        private void ShowSnackbarMessage(string message)
        {
            MySnackbar.MessageQueue.Enqueue(message);
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

        private void Path_is_File_Process(string FilePath)
        {
            input_Image(FilePath, false);
            OutputTextBox.Text = System.IO.Path.GetDirectoryName(FilePath);
            OutputNameTextBox.Text = System.IO.Path.GetFileNameWithoutExtension(FilePath) + "-ex";
            OutputNameTextBox.IsEnabled = true;
        }

        private void Path_is_Floder_Process(string FloderPath)
        {
            input_Image(FloderPath, true);
            OutputTextBox.Text = FloderPath;
            OutputNameTextBox.IsEnabled = false;
            OutputNameTextBox.Text = "---";
        }

        //--------------------------------------------事件------------------------------------------------//
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
                        Path_is_File_Process(imagePath);

                    }
                    else if (Directory.Exists(imagePath))// 是文件夹
                    {
                        Path_is_Floder_Process(imagePath);
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

                Path_is_File_Process(selectedFilePath);
            }
        }

        private void Floder_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    // 获取拖拽的第一个文件路径
                    string FloderPath = files[0];

                    if (File.Exists(FloderPath))//是文件
                    {
                        ShowSnackbarMessage("这不是一个文件夹");
                    }
                    else if (Directory.Exists(FloderPath))// 是文件夹
                    {
                        OutputTextBox.Text = FloderPath;
                    }
                }
            }
        }

        private void SelectFolderButton_Click(object sender, RoutedEventArgs e)
        {
            // 创建FolderBrowserDialog对象
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // 显示对话框
            DialogResult result = folderBrowserDialog.ShowDialog();

            // 检查用户是否点击了“确定”按钮
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                // 获取所选文件夹的路径
                string selectedFolderPath = folderBrowserDialog.SelectedPath;

                OutputTextBox.Text = selectedFolderPath;
            }
        }
    }
}
