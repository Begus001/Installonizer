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
using Path = System.IO.Path;

namespace Installonizer
{
	public partial class MainWindow : Window
	{
		FileType setFileType;
		string filename, filenameExt;

		public MainWindow()
		{
			InitializeComponent();
		}

		private enum FileType
		{
			Executable,
			Directory,
			Other,
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
					Filter = "Executables and Batch Files (*.exe, *.bat)|*.exe;*.bat|All Files (*.*)|*.*"
				};

				if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					filename = Path.GetFileNameWithoutExtension(dialog.FileName);
					filenameExt = Path.GetFileName(dialog.FileName);
					tbPath.Text = dialog.FileName;

					if (Path.GetExtension(dialog.FileName) == ".exe" || Path.GetExtension(dialog.FileName) == ".bat")
					{
						setFileType = FileType.Executable;
					}
					else
					{
						setFileType = FileType.Other;
					}
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
			Installonizor installonizer = new Installonizor();

			if (!String.IsNullOrWhiteSpace(tbPath.Text))
			{
				if (setFileType != FileType.NotSet)
				{
					if (setFileType == FileType.Directory)
					{
						if ((bool)!cbX86.IsChecked)
						{
							if (installonizer.MoveDirectory(tbPath.Text, filename, false))
							{
								SuccessMessage(true);
								return;
							}
							else
							{
								FailedMessage(true);
								return;
							}
						}
						else
						{
							if (installonizer.MoveDirectory(tbPath.Text, filename, true))
							{
								SuccessMessage(true);
								return;
							}
							else
							{
								FailedMessage(true);
								return;
							}
						}

					}
					else if (setFileType == FileType.Executable)
					{
						if ((bool)!cbX86.IsChecked)
						{
							if (installonizer.MoveFile(tbPath.Text, filename, filenameExt, false))
							{
								SuccessMessage(false);
								MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + filename + "\n" + tbPath.Text + "\n" + Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + filename + "\\" + filenameExt);
								return;
							}
							else
							{
								FailedMessage(false);
								return;
							}
						}
						else
						{
							if (installonizer.MoveFile(tbPath.Text, filename, filenameExt, true))
							{
								SuccessMessage(false);
								return;
							}
							else
							{
								FailedMessage(false);
								return;
							}
						}
					}
					else
					{
						if (MessageBox.Show("The entered file is not an executable or a batch file.\nDo you want to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
						{
							filenameExt = Path.GetFileName(tbPath.Text);


							if ((bool)!cbX86.IsChecked)
							{
								if (installonizer.MoveFile(tbPath.Text, filename, filenameExt, false))
								{
									SuccessMessage(false);
									return;
								}
								else
								{
									FailedMessage(false);
									return;
								}
							}
							else
							{
								if (installonizer.MoveFile(tbPath.Text, filename, filenameExt, true))
								{
									SuccessMessage(false);
									return;
								}
								else
								{
									FailedMessage(false);
									return;
								}
							}
						}
					}
				}
				else
				{
					filename = Path.GetFileNameWithoutExtension(tbPath.Text);

					if (Path.GetExtension(tbPath.Text) == "")
					{
						setFileType = FileType.Directory;

						if ((bool)!cbX86.IsChecked)
						{
							if (installonizer.MoveDirectory(tbPath.Text, filename, false))
							{
								SuccessMessage(false);
								return;
							}
							else
							{
								FailedMessage(false);
								return;
							}
						}
						else
						{
							if (installonizer.MoveDirectory(tbPath.Text, filename, true))
							{
								SuccessMessage(false);
								return;
							}
							else
							{
								FailedMessage(false);
								return;
							}
						}
					}
					else if (Path.GetExtension(tbPath.Text) == ".exe" || Path.GetExtension(tbPath.Text) == ".bat")
					{
						filenameExt = Path.GetFileName(tbPath.Text);
						setFileType = FileType.Executable;

						if ((bool)!cbX86.IsChecked)
						{
							if (installonizer.MoveFile(tbPath.Text, filename, filenameExt, false))
							{
								SuccessMessage(false);
								return;
							}
							else
							{
								FailedMessage(false);
								return;
							}
						}
						else
						{
							if (installonizer.MoveFile(tbPath.Text, filename, filenameExt, true))
							{
								SuccessMessage(false);
								return;
							}
							else
							{
								FailedMessage(false);
								return;
							}
						}
					}
					else
					{
						if (MessageBox.Show("The entered file is not an executable or a batch file.\nDo you want to continue?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
						{
							filenameExt = Path.GetFileName(tbPath.Text);
							setFileType = FileType.Other;

							if ((bool)!cbX86.IsChecked)
							{
								if (installonizer.MoveFile(tbPath.Text, filename, filenameExt, false))
								{
									SuccessMessage(false);
									return;
								}
								else
								{
									FailedMessage(false);
									return;
								}
							}
							else
							{
								if (installonizer.MoveFile(tbPath.Text, filename, filenameExt, true))
								{
									SuccessMessage(false);
									return;
								}
								else
								{
									FailedMessage(false);
									return;
								}
							}
						}
					}
				}
			}
			else
			{
				MessageBox.Show("No path was entered.\nPlease enter a path or browse for a directory or a file.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
			}

			setFileType = FileType.NotSet;
		}

		private void BtAbout_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Installonizer Version 0.2.0\nby Benjamin Goisser 2019\n\nhttps://github.com/Begus001/Installonizor", "About", MessageBoxButton.OK, MessageBoxImage.Information);
		}

		private void SuccessMessage(bool dir)
		{
			if (dir)
			{
				MessageBox.Show("Directory was successfully installonized!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
			}
			else
			{
				MessageBox.Show("File was successfully installonized!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void FailedMessage(bool dir)
		{
			if (dir)
			{
				MessageBox.Show("Directory could not be moved!\nDirectory either doesn't exist, already exists in destination or you don't possess the permission to move it.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			else
			{
				MessageBox.Show("File could not be moved!\nFile either doesn't exist, already exists in destination, you don't possess the permission to move it or folder already exists in destination.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
