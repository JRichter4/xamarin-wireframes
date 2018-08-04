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
    }
}
