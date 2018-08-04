using Wireframes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wireframes.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagDetailPage : ContentPage {
        public TagDetailPage(TagDetailViewModel viewModel) {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}