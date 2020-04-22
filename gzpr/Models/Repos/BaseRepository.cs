using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gzpr.Models.Repos
{
    public class BaseRepository
    {

        private static string DbFile {
            get { return Environment.CurrentDirectory +  "\\gzpr.db"; }
        }

        public static SQLiteConnection Connection()
        {
            return new SQLiteConnection(String.Format("Data Source={0}; Version=3;", DbFile));
        }
    }
}
