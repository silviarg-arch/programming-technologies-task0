using System.Windows;
using ProgrammingTechnologiesTask2.Presentation.ViewModel.ViewModels;

namespace ProgrammingTechnologiesTask2.Presentation.View
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainViewModel viewModel = new MainViewModel();

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = viewModel;
            mainWindow.Show();

            viewModel.LoadDataCommand.Execute(null);
        }
    }
}