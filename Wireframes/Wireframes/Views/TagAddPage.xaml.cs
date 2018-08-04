using Wireframes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wireframes.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagAddPage : ContentPage {
        public TagAddPage(TagDetailViewModel viewModel) {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}