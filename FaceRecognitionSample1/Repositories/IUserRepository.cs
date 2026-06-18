using System.Collections.Generic;
using System.Threading.Tasks;
using FaceRecognitionSample1.Models;

namespace FaceRecognitionSample1.Repositories
{
    /// <summary>
    /// Defines the contract for user data operations.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves all registered users asynchronously.
        /// </summary>
        Task<IEnumerable<UserEntity>> GetAllAsync();

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        Task AddAsync(UserEntity user);

        /// <summary>
        /// Deletes multiple users by their IDs.
        /// </summary>
        Task DeleteAsync(IEnumerable<string> userIds);
    }
}