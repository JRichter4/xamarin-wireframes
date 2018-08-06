using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Wireframes.Interfaces;
using Wireframes.Views;
using Xamarin.Forms;

namespace Wireframes.ViewModels {
    public class WireframeListViewModel : BaseViewModel {
        private bool isDataLoaded;
        private IPageService pageService;
        private IWireframeStore wireframeStore;

        // Commands
        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddWireframeCommand { get; private set; }
        public ICommand SelectWireframeCommand { get; private set; }
        public ICommand SearchWireframesCommand { get; private set; }
        
        public ObservableCollection<WireframeViewModel> Wireframes {
            get; private set;
        } = new ObservableCollection<WireframeViewModel>();

        private ObservableCollection<WireframeViewModel> allWireframes =
            new ObservableCollection<WireframeViewModel>();

        private string searchBarText;
        public string SearchBarText {
            get { return searchBarText ?? ""; }
            set {
                searchBarText = value;
                SearchWireframesCommand.Execute(null);
            }
        }

        private WireframeViewModel selectedWireframe;
        public WireframeViewModel SelectedWireframe {
            get { return selectedWireframe; }
            set {
                SetValue(ref selectedWireframe, value);
                SelectWireframeCommand.Execute(selectedWireframe);
            }
        }

        public WireframeListViewModel(IWireframeStore store, IPageService service) {
            wireframeStore = store;
            pageService = service;

            LoadDataCommand = new Command(async () => await LoadData());
            AddWireframeCommand = new Command(async () => await AddWireframe());
            SelectWireframeCommand = new Command<WireframeViewModel>(async w => await SelectWireframe(w));
            SearchWireframesCommand = new Command(() => SearchWireframes());
        }

        private async Task LoadData() {
            if (isDataLoaded) { return; }

            var wireframes = await wireframeStore.GetAllWirefames();
            foreach (var wireframe in wireframes) {
                Wireframes.Add(new WireframeViewModel(wireframe));
            }

            isDataLoaded = true;
            allWireframes = Wireframes;
        }

        private async Task AddWireframe() {
            var viewModel = new WireframeDetailViewModel(new WireframeViewModel(), wireframeStore, pageService);
            
            viewModel.WireframeAdded += (source, wireframe) => {
                Wireframes.Add(new WireframeViewModel(wireframe));
            };

            await pageService.PushAsync(new WireframeAddPage(viewModel));
        }

        private async Task SelectWireframe(WireframeViewModel wireframe) {
            if (wireframe == null) { return; }

            SelectedWireframe = null;

            var wireframeViewModel = new WireframeDetailViewModel(wireframe, wireframeStore, pageService);
            wireframeViewModel.WireframeUpdated += (source, updatedWireframe) => {
                wireframe.WireframeId = updatedWireframe.WireframeId;
                wireframe.Title = updatedWireframe.Title;
                wireframe.Description = updatedWireframe.Description;
                wireframe.Date = updatedWireframe.Date;
                wireframe.FileName = updatedWireframe.FileName;
                wireframe.FileLocation = updatedWireframe.FileLocation;
                wireframe.FileDate = updatedWireframe.FileDate;
            };

            await pageService.PushAsync(new WireframeDetailPage(wireframeViewModel));
        }

        private void SearchWireframes() {
            var results = allWireframes.Where(x => x.Title.Contains(searchBarText));
            Wireframes = new ObservableCollection<WireframeViewModel>(results);
            OnPropertyChanged(nameof(Wireframes));            
        }
    }
}
