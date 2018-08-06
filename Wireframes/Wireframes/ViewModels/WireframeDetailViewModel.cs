using Wireframes.Models;
using Wireframes.Interfaces;
using System.Windows.Input;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;

namespace Wireframes.ViewModels {
    public class WireframeDetailViewModel : BaseViewModel {
        public Wireframe Wireframe { get; private set; }
        public readonly IWireframeStore wireframeStore;
        public readonly IPageService pageService;

        // Event Handlers
        public event EventHandler<Wireframe> WireframeAdded;
        public event EventHandler<Wireframe> WireframeUpdated;

        // Commands
        public ICommand SaveWireframeCommand { get; private set; }
        public ICommand CapturePictureCommand { get; private set; }
        public ICommand SelectPictureCommand { get; private set; }

        public WireframeDetailViewModel(WireframeViewModel viewModel, IWireframeStore store, IPageService service) {
            if (viewModel == null) { throw new ArgumentNullException(nameof(viewModel)); }

            wireframeStore = store;
            pageService = service;

            SaveWireframeCommand = new Command(async () => await SaveWireframe());
            CapturePictureCommand = new Command(async () => await CapturePicture(viewModel));
            SelectPictureCommand = new Command(async () => await SelectPicture());

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

        private string tagName;
        public string TagName {
            get { return tagName; }
            set { tagName = value; }
        }

        #region Commands
        private async Task SaveWireframe() {
            if (string.IsNullOrWhiteSpace(Wireframe.Title)) {
                await pageService.DisplayAlert("Error", "Wireframe Title Cannot be Blank", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(Wireframe.FileLocation)) {
                await pageService.DisplayAlert("Error", "You Must Select a Picture", "OK");
                return;
            }

            if (Wireframe.WireframeId == 0) {
                await wireframeStore.AddWireframe(Wireframe);
                WireframeAdded?.Invoke(this, Wireframe);
            } else {
                await wireframeStore.UpdateWireframe(Wireframe);
                WireframeUpdated?.Invoke(this, Wireframe);
            }

            OnPropertyChanged(nameof(Wireframes));

            await pageService.PopAsync();
        }

        private async Task CapturePicture(WireframeViewModel wireframe) {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported) {
                DateTime currentDateTime = DateTime.Now;

                StoreCameraMediaOptions options = new StoreCameraMediaOptions {
                    Directory = "Captures",
                    Name = $"{currentDateTime.ToString("yyyyMMddHHmmssff")}.jpg",
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 50
                };

                var capturedImage = await CrossMedia.Current.TakePhotoAsync(options);
                if (capturedImage == null) return;

                Wireframe.FileName = options.Name;
                Wireframe.FileLocation = capturedImage.Path;
                Wireframe.FileDate = currentDateTime;

                OnPropertyChanged(nameof(Wireframe));
            } else {
                await App.Current.MainPage.DisplayAlert(
                    "Cannot Take Photo", "This device does not support taking a photo", "OK");
                return;
            }
        }

        private async Task SelectPicture() {
            await CrossMedia.Current.Initialize();

            if (CrossMedia.Current.IsPickPhotoSupported) {
                DateTime currentDateTime = DateTime.Now;

                PickMediaOptions options = new PickMediaOptions {
                    PhotoSize = PhotoSize.Medium,
                    CompressionQuality = 50
                };

                var retrievedImage = await CrossMedia.Current.PickPhotoAsync();
                if (retrievedImage == null) return;

                Wireframe.FileName = Path.GetFileName(retrievedImage.Path);
                Wireframe.FileLocation = retrievedImage.Path;
                Wireframe.FileDate = currentDateTime;

                OnPropertyChanged(nameof(Wireframe));
            } else {
                await App.Current.MainPage.DisplayAlert(
                    "Cannot Select Photo", "This device does not support selecting a photo", "OK");
                return;
            }
        }
        #endregion
    }
}
