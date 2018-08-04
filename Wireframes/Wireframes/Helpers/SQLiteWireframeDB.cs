using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Wireframes.Interfaces;

namespace Wireframes.Helpers {
    public class SQLiteWireframeDB : ISQLiteDB {
        private readonly SQLiteAsyncConnection connection;

        public SQLiteWireframeDB() {
            connection = new SQLiteAsyncConnection(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "WireframeDatabase.db3"
            ));
        }

        public SQLiteAsyncConnection GetConnection() {
            return connection;
        }
    }
}
