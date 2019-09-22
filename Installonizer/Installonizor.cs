using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using File = System.IO.File;
using IWshRuntimeLibrary;

namespace Installonizer
{
	class Installonizor
	{
		public bool MoveFile(string path, string filename, string filenameExt, bool x86)
		{
			try
			{
				if (!x86)
				{
					filename = UppercaseString(filename);
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + filename);
					File.Move(path, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + filename + "\\" + filenameExt);

					WshShell shell = new WshShell();
					IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) + "\\Programs\\" + filename + ".lnk");
					shortcut.TargetPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + filename + "\\" + filenameExt;
					shortcut.Save();
				}
				else
				{
					filename = UppercaseString(filename);
					Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + filename);
					File.Move(path, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + filename + "\\" + filenameExt);

					WshShell shell = new WshShell();
					IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu) + "\\Programs\\" + filename + ".lnk");
					shortcut.TargetPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + filename + "\\" + filenameExt;
					shortcut.Save();
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool MoveDirectory(string path, string filename, bool x86)
		{
			try
			{
				if (!x86)
				{
					filename = UppercaseString(filename);
					Directory.Move(path, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\" + filename);
				}
				else
				{
					filename = UppercaseString(filename);
					Directory.Move(path, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + "\\" + filename);
				}

				return true;
			}
			catch
			{
				return false;
			}
		}

		private string UppercaseString(string inputString)
		{
			if (!Char.IsUpper(inputString[0]))
			{
				return Char.ToUpper(inputString[0]) + inputString.Substring(1);
			}
			else
			{
				return inputString;
			}
		}
	}
}
