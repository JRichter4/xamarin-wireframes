using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Wireframes.ViewModels {
    public class BaseViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T field, T value, [CallerMemberName] string name = null) {
            if (EqualityComparer<T>.Default.Equals(field, value)) { return; }

            field = value;
            OnPropertyChanged(name);
        }
    }
}
