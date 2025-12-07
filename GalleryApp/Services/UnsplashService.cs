using GalleryApp.Models;
using System.Text.Json;

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

        public async Task<List<PhotoModel>> GetPhotosAsync(int page, int count = 30)
        {
            var response = await _httpClient.GetAsync($"photos?page={page}&per_page={count}");

            if (!response.IsSuccessStatusCode)
            {
                var reason = response.ReasonPhrase ?? "Unknown error";
                throw new HttpRequestException($"Unsplash request failed: {(int)response.StatusCode} {reason}");
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var photos = new List<PhotoModel>();
            foreach (var item in doc.RootElement.EnumerateArray())
            {
                photos.Add(new PhotoModel
                {
                    Id = item.GetProperty("id").GetString(),
                    SmallUrl = item.GetProperty("urls").GetProperty("small").GetString(),
                    RegularUrl = item.GetProperty("urls").GetProperty("regular").GetString(),
                    AuthorName = item.GetProperty("user").GetProperty("name").GetString(),
                    AuthorLink = item.GetProperty("user").GetProperty("links").GetProperty("html").GetString(),
                    IsFavourite = false
                });
            }

            return photos;

        }
    }
}
