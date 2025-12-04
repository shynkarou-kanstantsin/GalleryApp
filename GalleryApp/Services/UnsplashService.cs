using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using GalleryApp.Models;

namespace GalleryApp.Services
{
    public class UnsplashService
    {
        private readonly HttpClient _httpClient;

        public UnsplashService(string accessKey)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.unsplash.com/")
            };

            _httpClient.DefaultRequestHeaders.Add("Accept-Version", "v1");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Client-ID {accessKey}");
        }

        public async Task<List<PhotoModel>> GetRandomPhotosAsync(int count = 30)
        {
            var photos = await _httpClient.GetFromJsonAsync<List<PhotoModel>>($"photos/random?count={count}");
            return photos ?? new List<PhotoModel>();
        }
    }
}
