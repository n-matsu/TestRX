using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace WindowsFormsApplication2
{
    class Db
    {
        private DbConnection GetConnection()
        {
            return null;
        }

		public DbCommand CreateCommand()
		{
			return null;
		}

        public IEnumerable<DbDataReader> Select(DbCommand cmd)
        {
            DbConnection conn = this.GetConnection();
            cmd.Connection = conn;
            try
            {
                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                    yield return reader;
            }
            finally
            {
                cmd.Connection = null;
            }
        }

        public IObservable<DbDataReader> SelectAsObservable(DbCommand cmd)
        {
            return this.Select(cmd).ToObservable();
        }
    }
}
