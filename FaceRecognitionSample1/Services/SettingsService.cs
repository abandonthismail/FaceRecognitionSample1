using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionSample1.Services
{
    public class SettingsService : ISettingsService
    {
        public bool ShowMask { get; set; } = true;
        public bool ShowSmile { get; set; } = true;
        public bool ShowHeadPose { get; set; } = true;

        public void Load(string? filePath)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
