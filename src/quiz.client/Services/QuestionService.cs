using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Timers;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using quiz.client.Services;
using quiz.shared;
using quiz.shared.Services;

namespace quiz.client
{
    public class QuestionService : IQuestionService
    {
         private readonly HttpClient _httpClient;
        //private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;
        private Timer aTimer;
        private static int time;
        private static int questionNumber;
        private bool alreadyStarted = false;

        public QuestionService(HttpClient httpClient,
                           //AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            //_authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
            aTimer = new System.Timers.Timer(1000);
            
            questionNumber = 0;
        }

        public async Task<bool> AddQuestions(List<Question> questionsList)
        {
            try
            {
                await _httpClient.PostJsonAsync("api/Question/AddQuestions", questionsList);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public async Task<List<Question>> GetQuestions()
        {
            var result = await _httpClient.GetAsync("api/Question/GetQuestions");
            var jsonResult = await result.Content.ReadAsStringAsync();
            var listUser = JsonConvert.DeserializeObject<List<Question>>(jsonResult);

            return listUser;
        }

        public void StartTime()
        {
            if (!alreadyStarted)
            {
                aTimer.AutoReset = true;
                aTimer.Enabled = true;
                aTimer.Elapsed += OnTimedEvent;
                aTimer.Start();
                alreadyStarted = true;
            }
        }

        public void GetTimer(ref Timer timer)
        {
            timer = aTimer;
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(time > 0)
            {
                time--;       
            }
            else
            {
                aTimer.Stop();
                await Task.Delay(1000);
                aTimer.Start();
                
                time = 10;
                questionNumber++;
            }
        }

        public int GetTime(){
            return time;
        }

        public int GetQuestionNumber()
        {
            return questionNumber;
        }

        public async Task<bool> DeleteAllQuestions()
        {
            var result = await _httpClient.DeleteAsync("api/Question/DeleteQuestions");

            return result.IsSuccessStatusCode;
        }

        public async Task<string> GetTemplateFile()
        {
            Console.WriteLine("metodo");
            var result = await _httpClient.GetStringAsync("api/Question/GetTemplateFile");
            Console.WriteLine(result);
            // var data = await result.Content.ReadAsStringAsync();

            return result;
        }
    }
}