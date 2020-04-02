using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using quiz.shared;

namespace quiz.client.Services
{
    public interface IBlobStorageService
    {
        Task<bool> PutContentInBlobAsync(StreamContent content, string name);

        Task<bool> DeleteAllBlobsAsync();

        Task<byte[]> GetBlobAsync(string filename);
    }
}