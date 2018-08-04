using System.Threading.Tasks;
using Xamarin.Forms;

namespace Wireframes.Interfaces {
    public interface IPageService {
        // Navigation
        Task PushAsync(Page page);
        Task<Page> PopAsync();

        // Alerts
        Task DisplayAlert(string title, string message, string ok);
        Task<bool> DisplayAlert(string title, string message, string confirm, string cancel);
    }
}
