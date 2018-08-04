using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Wireframes.Interfaces;
using Wireframes.Models;

namespace Wireframes.Helpers {
    class SQLiteWireframeStore : IWireframeStore {
        private SQLiteAsyncConnection connection;

        public SQLiteWireframeStore(ISQLiteDB db) {
            connection = db.GetConnection();
            connection.CreateTableAsync<Wireframe>();
        }

        public async Task<Wireframe> GetWireframeById(int id) {
            return await connection.FindAsync<Wireframe>(id);
        }

        public async Task<IEnumerable<Wireframe>> GetAllWirefames() {
            return await connection.Table<Wireframe>().ToListAsync();
        }

        public async Task<int> AddWireframe(Wireframe wireframe) {
            return await connection.InsertAsync(wireframe);
        }

        public async Task<int> UpdateWireframe(Wireframe wireframe) {
            return await connection.UpdateAsync(wireframe);
        }

        public async Task<int> DeleteWireframe(Wireframe wireframe) {
            return await connection.DeleteAsync(wireframe);
        }
    }
}
