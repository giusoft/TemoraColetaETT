using TemoraColetaETT.UI.ViewModels;
using TemoraColetaETT.UI.Views;

namespace TemoraColetaETT.UI
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
            Routing.RegisterRoute(nameof(DashboardView), typeof(DashboardView));
            Routing.RegisterRoute(nameof(RegisterPersonView), typeof(RegisterPersonView));
            Routing.RegisterRoute(nameof(RegisterBiometricsFacialView), typeof(RegisterBiometricsFacialView));
        }
    }
}