using Wireframes.Views;
using Xamarin.Forms;

namespace Wireframes {
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();
        }

        private async void ViewTagsButton_Clicked(object sender, System.EventArgs e) {
            await Navigation.PushAsync(new TagListPage());
        }

        private async void ViewWireframesButton_Clicked(object sender, System.EventArgs e) {
            await Navigation.PushAsync(new WireframeListPage(false));
        }

        private async void SearchWireframesButton_Clicked(object sender, System.EventArgs e) {
            await Navigation.PushAsync(new WireframeListPage(true));
        }
    }
}
