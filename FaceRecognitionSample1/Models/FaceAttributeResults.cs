using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionSample1.Models
{
    public record FaceAttributeResults(
        double? MaskScore,
        double? SmileScore,
        double? Pitch, double? Yaw, double? Roll
    );
}

