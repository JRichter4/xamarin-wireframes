using SQLite;

namespace Wireframes.Interfaces {
    public interface ISQLiteDB {
        SQLiteAsyncConnection GetConnection();
    }
}
