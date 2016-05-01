using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;

using System.Windows.Forms;
using System.IO;

namespace SubfoldersFilesExtractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        static string fromPath = "";
        private void SpecifyFrom_button_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();

            var showingResult = fbd.ShowDialog();

            if (showingResult != System.Windows.Forms.DialogResult.OK) return;

            fromPath = fbd.SelectedPath;

            FromPath_label.Content = fromPath;
        }

        static string toPath = "";
        private void SpecifyTo_button_Click(object sender, RoutedEventArgs e)
        {
            var fbd = new FolderBrowserDialog();

            var showingResult = fbd.ShowDialog();

            if (showingResult != System.Windows.Forms.DialogResult.OK) return;

            toPath = fbd.SelectedPath;

            ToPath_label.Content = toPath;
        }

        private void Go_button_Click(object sender, RoutedEventArgs e)
        {
            if(fromPath == "" || toPath == "")
            {
                System.Windows.MessageBox.Show("Specify From and To paths first.");

                return;
            }



            var allFiles = FilesHelper.GetAllFilesFromDirectory_Recursively(fromPath);

            foreach(var filePath in allFiles)
            {
                var fullFileName = Path.GetFileName(filePath);
                var fileName = Path.GetFileNameWithoutExtension(filePath);
                var fileExtension = Path.GetExtension(filePath);

                var newPath = toPath + @"\" + fullFileName;


                if(!File.Exists(newPath))
                {
                    File.Move(filePath, newPath);

                    continue;
                }



                for(int copyNumber = 1; ;copyNumber++)
                {
                    newPath = $@"{toPath}\{fileName}_copy{copyNumber}.{fileExtension}";

                    if (!File.Exists(newPath))
                    {
                        File.Move(filePath, newPath);

                        break;
                    }
                }
            }


            System.Windows.MessageBox.Show("Done.");
        }
    }
}
