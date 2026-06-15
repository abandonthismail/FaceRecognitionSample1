using System;
using FaceRecognitionSample1.Models;
using FaceRecognitionSample1.Services.Interfaces;

namespace FaceRecognitionSample1.Services.Mocks
{
    public class MockCameraAdapter : ICameraProvider
    {
        public event EventHandler<FrameCapturedEventArgs> FrameCaptured;

        public void Start()
        {
            // Simulate camera start
        }

        public void Stop()
        {
            // Simulate camera stop
        }

        public void Dispose()
        {
            // Cleanup resources
        }
    }
}
