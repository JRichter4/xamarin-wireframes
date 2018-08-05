using Wireframes.Models;
using Wireframes.Interfaces;
using System.Windows.Input;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Wireframes.ViewModels {
    public class WireframeDetailViewModel {
        public Wireframe Wireframe { get; private set; }
        public readonly IWireframeStore wireframeStore;
        public readonly IPageService pageService;

        // Event Handlers
        public event EventHandler<Wireframe> WireframeAdded;
        public event EventHandler<Wireframe> WireframeUpdated;

        // Commands
        public ICommand SaveWireframeCommand { get; private set; }

        public WireframeDetailViewModel(WireframeViewModel viewModel, IWireframeStore store, IPageService service) {
            if (viewModel == null) { throw new ArgumentNullException(nameof(viewModel)); }

            wireframeStore = store;
            pageService = service;

            SaveWireframeCommand = new Command(async () => await SaveWireframe());

            Wireframe = new Wireframe {
                WireframeId = viewModel.WireframeId,
                Title = viewModel.Title,
                Description = viewModel.Description,
                Date = viewModel.Date,
                FileName = viewModel.FileName,
                FileLocation = viewModel.FileLocation,
                FileDate = viewModel.FileDate
            };
        }

        private async Task SaveWireframe() {
            if (string.IsNullOrWhiteSpace(Wireframe.Title)) {
                await pageService.DisplayAlert("Error", "Wireframe Title Cannot be Blank", "OK");
                return;
            }

            if (Wireframe.WireframeId == 0) {
                await wireframeStore.AddWireframe(Wireframe);
                WireframeAdded?.Invoke(this, Wireframe);
            } else {
                await wireframeStore.UpdateWireframe(Wireframe);
                WireframeUpdated?.Invoke(this, Wireframe);
            }

            await pageService.PopAsync();
        }
    }
}
