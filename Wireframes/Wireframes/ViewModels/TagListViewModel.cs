using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Wireframes.Interfaces;
using Wireframes.Models;
using Wireframes.Views;
using Xamarin.Forms;

namespace Wireframes.ViewModels {
    public class TagListViewModel : BaseViewModel {
        private bool isDataLoaded;
        private ITagStore tagStore;
        private IPageService pageService;

        public ICommand LoadDataCommand { get; private set; }
        public ICommand AddTagCommand { get; private set; }
        public ICommand SelectTagCommand { get; private set; }
        public ICommand DeleteTagCommand { get; private set; }

        public ObservableCollection<TagViewModel> Tags { get; private set; } = new ObservableCollection<TagViewModel>();

        private TagViewModel selectedTag;
        public TagViewModel SelectedTag {
            get { return selectedTag; }
            set { SetValue(ref selectedTag, value); }
        }

        public TagListViewModel(ITagStore store, IPageService service) {
            tagStore = store;
            pageService = service;

            // Commands
            LoadDataCommand = new Command(async () => await LoadData());
            AddTagCommand = new Command(async () => await AddTag());
            SelectTagCommand = new Command<TagViewModel>(async tag => await SelectTag(tag));
            DeleteTagCommand = new Command<TagViewModel>(async tag => await DeleteTag(tag));
        }

        private async Task LoadData() {
            if (isDataLoaded) { return; }
            isDataLoaded = true;

            var tags = await tagStore.GetAllTags();
            foreach (var tag in tags) {
                Tags.Add(new TagViewModel(tag));
            }
        }

        private async Task AddTag() {
            var viewModel = new TagDetailViewModel(new TagViewModel(), tagStore, pageService);

            viewModel.TagAdded += (source, tag) => {
                Tags.Add(new TagViewModel(tag));
            };

            await pageService.PushAsync(new TagDetailPage(viewModel));
        }

        private async Task SelectTag(TagViewModel tag) {
            if (tag == null) { return; }

            SelectedTag = null;

            var tagViewModel = new TagDetailViewModel(tag, tagStore, pageService);
            tagViewModel.TagUpdated += (source, updatedTag) => {
                tag.TagId = updatedTag.TagId;
                tag.Name = updatedTag.Name;
                tag.WireframeId = updatedTag.WireframeId;
            };

            await pageService.PushAsync(new TagDetailPage(tagViewModel));
        }

        private async Task DeleteTag(TagViewModel viewModel) {
            var userConfirm = await pageService.DisplayAlert(
                "Confirm", $"Are you sure you want to delete the {viewModel.Name} Tag?", "Yes", "No");

            if (userConfirm) {
                Tags.Remove(viewModel);
                var tag = await tagStore.GetTagById(viewModel.TagId);
                await tagStore.DeleteTag(tag);
            }
        }
    }
}
