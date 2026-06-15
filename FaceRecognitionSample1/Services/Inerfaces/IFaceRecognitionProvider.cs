using System;
using System.Threading.Tasks;
using FaceRecognitionSample1.Models;

namespace FaceRecognitionSample1.Services.Interfaces
{
    public class RecognitionResult
    {
        public bool IsMatch { get; set; }
        public double Score { get; set; }
        // Add TargetId, TargetName etc. for 1:N recognition later
    }

    public interface IFaceRecognitionProvider : IDisposable
    {
        Task<RecognitionResult> RecognizeAsync(ImageFrame frame);
    }
}