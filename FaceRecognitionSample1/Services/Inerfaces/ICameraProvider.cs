using System;
using FaceRecognitionSample1.Models;

namespace FaceRecognitionSample1.Services.Interfaces
{
    public class FrameCapturedEventArgs : EventArgs
    {
        public ImageFrame Frame { get; set; }
    }

    public interface ICameraProvider : IDisposable
    {
        void Start();
        void Stop();
        event EventHandler<FrameCapturedEventArgs> FrameCaptured;
    }
}