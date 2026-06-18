namespace FaceRecognitionSample1.Models
{
    /// <summary>
    /// Represents the volatile session data for a user in the currently active recognition engine.
    /// This data is mapped in-memory upon engine initialization and should NEVER be persisted.
    /// </summary>
    public class ActiveFaceSession
    {
        /// <summary>
        /// The unique application user ID.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// The ID used for 1:1 matching in the current engine.
        /// </summary>
        public long OneToOneEngineId { get; set; }

        /// <summary>
        /// The ID used for 1:N matching in the current engine.
        /// Represented as a 64-bit unsigned integer.
        /// </summary>
        public ulong OneToManyEngineId { get; set; }
    }
}
