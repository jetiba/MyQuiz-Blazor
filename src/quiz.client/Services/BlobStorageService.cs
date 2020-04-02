using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using quiz.client.Services;
using quiz.shared;

namespace quiz.client
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public BlobStorageService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<bool> DeleteAllBlobsAsync()
        {
            var result = await _httpClient.DeleteAsync("api/BlobStorage/DeleteBlobs");
            return result.IsSuccessStatusCode ? true : false ;

        }

        public async Task<byte[]> GetBlobAsync(string filename)
        {
            try
            {
                var result = await _httpClient.GetStreamAsync($"api/BlobStorage/GetContentFromBlob?filename={filename}");
                byte[] byteres;
                using (var streamReader = new MemoryStream())
                {
                    result.CopyTo(streamReader);
                    byteres = streamReader.ToArray();
                    return byteres;
                }
            }
            catch (Exception)
            {
                return null;
            }        

        }

        public async Task<bool> PutContentInBlobAsync(StreamContent content, string name)
        {
           var result = await _httpClient.PostAsync($"api/BlobStorage/PutContentInBlob?filename={name}", content);
                
            return result.IsSuccessStatusCode ? true : false ;            
        }
    }
}
