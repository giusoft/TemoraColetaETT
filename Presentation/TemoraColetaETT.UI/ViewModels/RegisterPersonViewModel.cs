using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemoraColetaETT.UI.Views;

namespace TemoraColetaETT.UI.ViewModels
{
    [QueryProperty(nameof(PersonId), "personId")]
    [QueryProperty(nameof(RegistrationType), "registrationType")]
    public partial class RegisterPersonViewModel : ObservableObject
    {
        [ObservableProperty]
        string personId;

        [ObservableProperty]
        string registrationType;

        [ObservableProperty]
        bool isDriverRegistration;

        [ObservableProperty]
        string nextButtonText;

        public RegisterPersonViewModel()
        {
            NextButtonText = "Avançar";
        }

        partial void OnPersonIdChanged(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                // Lógica para carregar os dados da pessoa com base no ID
                // Por exemplo: LoadPersonData(value);
            }
        }

        partial void OnRegistrationTypeChanged(string value)
        {
            IsDriverRegistration = string.Equals(value, "condutor", StringComparison.OrdinalIgnoreCase);
        }

        [RelayCommand]
        private async Task NextStepAsync()
        {
            // Aqui você pode adicionar lógicas de validação ou salvamento de dados antes de navegar
            await Shell.Current.GoToAsync(nameof(FacialBiometricsView));
        }
    }
}
