using System;
using FaceRecognitionSample1.Models;
using FaceRecognitionSample1.Services;

namespace FaceRecognitionSample1.Infrastructure
{
    public record MetricDefinition(
        string DisplayName,
        Func<ISettingsService, bool> CheckSetting,
        Func<FaceAttributeResults, string?> GetValueText
    );
}