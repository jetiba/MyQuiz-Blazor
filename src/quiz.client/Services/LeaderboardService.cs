using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using quiz.client.Services;
using quiz.shared;

namespace quiz.client
{
    public class LeaderboardService : ILeaderboardService
    {
         private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public LeaderboardService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }
        
        public async Task<bool> AddPoints(Leaderboard leaderboard)
        {
            var result = await _httpClient.PostJsonAsync<HttpResponseMessage>("api/Leaderboard/AddPoints", leaderboard);

            return result.IsSuccessStatusCode ? true : false ;
        }

        public async Task<List<Leaderboard>> GetAllPoints()
        {
             var result = await _httpClient.GetAsync("api/Leaderboard/GetAllPoints");
             var jsonResult = await result.Content.ReadAsStringAsync();
             var listUser = JsonConvert.DeserializeObject<List<Leaderboard>>(jsonResult);
             
             return listUser;
        }

        public async Task<int> GetPointsByUser(string username)
        {
             var result = await _httpClient.GetAsync($"api/Leaderboard/GetPointsByUser?username={username}");
            var jsonResult = await result.Content.ReadAsStringAsync();
             var listUser = JsonConvert.DeserializeObject<Leaderboard>(jsonResult);
             return listUser.Points;
        }
    }
}