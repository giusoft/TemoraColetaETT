using OpenCvSharp;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Maui.Dispatching;
using TemoraColetaETT.UI.Views;
using TemoraColetaETT.Application.Interfaces;

namespace TemoraColetaETT.UI.ViewModels
{
    public partial class RegisterBiometricsFacialViewModel : ObservableObject
    {
        private readonly ICameraService _cameraService;
        private readonly IDispatcher _dispatcher;

        [ObservableProperty]
        private bool isInitializing;

        [ObservableProperty]
        private bool isCameraReady;

        [ObservableProperty]
        private ImageSource cameraImageSource = null!;

        [ObservableProperty]
        private string statusMessage = string.Empty;

        public RegisterBiometricsFacialViewModel(ICameraService cameraService, IDispatcher dispatcher)
        {
            _cameraService = cameraService;
            _dispatcher = dispatcher;
        }
        private async Task NextStep()
        {
            await Shell.Current.GoToAsync(nameof(SignatureView));
        }

        public async Task OnAppearingAsync()
        {
            await InitializeCameraAsync();
        }

        public void Cleanup()
        {
            _cameraService.StopCapture();
            IsCameraReady = false;
        }

        private async Task InitializeCameraAsync()
        {
            IsInitializing = true;
            IsCameraReady = false;

            try
            {
                StatusMessage = "Checking for available webcams...";
                if (!_cameraService.IsWebcamAvailable())
                {
                    StatusMessage = "No webcam available.";
                    return;
                }

                StatusMessage = "Webcam found. Starting capture...";
                // Do not await this call as it's a long-running task
                _cameraService.StartCaptureAsync((frameBytes) =>
                {
                    _dispatcher.Dispatch(() =>
                    {
                        CameraImageSource = ImageSource.FromStream(() => new MemoryStream(frameBytes));
                        if (!IsCameraReady)
                        {
                            IsCameraReady = true;
                            IsInitializing = false; // Hide loading indicator once camera is ready
                        }
                    });
                },
                (status) =>
                {
                     _dispatcher.Dispatch(() =>
                     {
                        StatusMessage = status;
                     });
                });
            }
            catch (Exception ex)
            {
                // TODO: Log error or show an alert to the user
                IsCameraReady = false;
                IsInitializing = false;
            }
        }

        [RelayCommand]
        private async Task CapturePhotoAsync()
        {
            if (!IsCameraReady)
            {
                return;
            }
            byte[] photoBytes = await _cameraService.CaptureAndValidatePhotoAsync();

            if (photoBytes != null)
            {
                Cleanup();
                await NextStep();
            }
        }
    }
}