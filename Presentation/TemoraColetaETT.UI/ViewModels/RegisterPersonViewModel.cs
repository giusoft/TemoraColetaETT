using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
            NextButtonText = "Avançar para Biometria Facial";
        }

        [RelayCommand]
        async Task NextStep()
        {
            await Shell.Current.GoToAsync("FacialBiometricsView");
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
    }
}
