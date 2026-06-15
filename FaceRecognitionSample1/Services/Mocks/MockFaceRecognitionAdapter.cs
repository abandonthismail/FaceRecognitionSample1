using FaceRecognitionSample1.Models;
using FaceRecognitionSample1.Services.Interfaces;

namespace FaceRecognitionSample1
{
    internal class MockFaceRecognitionAdapter : IFaceRecognitionProvider
    {
        public void Dispose()
        {
        }

        public Task<RecognitionResult> RecognizeAsync(ImageFrame frame)
        {
            return Task.Run(() =>
            {
                return new RecognitionResult();
            });
        }
    }
}