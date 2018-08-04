using Wireframes.Models;

namespace Wireframes.ViewModels {
    public class TagViewModel : BaseViewModel {
        public TagViewModel() { }

        public TagViewModel(Tag tag) {
            TagId = tag.TagId;
            name = tag.Name;
            WireframeId = tag.WireframeId;
        }
                
        public int TagId { get; set; }

        private string name;
        public string Name {
            get { return name; }
            set {
                SetValue(ref name, value);
                OnPropertyChanged(nameof(Name));
            }
        }

        public int WireframeId { get; set; }
    }
}
