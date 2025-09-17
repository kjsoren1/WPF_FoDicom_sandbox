using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfAppTutorial.ViewModels;

namespace WpfAppTutorial
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
            LogManager.GetLog = type => new DebugLog(type);
        }

		protected override async void OnStartup(object sender, StartupEventArgs e)
		{
			await DisplayRootViewForAsync(typeof(ShellViewModel));
		}
	}
}
