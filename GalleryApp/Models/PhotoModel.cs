namespace GalleryApp.Models
{
    public class PhotoModel
    {
        public string Id { get; set; }
        public string SmallUrl { get; set; }
        public string RegularUrl { get; set; }
        public string AuthorName { get; set; }
        public string AuthorLink { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
    }
}
