using Wireframes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wireframes.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WireframeDetailPage : ContentPage {
        public WireframeDetailPage(WireframeDetailViewModel viewModel) {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}