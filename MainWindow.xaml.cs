using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
namespace Assignment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> files = new List<string>();
        BackgroundWorker backgroundWorker = new BackgroundWorker();
        string currentPath = "";
        bool? imagecheck, audiocheck, videocheck, pdfcheck;
        public MainWindow()
        {
            InitializeComponent();
            files_listbox.ItemsSource = files;

            //Background Methods
            backgroundWorker.DoWork += backgroundWorker_DoWork;
            backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;

            //Background Properties
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
        }

        #region Background Method 
        // Background Worker Methods
        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("completed");
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //files_listbox.Items.Add(files[e.ProgressPercentage]);
            // System.Threading.Thread.Sleep(100);
            //files_listbox.Items[0] = "hello";
            files_listbox.Items.Refresh();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            searchFiles(currentPath, e);
        }
        #endregion
        private void start_button_click(object sender, RoutedEventArgs e)
        {
            currentPath = path_textBox.Text;
            imagecheck = image_checkbox.IsChecked;
            audiocheck = audio_checkbox.IsChecked;
            videocheck = video_checkbox.IsChecked;
            pdfcheck = pdf_checkbox.IsChecked;
            if (!backgroundWorker.IsBusy)
            {
                backgroundWorker.RunWorkerAsync();
                start_button.Content = "Stop";
            }
            else 
            {
                backgroundWorker.CancelAsync();
                start_button.Content = "Start";                                                                                                                                                 
            }
           // searchFiles(path_textBox.Text);
        }

        #region browsing directory and subdirectories
        private void searchFiles(string path, DoWorkEventArgs e)
        {
            if (backgroundWorker.CancellationPending) 
            {
                e.Cancel = true;
            }
            //System.Threading.Thread.Sleep(100);

            //browsing subdirectories
            foreach (string directory in Directory.GetDirectories(path))
            {
              //  System.Threading.Thread.Sleep(100);

                searchFiles(directory, e);

            }
           // System.Threading.Thread.Sleep(100);
            //browsing directory
            foreach (string file in Directory.GetFiles(path))
            {
                string extension = GetExtension(getFileOrFolder(file));
                if (imagecheck == true && (extension.Equals("jpg") || extension.Equals("png") || extension.Equals("gif")))
                {
                    files.Add($"{getFileOrFolder(file)}: {file}");
                }
                if (pdfcheck == true && extension.Equals("txt"))
                {
                    files.Add($"{getFileOrFolder(file)}: {file}");
                   

                }
                if (audiocheck == true && (extension.Equals("mp3") || extension.Equals("aac")))
                {
                    files.Add($"{getFileOrFolder(file)}: {file}");
                }
                if (videocheck== true && extension.Equals("mp4"))
                {
                    files.Add($"{getFileOrFolder(file)}: {file}");
                }
                Console.WriteLine(Path.GetExtension(path));
                backgroundWorker.ReportProgress(0);
                //files_listbox.Items.Refresh();
                //System.Threading.Thread.Sleep(100);
                //files_listbox.Items.Add(file);
            }

           
            return;
        }
        #endregion

        #region Helper Function
        // Get the file or folder name
        public static string getFileOrFolder(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            string normalizedPath = path.Replace("/", "\\");
            int lastIndex = normalizedPath.LastIndexOf('\\');
            if (lastIndex <= 0)
                return path;
            return path.Substring(lastIndex + 1);
        }

        #region Get Extension function
        /// <summary>
        /// Return the Extension 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return string.Empty;
            int lastIndex = fileName.LastIndexOf('.');
            return fileName.Substring(lastIndex + 1);
        }
        #endregion

        #endregion 
    }
}
