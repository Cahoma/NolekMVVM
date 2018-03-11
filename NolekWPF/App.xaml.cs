using Autofac;

using NolekWPF.Model;
using NolekWPF.Startup;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using NolekWPF.DataAccess;
using NolekWPF.Data.Repositories;

namespace NolekWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //private IErrorDataService _errorDataService;
        private IErrorRepository errorRepository;
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.Bootstrap();
            try
            {
                var mainWindow = container.Resolve<MainWindow>(); //the main window must have the view model passed to its constructor, and the viewmodel needs the data access passed to its constructor
                                                                  //resolve will automatically inject all required object to constructors 
                mainWindow.Show();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Unhandled Exception", MessageBoxButton.OK, MessageBoxImage.Error); e.Handled = false;
            errorRepository = new ErrorRepository(new wiki_nolek_dk_dbEntities());

            //add the error to database
            Error error = new Error
            {
                ErrorMessage = e.Exception.Message,
                ErrorTimeStamp = DateTime.Now,
                ErrorStackTrace = e.Exception.StackTrace
            };

            errorRepository.Add(error);
            errorRepository.SaveAsync();
        }
    }
}
