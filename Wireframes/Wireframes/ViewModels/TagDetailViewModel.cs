using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Wireframes.Interfaces;
using Wireframes.Models;
using Xamarin.Forms;

namespace Wireframes.ViewModels {
    public class TagDetailViewModel {
        public Tag Tag { get; private set; }
        public readonly ITagStore tagStore;
        public readonly IPageService pageService;        

        // Event Handlers
        public event EventHandler<Tag> TagAdded;
        public event EventHandler<Tag> TagUpdated;

        // Commands
        public ICommand SaveTagCommand { get; private set; }

        public TagDetailViewModel(TagViewModel viewModel, ITagStore store, IPageService service) {
            if (viewModel == null) { throw new ArgumentNullException(nameof(viewModel)); }

            tagStore = store;
            pageService = service;

            SaveTagCommand = new Command(async () => await SaveTag());

            Tag = new Tag {
                TagId = viewModel.TagId,
                Name = viewModel.Name,
            };
        }

        private async Task SaveTag() {
            if (string.IsNullOrWhiteSpace(Tag.Name)) {
                await pageService.DisplayAlert("Error", "Tag Name Cannot be Blank", "OK");
                return;
            }

            if (Tag.TagId == 0) { // New Tag
                await tagStore.AddTag(Tag);
                TagAdded?.Invoke(this, Tag);
            } else { // Exisiting Tag
                await tagStore.UpdateTag(Tag);
                TagUpdated?.Invoke(this, Tag);
            }

            await pageService.PopAsync();
        }        
    }
}
