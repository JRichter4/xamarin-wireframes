using Wireframes.Helpers;
using Wireframes.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Wireframes {
    public partial class App : Application {
        private static SQLiteWireframeDB wireframeDB;

        public App() {
            InitializeComponent();
            MainPage = new NavigationPage(new WireframesMainPage());
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }

        public static SQLiteWireframeDB WireframeDatabase {
            get {
                if (wireframeDB == null) {
                    wireframeDB = new SQLiteWireframeDB();
                }

                return wireframeDB;
            }
        }
    }
}
