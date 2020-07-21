using Staffing.InterfacesVM;
using Staffing.Model;
using Staffing.ViewModel;
using System.Windows;

namespace Staffing
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly StaffingModel model = new StaffingModel();
        private IMainViewModel viewModel /* = new OnlyViewVM() */;
        private StaffingWind staffingWind = new StaffingWind();
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            viewModel = new StaffingViewModel(model);
            staffingWind.DataContext = viewModel;
            MainWindow = staffingWind;
            ShutdownMode = ShutdownMode.OnMainWindowClose;
            MainWindow.Show();
        }
    }
}
