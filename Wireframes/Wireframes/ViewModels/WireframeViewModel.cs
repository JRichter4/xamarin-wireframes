using System;
using Wireframes.Models;

namespace Wireframes.ViewModels {
    public class WireframeViewModel : BaseViewModel {
        public WireframeViewModel() {
            date = DateTime.Now;
        }

        public WireframeViewModel(Wireframe wireframe) {
            WireframeId = wireframe.WireframeId;
            title = wireframe.Title;
            description = wireframe.Description;
            date = wireframe.Date;
            fileName = wireframe.FileName;
            fileLocation = wireframe.FileLocation;
            fileDate = wireframe.FileDate;
        }

        public int WireframeId { get; set; }

        private string title;
        public string Title {
            get { return title; }
            set {
                SetValue(ref title, value);
                OnPropertyChanged(nameof(Title));
            }
        }

        private string description;
        public string Description {
            get { return description; }
            set {
                SetValue(ref description, value);
                OnPropertyChanged(nameof(Description));
            }
        }

        private DateTime date;
        public DateTime Date {
            get { return date; }
            set {
                SetValue(ref date, value);
                OnPropertyChanged(nameof(Date));
            }
        }

        private string fileName;
        public string FileName {
            get { return fileName; }
            set {
                SetValue(ref fileName, value);
                OnPropertyChanged(nameof(FileName));
            }
        }

        private string fileLocation;
        public string FileLocation {
            get { return fileLocation; }
            set {
                SetValue(ref fileLocation, value);
                OnPropertyChanged(nameof(FileLocation));
            }
        }

        private DateTime fileDate;
        public DateTime FileDate {
            get { return fileDate; }
            set {
                SetValue(ref fileDate, value);
                OnPropertyChanged(nameof(FileDate));
            }
        }
    }
}
