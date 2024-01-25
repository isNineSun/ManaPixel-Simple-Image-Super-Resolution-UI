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
using System.Management;

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
        string[] ModleList =
        {
            "realesrgan-x4plus",
            "realesrnet-x4plus",
            "realesrgan-x4plus-anime",
            "realesr-animevideov3"
        };

        public MainWindow()
        {
            InitializeComponent();

            ModelSelection.ItemsSource = ModleList;
        }

        /// <summary>
        /// 设置 Border 的背景为指定图片
        /// </summary>
        /// <param name="imagePath"></param>
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

        /// <summary>
        /// 信息提示框弹出SnackBar
        /// </summary>
        /// <param name="message"></param>
        private void ShowSnackbarMessage(string message)
        {
            MySnackbar.MessageQueue.Enqueue(message);
        }

        /// <summary>
        /// 输入图片的处理
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="isFloder"></param>
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

        /// <summary>
        /// 当路径为文件时的处理
        /// </summary>
        /// <param name="FilePath"></param>
        private void Path_is_File_Process(string FilePath)
        {
            input_Image(FilePath, false);
            OutputTextBox.Text = System.IO.Path.GetDirectoryName(FilePath);
            OutputNameTextBox.Text = System.IO.Path.GetFileNameWithoutExtension(FilePath) + "-ex";
            OutputNameTextBox.IsEnabled = true;
        }

        /// <summary>
        /// 当路径为文件夹时的处理
        /// </summary>
        /// <param name="FloderPath"></param>
        private void Path_is_Floder_Process(string FloderPath)
        {
            input_Image(FloderPath, true);
            OutputTextBox.Text = FloderPath;
            OutputNameTextBox.IsEnabled = false;
            OutputNameTextBox.Text = "---";
        }

        /// <summary>
        /// 组合输出路径
        /// </summary>
        private string CombinOutputPath() 
        {
            string rootPath = OutputTextBox.Text;
            string subFolder = "\\";
            string fileName = OutputNameTextBox.Text;
            string fileExtension = "." + fomatBlock.Text;

            string fullPath = rootPath + subFolder + fileName + fileExtension;

            return fullPath;
        }

        /// <summary>
        /// 获取GPU列表
        /// </summary>
        /// <returns></returns>
        static string[] GetGPUList()
        {
            // 使用WMI查询获取GPU信息
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            var gpuCollection = searcher.Get();

            // 保存GPU信息到字符串数组
            List<string> gpuList = new List<string>();
            int index = 0;

            foreach (ManagementObject gpu in gpuCollection)
            {
                string gpuName = gpu["Name"].ToString();

                // 过滤掉虚拟设备
                if (!gpuName.Contains("OrayIddDriver"))
                {
                    string gpuInfo = $"{index}. {gpuName}";
                    gpuList.Add(gpuInfo);
                    index++;
                }
            }

            return gpuList.ToArray();
        }

        /// <summary>
        /// 执行CMD指令：超分辨率指令，MANA！
        /// </summary>
        /// <param name="inputImagePath"></param>
        /// <param name="outputImagePath"></param>
        /// <param name="modelName"></param>
        public static void ExecuteCommand(string inputImagePath, string outputImagePath, string modelName)
        {
            try
            {
                string executablePath = "./realesrgan/realesrgan-ncnn-vulkan.exe"; // 替换为实际的可执行文件路径

                // 构建命令行参数字符串
                string arguments = $"-i {inputImagePath} -o {outputImagePath} -n {modelName}";

                // 创建 ProcessStartInfo 对象
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = executablePath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                // 创建 Process 对象并启动
                using (Process process = new Process { StartInfo = psi })
                {
                    // 打印实际执行的命令
                    Console.WriteLine($"Executing command: {executablePath} {arguments}");

                    process.Start();

                    // 等待进程结束
                    process.WaitForExit();

                    // 处理输出（可选）
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    Console.WriteLine("Command Output:");
                    Console.WriteLine(output);

                    Console.WriteLine("Command Error:");
                    Console.WriteLine(error);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
            }
        }

        /// <summary>
        /// MANA功能管理
        /// </summary>
        private void ManagerMANA()
        {
            
        }

        //-----------------------------------------------事件-----------------------------------------------------//
        /// <summary>
        /// 拖入文件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Images_File_DragEnter(object sender, DragEventArgs e)
        {
            Debug.WriteLine("拖拽图片进来了");
        }

        /// <summary>
        /// 拖入并放下文件事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 打开资源管理器选择图片事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 拖放文件夹事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 打开资源管理器选择文件夹事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 获取GPU列表事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPUListRefresh(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.ComboBox GpuList = sender as System.Windows.Controls.ComboBox;

            GpuList.ItemsSource = GetGPUList();
        }

        /// <summary>
        /// 在资源管理器中打开输出文件夹事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenOutPutPathFloder(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OutputTextBox.Text))
            {
                try
                {
                    // 使用 Process.Start 打开文件夹
                    Process.Start("explorer.exe", OutputTextBox.Text);
                }
                catch (Exception ex)
                {
                    // 处理异常
                    ShowSnackbarMessage($"无法打开文件夹: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 超分辨率事件执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Mana_Start(object sender, RoutedEventArgs e)
        {
            ExecuteCommand(InputTextBox.Text, CombinOutputPath(), ModleList[ModelSelection.SelectedIndex]);
        }
    }
}
