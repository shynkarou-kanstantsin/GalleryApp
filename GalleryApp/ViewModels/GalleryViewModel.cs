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
        private ObservableCollection<PhotoModel> favouritePhotos;

        public GalleryViewModel()
        {
            FeedPhotos = new ObservableCollection<PhotoModel>();
            FavouritePhotos = new ObservableCollection<PhotoModel>();
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
        }
    }
}
