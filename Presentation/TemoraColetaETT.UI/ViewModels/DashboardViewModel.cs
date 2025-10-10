using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemoraColetaETT.Application.Interfaces;
using TemoraColetaETT.Infrastructure.Services;

namespace TemoraColetaETT.UI.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IAuthService _authService;

        [ObservableProperty]
        private string _welcomeMessage = "Bem-vindo!";

        [ObservableProperty]
        private bool _isBusy;

        public DashboardViewModel(IAuthService authService)
        {
            _authService = authService;
        }

        [ObservableProperty]
        private string _authToken;

        [RelayCommand]
        public async Task LoadDashboardAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                // Sua lógica aqui
                AuthToken = await _authService.GetTokenAsync();
                LogService.LogInfo($"Token recuperado: {AuthToken}");
                WelcomeMessage = "Dashboard carregado!";
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task InitializeAsync()
        {
            if (LoadDashboardCommand.CanExecute(null))
            {
                LoadDashboardCommand.Execute(null);
            }
        }
    }
}