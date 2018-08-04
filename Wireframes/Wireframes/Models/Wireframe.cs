using System;
using SQLite;

namespace Wireframes.Models {
    public class Wireframe {
        [PrimaryKey, AutoIncrement]
        public int WireframeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public DateTime FileDate { get; set; }
    }
}
