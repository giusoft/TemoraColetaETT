using System.Diagnostics;
using TemoraColetaETT.Application.Interfaces;

namespace TemoraColetaETT.Infrastructure.Services
{
    public class FacialRecognitionService : IFacialRecognitionService
    {
        private const string ExecutableName = "e2.exe";
        private const string ModelsDir = "models";
        private readonly string _appDataDir = FileSystem.AppDataDirectory;

        public async Task<string> StartCaptureAsync()
        {
            await DeployAssetsAsync();

            var executablePath = Path.Combine(_appDataDir, "e2", ExecutableName);
            if (!File.Exists(executablePath))
            {
                throw new FileNotFoundException("O executável de captura facial não foi encontrado", executablePath);
            }

            var startInfo = new ProcessStartInfo(executablePath)
            {
                WorkingDirectory = Path.GetDirectoryName(executablePath),
                UseShellExecute = false,
            };

            using (var process = Process.Start(startInfo))
            {
                if (process == null)
                {
                    throw new InvalidOperationException("Não foi possivel iniciar o processo de captura.");
                }

                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"O processo de captura facial terminou com erro (Código: {process.ExitCode}).");
                }
            }
            return "Captura concluída com sucesso";
        }

        private async Task DeployAssetsAsync()
        {
            var e2Dir = Path.Combine(_appDataDir, "e2");
            if (!Directory.Exists(e2Dir))
            {
                Directory.CreateDirectory(e2Dir);
            }

            await DeployAssetAsync($"e2/{ExecutableName}", Path.Combine(e2Dir, ExecutableName));

            var modelsPath = Path.Combine(e2Dir, ModelsDir);
            if (!Directory.Exists(modelsPath))
            {
                Directory.CreateDirectory(modelsPath); 
            }

            // modulos de auxiliares para execução do e2.exe
            var modelFiles = new[] { "deploy.prototxt", "drawLandmarks.hpp", "face_detection_yunet_2022mar.onnx", "face_recognition_sface_2021dec.onnx", "glasses.xml", "haarcascade_eye.xml", "haarcascade_eye_tree_eyeglasses.xml", "haarcascade_frontalface_alt2.xml", "haarcascade_frontalface_default.xml", "haarcascade_lefteye_2splits.xml", "haarcascade_license_plate_rus_16stages.xml", "haarcascade_mcs_mouth.xml", "haarcascade_righteye_2splits.xml", "lbfmodel.yaml", "mmod_human_face_detector.dat", "opencv_face_detector.pbtxt", "opencv_face_detector_uint8.pb", "phone1.xml", "phone2.xml", "res10_300x300_ssd_iter_140000_fp16.caffemodel", "shape_predictor_68_face_landmarks.dat" };
            
            foreach(var modelFile in modelFiles)
            {
                await DeployAssetAsync($"e2/{ModelsDir}/{modelFile}", Path.Combine(modelsPath, modelFile));
            }
        }

        private async Task DeployAssetAsync(string assetPath, string destinationPath)
        {
            if (File.Exists(destinationPath))
            {
                return;
            }
            using var stream = await FileSystem.OpenAppPackageFileAsync(assetPath);
            using var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write);
            await stream.CopyToAsync(fileStream);
        }
    }
}
