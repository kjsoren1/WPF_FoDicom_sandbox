using Caliburn.Micro;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTutorial.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {

		private string _firstName = "Kelly";
		private string _directorySelected = "Select Directory";
		private string _fileSelected = "Select File";

		public string FirstName
		{
			get { return _firstName; }
			set
			{ 
				_firstName = value;
				NotifyOfPropertyChange(() => FirstName);
			}
		}

		public string DirectorySelected
		{
			get { return _directorySelected; }
			set
			{
				_directorySelected = value;
				NotifyOfPropertyChange(nameof(DirectorySelected));
			}
		}

		public string FileSelected
		{
			get { return _fileSelected; }
			set
			{
				_fileSelected = value;
				NotifyOfPropertyChange(nameof(FileSelected));
			}
		}

		public bool CanSelectDirectory()
		{
			return true;
		}

		public bool CanSelectFile()
		{
			return true;
		}

		public void SelectDirectory()
		{
			OpenFolderDialog folderDialog = new OpenFolderDialog
			{
				Title = "Select Folder",
				InitialDirectory = DirectorySelected
			};

			if (folderDialog.ShowDialog() == true)
			{
				DirectorySelected = folderDialog.FolderName;
				//NotifyOfPropertyChange(() => DirectorySelected);
			}
		}
		public void SelectFile()
		{
			OpenFileDialog fileDialog = new OpenFileDialog
			{
				Title = "Select File",
				InitialDirectory = DirectorySelected
			};

			if (fileDialog.ShowDialog() == true)
			{
				FileSelected = fileDialog.FileName;
			}
		}

		public async void LoadPageOne()
		{
			await ActivateItemAsync(new FirstChildViewModel(FileSelected), new CancellationToken());
		}

		public async void LoadPageTwo()
		{
			await ActivateItemAsync(new SecondChildViewModel(), new CancellationToken());
		}
	}
}
