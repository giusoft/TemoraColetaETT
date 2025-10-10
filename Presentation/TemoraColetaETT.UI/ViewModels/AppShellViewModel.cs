using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemoraColetaETT.UI.Views;

namespace TemoraColetaETT.UI.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [RelayCommand]
        async Task NavigateToStudentRegistration()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPersonView)}?registrationType=aluno");
        }

        [RelayCommand]
        async Task NavigateToDriverRegistration()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPersonView)}?registrationType=condutor");
        }
    }
}
