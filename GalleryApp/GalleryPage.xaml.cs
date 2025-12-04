using GalleryApp.ViewModels;

namespace GalleryApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.BindingContext = new GalleryViewModel();
        }
    }
}
