using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceRecognitionSample1.Models;

namespace FaceRecognitionSample1.Repositories
{
    /// <summary>
    /// In-memory implementation of the user repository.
    /// Data is volatile and will be lost when the application closes.
    /// </summary>
    public class InMemoryUserRepository : IUserRepository
    {
        // Thread-safe list to hold user data in RAM
        private readonly List<UserEntity> _users = new();

        public InMemoryUserRepository()
        {
            // Injecting initial dummy data containing mock feature data for Engine Version 1
            var dummyFeatureV1 = new FaceFeatureEntity
            {
                EngineVersion = 1,
                FeatureData = new byte[] { 0x01, 0x02, 0x0A, 0x0B }, // Mock binary data
                ExtractedAt = DateTime.Now.AddDays(-5)
            };
            string[] lastNames = { "佐藤", "鈴木", "高橋", "田中", "伊藤", "渡辺", "山本", "中村", "小林", "加藤", "吉田", "山田" };
            string[] firstNames = { "花子", "太郎", "健太", "美咲", "陽子", "拓也", "直樹", "由美", "翔太", "愛" };

            Random rnd = new Random();

            for (int i = 1; i <= 15; i++)
            {
                string lastName = lastNames[rnd.Next(lastNames.Length)];
                string firstName = firstNames[rnd.Next(firstNames.Length)];

                _users.Add(new UserEntity
                {
                    UserId = $"U{i:D4}",
                    UserName = $"{lastName} {firstName}", // 苗字と名前の組み合わせ
                    CreatedAt = DateTime.Now.AddDays(-i),
                    FaceFeatures = new List<FaceFeatureEntity> { dummyFeatureV1 }
                });
            }
        }

        public Task<IEnumerable<UserEntity>> GetAllAsync()
        {
            // Simulate network/database delay for realistic UI loading behavior
            // return Task.Delay(500).ContinueWith(_ => _users.AsEnumerable());
            return Task.FromResult(_users.AsEnumerable());
        }

        public Task AddAsync(UserEntity user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(IEnumerable<string> userIds)
        {
            _users.RemoveAll(u => userIds.Contains(u.UserId));
            return Task.CompletedTask;
        }
    }
}