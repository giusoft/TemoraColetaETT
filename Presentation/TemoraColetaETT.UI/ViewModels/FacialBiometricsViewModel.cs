using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TemoraColetaETT.Application.Interfaces;

namespace TemoraColetaETT.UI.ViewModels
{
    public partial class FacialBiometricsViewModel : ObservableObject
    {
        private readonly IFacialRecognitionService _facialRegcognitionService;

        public FacialBiometricsViewModel(IFacialRecognitionService facialRegcognitionService)
        {
            _facialRegcognitionService = facialRegcognitionService;
        }

        [RelayCommand]
        private async Task StartFacialCaptureAsync()
        {
            try
            {
                var result = await _facialRegcognitionService.StartCaptureAsync();
                await Shell.Current.DisplayAlert("Sucesso", $"Captura finalizada. Resultado: {result}", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", $"Falha na captura {ex.Message}", "OK");
            }
            finally
            {
                // Implementar finalização
            }
        }

        [RelayCommand]
        private async Task GoNextAsync()
        {
            await Shell.Current.DisplayAlert("Navegação", "Próxima tela sendo chamada.", "OK");
        }
    }
}
