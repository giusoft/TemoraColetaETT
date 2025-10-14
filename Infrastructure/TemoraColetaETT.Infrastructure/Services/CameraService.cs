using OpenCvSharp;
using TemoraColetaETT.Application.Interfaces;

namespace TemoraColetaETT.Infrastructure.Services
{
    public class CameraService : ICameraService
    {
        private VideoCapture _capture = null!;
        private CancellationTokenSource _cancellationTokenSource = null!;

        public bool IsWebcamAvailable()
        {
            var tempCapture = new VideoCapture( 0,VideoCaptureAPIs.DSHOW);
            bool isAvailable = tempCapture.IsOpened();
            tempCapture.Release();
            return isAvailable;
        }

        public Task StartCaptureAsync(Action<byte[]> onFrameReady, Action<string> onStatusChanged)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            return Task.Run(async () => 
            {
                onStatusChanged?.Invoke("Attempting to open camera...");
                _capture = new VideoCapture(0,VideoCaptureAPIs.DSHOW);
                if (!_capture.IsOpened())
                {
                    onStatusChanged?.Invoke("Failed to open camera.");
                    return;
                }

                onStatusChanged?.Invoke("Camera opened successfully.");
                _capture.FrameWidth = 640;
                _capture.FrameHeight = 480;

                onStatusChanged?.Invoke("Starting frame capture...");
                while (!token.IsCancellationRequested)
                {
                    using (var frame = new Mat())
                    {
                        _capture.Read(frame);
                        if (frame.Empty()) break;

                        onStatusChanged?.Invoke("Frame captured.");
                        var stream = frame.ToMemoryStream(".jpg");

                        onFrameReady?.Invoke(stream.ToArray());
                    }
                    await Task.Delay(33, token);
                }
                onStatusChanged?.Invoke("Capture loop finished.");
                _capture.Release();
            }, token);
        }
    
        public void StopCapture()
        {
            _cancellationTokenSource?.Cancel();
        }

        public Task<byte[]?> CaptureAndValidatePhotoAsync()
        {
            using (var frame = new Mat())
            {
                _capture.Read(frame);
                if (frame.Empty()) return Task.FromResult((byte[]?)null);

                var stream = frame.ToMemoryStream(".jpg");
                return Task.FromResult((byte[]?)stream.ToArray());
            }
        }
    }
}
