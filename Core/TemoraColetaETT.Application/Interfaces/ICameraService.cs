namespace TemoraColetaETT.Application.Interfaces
{
    public interface ICameraService
    {
        // <summary>
        // Inicia a captura da caamera e inovoca o callback novo frame.
        // </summary>
        // <param name="onFrameReady"> Ação a ser executada com os bytes do novo frase. </param>
        Task StartCaptureAsync(Action<byte[]> onFrameReady, Action<string> onStatusChanged);

        /// <summary>
        /// Para a captura da câmera.
        /// </summary>
        void StopCapture();

        // <summary>
        // Captura o frame atual, valida e retorna como um array de bytes.
        // </summary> 
        // <returns> array de bytes da i8magem capturada ou null se a validação falhar </returns>
        Task<byte[]?> CaptureAndValidatePhotoAsync();

        // <summary>
        // Verifica se há webcams disponíveis no sisyema.
        // </summary>
        // <returns> True se pelo menos uma webcam for encontrada, caso contrário false. </returns>
        bool IsWebcamAvailable();
    }
}