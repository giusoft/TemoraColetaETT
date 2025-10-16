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
            // 1. **Lógica de Salvar** (Seria implementada após a View fornecer a imagem)
            // Lembre-se: Capturar a imagem do AbsoluteLayout é complexo e deve ser feito no Code-Behind/Serviço.

            // 2. Navegação para a próxima etapa (Substitua a rota pelo seu destino final)
            // Exemplo: await Shell.Current.GoToAsync("//FinalStepView");
            await Shell.Current.GoToAsync(".."); // Exemplo: volta para a tela anterior
        }
    }
}