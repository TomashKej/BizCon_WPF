using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using MVVMFirma.Views;
using MVVMFirma.ViewModels;

namespace MVVMFirma
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            LoginWindowView window = new LoginWindowView();
            var viewModel = new LoginWindowViewModel();
            window.DataContext = viewModel;
            
            viewModel.RequestClose += (s, ev) => window.Close();
            window.Show();
            //base.OnStartup(e);
            //MainWindow window = new MainWindow();
            //var viewModel = new MainWindowViewModel();
            //window.DataContext = viewModel;
            //window.Show();
        }
    }

}
