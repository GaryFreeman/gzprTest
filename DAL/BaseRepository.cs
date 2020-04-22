using System;
using System.Data.SQLite;

namespace DAL
{
    public class BaseRepository
    {
        private static string DbFile => Environment.CurrentDirectory +  "\\Website.db";

        public static SQLiteConnection Connection()
        {
            return new SQLiteConnection($"Data Source={DbFile}; Version=3;");
        }
    }
}
