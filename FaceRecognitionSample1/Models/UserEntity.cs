using System;
using System.Collections.Generic;

namespace FaceRecognitionSample1.Models
{
    /// <summary>
    /// Represents the persistent user information.
    /// </summary>
    public class UserEntity
    {
        public string UserId { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The original face image used for registration, stored as a byte array.
        /// Retaining this allows for re-extraction of features when the engine version is upgraded.
        /// </summary>
        public byte[]? OriginalFaceImage { get; set; }

        /// <summary>
        /// A collection of extracted face features across different engine versions.
        /// </summary>
        public List<FaceFeatureEntity> FaceFeatures { get; set; } = new();
    }
}