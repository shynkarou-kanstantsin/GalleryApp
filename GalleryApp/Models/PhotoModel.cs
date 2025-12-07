using CommunityToolkit.Mvvm.ComponentModel;

namespace GalleryApp.Models
{
    public partial class PhotoModel : ObservableObject
    {
        public string Id { get; set; }
        public string SmallUrl { get; set; }
        public string RegularUrl { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLink { get; set; }
        public string Description { get; set; }

        [ObservableProperty]
        private bool isFavourite;
    }
}
