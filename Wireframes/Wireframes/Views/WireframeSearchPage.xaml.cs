using Wireframes.Helpers;
using Wireframes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wireframes.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WireframeSearchPage : ContentPage {
        public WireframeListViewModel ViewModel {
            get { return BindingContext as WireframeListViewModel; }
            set { BindingContext = value; }
        }

        public WireframeSearchPage() {
            var pageService = new PageService();
            var wireframeStore = new SQLiteWireframeStore(App.WireframeDatabase);
            ViewModel = new WireframeListViewModel(wireframeStore, pageService);

            InitializeComponent();
        }

        protected override void OnAppearing() {
            ViewModel.LoadDataCommand.Execute(null);
            ViewModel.SearchWireframesCommand.Execute(null);
            base.OnAppearing();
        }
    }
}