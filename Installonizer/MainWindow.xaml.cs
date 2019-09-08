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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Installonizer
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

        private void BtSetPath_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)!cbBrowseFile.IsChecked)
            {
                System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog
                {
                    Description = "Select Folder to be installonized:",
                    ShowNewFolderButton = false,
                    RootFolder = Environment.SpecialFolder.UserProfile
                };
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbPath.Text = dialog.SelectedPath;
                }
                dialog.Dispose();
            }
            else
            {
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog()
                {
                    Title = "Select File to be installonized",
                    Filter = "Executables and Batch Files (*.exe, *.bat) | *exe; .bat | All Files (*.*) | *.*"
                };
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tbPath.Text = dialog.FileName;
                }
                dialog.Dispose();
            }
            
        }
    }
}
