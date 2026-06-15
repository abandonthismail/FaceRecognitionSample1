using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionSample1.Services
{
    public interface ISettingsService
    {
        bool ShowMask { get; set; }
        bool ShowSmile { get; set; }
        bool ShowHeadPose { get; set; }
        void Save();
        void Load(string? filePath);
    }
}
