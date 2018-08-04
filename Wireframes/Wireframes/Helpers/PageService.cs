using System.Threading.Tasks;
using Wireframes.Interfaces;
using Xamarin.Forms;

namespace Wireframes.Helpers {
    public class PageService : IPageService {
        private Page MainPage { get => Application.Current.MainPage; }
        
        // Navigation
        public async Task PushAsync(Page page) {
            await MainPage.Navigation.PushAsync(page);
        }

        public async Task<Page> PopAsync() {
            return await MainPage.Navigation.PopAsync();
        }

        // Alerts
        public async Task DisplayAlert(string title, string message, string ok) {
            await MainPage.DisplayAlert(title, message, ok);
        }

        public async Task<bool> DisplayAlert(string title, string message, string confirm, string cancel) {
            return await MainPage.DisplayAlert(title, message, confirm, cancel);
        }
    }
}
