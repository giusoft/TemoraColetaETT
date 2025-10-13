namespace TemoraColetaETT.Application.Interfaces
{
    public interface IFacialRecognitionService
    {
        Task<string> StartCaptureAsync();
    }
}