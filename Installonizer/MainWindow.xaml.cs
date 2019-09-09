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
using System.IO;
using Path = System.IO.Path;

namespace Installonizer
{
	public partial class MainWindow : Window
	{
		FileType setFileType;
		string filename;

		public MainWindow()
		{
			InitializeComponent();
		}

		private enum FileType
		{
			Executable,
			Directory,
			NotSet
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
					filename = Path.GetFileNameWithoutExtension(dialog.SelectedPath);
					tbPath.Text = dialog.SelectedPath;
					setFileType = FileType.Directory;
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
					filename = Path.GetFileNameWithoutExtension(dialog.FileName);
					tbPath.Text = dialog.FileName;
					setFileType = FileType.Executable;
				}
				dialog.Dispose();
			}

		}

		private void TbPath_TextChanged(object sender, TextChangedEventArgs e)
		{
			setFileType = FileType.NotSet;
		}

		private void BtInstallonize_Click(object sender, RoutedEventArgs e)
		{
			if (setFileType != FileType.NotSet)
			{
				if (setFileType == FileType.Directory)
				{
					if ((bool)!cbX86.IsChecked)
					{
						try
						{
							MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + filename);
							Directory.Move(tbPath.Text, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + filename);
						}
						catch
						{

						}
					}
					else
					{
						try
						{
							MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + filename);
							Directory.Move(tbPath.Text, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + filename);
						}
						catch
						{

						}
					}
				}
			}
		}
	}
}
