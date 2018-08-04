using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Wireframes.Interfaces;
using Wireframes.Models;

namespace Wireframes.Helpers {
    public class SQLiteTagStore : ITagStore {
        private SQLiteAsyncConnection connection;

        public SQLiteTagStore(ISQLiteDB db) {
            connection = db.GetConnection();
            connection.CreateTableAsync<Tag>();
        }

        public async Task<Tag> GetTagById(int id) {
            return await connection.FindAsync<Tag>(id);
        }

        public async Task<IEnumerable<Tag>> GetAllTags() {
            return await connection.Table<Tag>().ToListAsync();
        }

        public async Task<int> AddTag(Tag tag) {
            return await connection.InsertAsync(tag);
        }

        public async Task<int> UpdateTag(Tag tag) {
            return await connection.UpdateAsync(tag);
        }

        public async Task<int> DeleteTag(Tag tag) {
            return await connection.DeleteAsync(tag);
        }
    }
}
