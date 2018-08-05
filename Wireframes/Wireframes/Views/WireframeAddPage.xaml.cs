using Wireframes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wireframes.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WireframeAddPage : ContentPage {
        public WireframeAddPage(WireframeDetailViewModel viewModel) {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}