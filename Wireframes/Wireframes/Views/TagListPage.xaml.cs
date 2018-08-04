using Wireframes.Helpers;
using Wireframes.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Wireframes.Views {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TagListPage : ContentPage {
        public TagListViewModel ViewModel {
            get { return BindingContext as TagListViewModel; }
            set { BindingContext = value; }
        }

        public TagListPage() {
            var pageService = new PageService();
            var tagStore = new SQLiteTagStore(App.WireframeDatabase);
            ViewModel = new TagListViewModel(tagStore, pageService);

            InitializeComponent();
        }

        protected override void OnAppearing() {
            ViewModel.LoadDataCommand.Execute(null);
            base.OnAppearing();
        }

        void OnTagSelected(object sender, SelectedItemChangedEventArgs e) {
            ViewModel.SelectTagCommand.Execute(e.SelectedItem);
        }
    }
}