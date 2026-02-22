using MVVMFirma.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System;
using MVVMFirma.Helper;

namespace MVVMFirma.Views
{
    /// <summary>
    /// Interaction logic for LoginWindowView.xaml
    /// </summary>
    public partial class LoginWindowView : Window
    {
        public LoginWindowView()
        {
            InitializeComponent();
        //    var vm = new LoginWindowViewModel();
        //    vm.LoginSucceeded += OnLoginSucceeded;

        //    DataContext = vm;
        //}
        //private void OnLoginSucceeded()
        //{
        //    var mainWindow = new MainWindow();
        //    mainWindow.Show();

        //    this.Close();
        }
    }
}
