using Caliburn.Micro;
using FellowOakDicom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppTutorial.ViewModels
{
    public class SecondChildViewModel : Screen
    {

		private string _fileToShow = "File to show";
		private string _patientName = "";


		public SecondChildViewModel() { }

		public SecondChildViewModel(string fileToShow) : base() {
			FileToShow = fileToShow;
		}

		public string FileToShow {
			get { return _fileToShow; }
			set {
				_fileToShow = value;

				PatientName = ReadPatientNameFromFile(_fileToShow);
			}
		}

		public string PatientName {
			get { return _patientName; }
			set { 
				_patientName = value;
				NotifyOfPropertyChange(nameof(PatientName));
			}
		}

		private string ReadPatientNameFromFile(string fileName) {
			string patientName = "";
			var dicomFile = DicomFile.Open(fileName);

			var toString = dicomFile.ToString();
			patientName = dicomFile.Dataset.GetString(DicomTag.PatientName);

			return patientName;
		}

	}

}
