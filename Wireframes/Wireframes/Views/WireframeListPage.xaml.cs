using Wireframes.Helpers;
using Wireframes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wireframes.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WireframeListPage : ContentPage {
        public WireframeListViewModel ViewModel {
            get { return BindingContext as WireframeListViewModel; }
            set { BindingContext = value; }
        }

        public WireframeListPage(bool searchBar) {
            var pageService = new PageService();
            var wireframeStore = new SQLiteWireframeStore(App.WireframeDatabase);
            ViewModel = new WireframeListViewModel(searchBar, wireframeStore, pageService);
            

            InitializeComponent();
        }

        protected override void OnAppearing() {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }
    }
}