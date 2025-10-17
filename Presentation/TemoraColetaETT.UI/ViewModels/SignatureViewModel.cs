using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace TemoraColetaETT.UI.ViewModels
{
    public partial class SignatureViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool needsClear = false;

        [RelayCommand]
        private void ClearSignature()
        {
            NeedsClear = true;
        }

        [RelayCommand]
        private async Task SaveAndAdvance()
        {
            // @TODO: Salvar a assinatura e avançar para a tela seguinte
            await Shell.Current.GoToAsync("..");
        }
    }
}