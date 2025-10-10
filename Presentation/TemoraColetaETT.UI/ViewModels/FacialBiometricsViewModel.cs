using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Diagnostics;
using System.IO;
using Microsoft.Maui.Controls;

namespace TemoraColetaETT.UI.ViewModels
{
    public partial class FacialBiometricsViewModel : ObservableObject
    {
        [ObservableProperty]
        ImageSource photoSource;

        [ObservableProperty]
        string statusMessage;
        
        [ObservableProperty]
        string title;

        private const string PhotoFileName = "bio-foto.jpg";
        private readonly string _procFolderPath;
        private readonly string _executablePath;

        public FacialBiometricsViewModel()
        {
            Title = "Biometria Facial";
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            _procFolderPath = Path.Combine(baseDirectory, "proc");
            _executablePath = Path.Combine(baseDirectory, "e2.exe");

            if (!Directory.Exists(_procFolderPath))
            {
                Directory.CreateDirectory(_procFolderPath);
            }
        }

        [RelayCommand]
        private void CapturePhoto()
        {
            StatusMessage = "Iniciando captura...";

            if (!File.Exists(_executablePath))
            {
                StatusMessage = "Erro: Executável 'e2.exe' não encontrado.";
                return;
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = _executablePath,
                WorkingDirectory = Path.GetDirectoryName(_executablePath),
                UseShellExecute = false,
                CreateNoWindow = true, // Ocultar a janela para capturar a saída
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            try
            {
                using (var process = new Process { StartInfo = startInfo })
                {
                    process.Start();
                    
                    // Ler a saída e o erro
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit(); 

                    string photoPath = Path.Combine(_procFolderPath, PhotoFileName);

                    if (File.Exists(photoPath))
                    {
                        PhotoSource = null; 
                        PhotoSource = ImageSource.FromFile(photoPath);
                        StatusMessage = "Foto capturada com sucesso!";
                    }
                    else
                    {
                        StatusMessage = "Captura finalizada, mas o arquivo da foto não foi encontrado.";
                        if (!string.IsNullOrWhiteSpace(output))
                        {
                            StatusMessage += $"\nSaída: {output}";
                        }
                        if (!string.IsNullOrWhiteSpace(error))
                        {
                            StatusMessage += $"\nErro: {error}";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                StatusMessage = $"Falha ao executar o processo de captura: {ex.Message}";
            }
        }
    }
}
