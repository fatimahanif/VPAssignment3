using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Assignment3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> files = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
            files_listbox.ItemsSource = files;
        }

        private void start_button_click(object sender, RoutedEventArgs e)
        {
            searchFiles(path_textBox.Text);
        }

        private void searchFiles(string path) 
        {
            //browsing subdirectories
            foreach (string directory in Directory.GetDirectories(path))
            {
                searchFiles(directory);
            }
            //browsing directory
            foreach (string file in Directory.GetFiles(path))
            {
                Console.WriteLine(file);
                //files_listbox.Items.Refresh();
                //System.Threading.Thread.Sleep(100);
                //files_listbox.Items.Add(file);
                //files.Add(file);
                //files_listbox.Items.Refresh();
            }
            return;
        }
    }
}
