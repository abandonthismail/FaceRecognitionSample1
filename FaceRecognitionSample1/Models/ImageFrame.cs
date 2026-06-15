using System;

namespace FaceRecognitionSample1.Models
{
    public class ImageFrame : IDisposable
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public int Stride { get; set; }
        public byte[] PixelData { get; set; }

        public void Dispose()
        {
            // Future implementation for unmanaged resource cleanup if using IntPtr
        }
    }
}
