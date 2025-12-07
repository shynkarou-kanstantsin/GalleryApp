using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalleryApp.Models;
using GalleryApp.Services;

namespace GalleryApp.ViewModels
{
    internal partial class GalleryViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<PhotoModel> feedPhotos;

        [ObservableProperty]
        private ObservableCollection<PhotoModel> favouritePhotos;

        [ObservableProperty]
        private bool isLoading;

        private int currentPage = 1;
        private const int PageSize = 30;

        private readonly UnsplashService _unsplashService;

        private const string FavouriteKey = "Favourites";

        public GalleryViewModel()
        {
            FeedPhotos = new ObservableCollection<PhotoModel>();
            FavouritePhotos = new ObservableCollection<PhotoModel>();
            _unsplashService = new UnsplashService("nIl5_5WLigTHarsoEk1IcxOI_5LuYiKeQeyqEm6cV1g");
            Task.Run(async () => await LoadNextPageAsync());
        }
        
        [RelayCommand]
        public void ChangeLikeability(PhotoModel photo)
        {
            if (photo.IsFavourite)
            {
                FavouritePhotos.Remove(photo);
                photo.IsFavourite = false;
            }
            else
            {
                FavouritePhotos.Add(photo);
                photo.IsFavourite = true;
            }

            SaveFavouriteIds();
        }

        private void SaveFavouriteIds()
        {
            var ids = FavouritePhotos.Select(p => p.Id).ToList();
            var json = JsonSerializer.Serialize(ids);
            Preferences.Set(FavouriteKey, json);
        }

        private List<string> LoadFavouriteIds()
        {
            var json = Preferences.Get(FavouriteKey, "");
            return string.IsNullOrEmpty(json) ? new List<string>() : JsonSerializer.Deserialize<List<string>>(json);
        }

        [RelayCommand]
        public async Task LoadNextPageAsync()
        {
            if (isLoading) return;
            isLoading = true;

            try
            {
                var photos = await _unsplashService.GetPhotosAsync(currentPage , PageSize);
                var favouriteIds = LoadFavouriteIds();
                
                foreach (var photo in photos)
                {
                    photo.IsFavourite = favouriteIds.Contains(photo.Id);
                    FeedPhotos.Add(photo);

                    if (photo.IsFavourite)
                    {
                        FavouritePhotos.Add(photo);
                    }
                }

                currentPage++;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Error", "Failed to load new photos.", "OK");
            }
            finally
            {
                isLoading = false;
            }
        }
    }
}
