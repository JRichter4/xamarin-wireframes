using SQLite;

namespace Wireframes.Models {
    public class Tag {
        [PrimaryKey, AutoIncrement]
        public int TagId { get; set; }
        public string Name { get; set; }
    }
}
