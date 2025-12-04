using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalleryApp.Models;

namespace GalleryApp.ViewModels
{
    internal partial class GalleryViewModel: ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<PhotoModel> feedPhotos;

        [ObservableProperty]
        private ObservableCollection<PhotoModel> favoritePhotos;

        public GalleryViewModel()
        {
            FeedPhotos = new ObservableCollection<PhotoModel>();
            FavoritePhotos = new ObservableCollection<PhotoModel>();
        }
        
        [RelayCommand]
        public void ChangeLikeability(PhotoModel photo)
        {
            if (photo.IsFavourite)
            {
                FavoritePhotos.Remove(photo);
                photo.IsFavourite = false;
            }
            else
            {
                FavoritePhotos.Add(photo);
                photo.IsFavourite = true;
            }
        }
    }
}
