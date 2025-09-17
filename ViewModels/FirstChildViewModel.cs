using Caliburn.Micro;
using FellowOakDicom;
using FellowOakDicom.Media;
using FellowOakDicom.Network;
using FellowOakDicom.Network.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfAppTutorial.ViewModels
{
	public class FirstChildViewModel: Screen
	{
		private string _fileToShow = "File to show";
		private string _instanceUID = "";


		public FirstChildViewModel() { }

		public FirstChildViewModel(string fileToShow) : base() {
			FileToShow = fileToShow;
		}

		public string FileToShow {
			get { return _fileToShow; }
			set {
				_fileToShow = value;


				InstanceUID = ReadUIDFromFile(_fileToShow);
			}
		}

		public string InstanceUID {
			get { return _instanceUID; }
			set { 
				_instanceUID = value;
				NotifyOfPropertyChange(nameof(InstanceUID));
			}
		}

		private string ReadUIDFromFile(string fileName) {
			string instanceUID = "";
			var dicomFile = DicomFile.Open(fileName);

			var toString = dicomFile.ToString();
			instanceUID = dicomFile.Dataset.GetString(DicomTag.SOPInstanceUID);

			return instanceUID;
		}

	}

	/* COPY_PASTE from fo-dicom readme
	public class CustomDependency {

	}

	public class Worker: IHostedService
	{
		private readonly ILogger<Worker> _logger;
		private readonly IDicomServerFactory _dicomServerFactory;
		private readonly IDicomClientFactory _dicomClientFactory;
		private IDicomServer? _server;

		public Worker(ILogger<Worker> logger, IDicomServerFactory dicomServerFactory, IDicomClientFactory dicomClientFactory) {
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_dicomServerFactory = dicomServerFactory ?? throw new ArgumentNullException(nameof(dicomServerFactory));
			_dicomClientFactory = dicomClientFactory ?? throw new ArgumentNullException(nameof(dicomClientFactory));
		}

		public async Task StartAsync(CancellationToken cancellationToken) {
			_logger.LogInformation("Starting DICOM server");
			_server = _dicomServerFactory.Create<EchoService>(104);
			_logger.LogInformation("DICOM server is running");

			var client = _dicomClientFactory.Create("127.0.0.1", 104, false, "AnySCU", "AnySCP");

			_logger.LogInformation("Sending C-ECHO request");
			DicomCEchoResponse? response = null;
			await client.AddRequestAsync(new DicomCEchoRequest { OnResponseReceived = (_, r) => response = r });
			await client.SendAsync(cancellationToken);
			if (response != null) {
				_logger.LogInformation("C-ECHO response received");
			} else {
				_logger.LogError("No C-ECHO response received");
			}
		}

		public Task StopAsync(CancellationToken cancellationToken) {
			if (_server != null) {
				_server.Stop();
				_server.Dispose();
				_server = null;
			}
			return Task.CompletedTask;
		}
	}

	public class EchoService: DicomService, IDicomServiceProvider, IDicomCEchoProvider
	{
		private readonly ILogger _logger;
		private readonly CustomDependency _customDependency;

		public EchoService(INetworkStream stream,
			Encoding fallbackEncoding,
			ILogger logger,
			DicomServiceDependencies dependencies,
			CustomDependency customDependency) : base(stream, fallbackEncoding, logger, dependencies) {
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
			_customDependency = customDependency ?? throw new ArgumentNullException(nameof(customDependency));
		}

		public void OnReceiveAbort(DicomAbortSource source, DicomAbortReason reason) => _logger.LogInformation("Received abort");
		public void OnConnectionClosed(Exception exception) => _logger.LogInformation("Connection closed");

		public Task OnReceiveAssociationRequestAsync(DicomAssociation association) {
			foreach (DicomPresentationContext presentationContext in association.PresentationContexts)
				presentationContext.SetResult(DicomPresentationContextResult.Accept);
			return SendAssociationAcceptAsync(association);
		}

		public Task OnReceiveAssociationReleaseRequestAsync() {
			_logger.LogInformation("Received association release");
			return Task.CompletedTask;
		}

		public Task<DicomCEchoResponse> OnCEchoRequestAsync(DicomCEchoRequest request) => Task.FromResult(new DicomCEchoResponse(request, DicomStatus.Success));
	}
	*/
}
