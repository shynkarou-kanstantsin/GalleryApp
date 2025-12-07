using GalleryApp.ViewModels;

namespace GalleryApp
{
    public partial class GalleryPage : ContentPage
    {
        public GalleryPage()
        {
            InitializeComponent();

            this.BindingContext = new GalleryViewModel();
        }
    }
}
