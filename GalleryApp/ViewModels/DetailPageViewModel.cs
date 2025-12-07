using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalleryApp.Models;

namespace GalleryApp.ViewModels
{
    internal partial class DetailPageViewModel: ObservableObject
    {
        [ObservableProperty]
        private PhotoModel photo;

        public DetailPageViewModel(PhotoModel photo, GalleryViewModel gallery)
        {
            Photo = photo;
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
