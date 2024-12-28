using System;
using System.Data.SqlClient;

namespace FontTest
{
    internal class Class1
    {
    }

    class MyCon : IDisposable
    {
        SqlConnection Conn = null;

        public MyCon()
        {
            Conn = new SqlConnection("");
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Conn.Close();
            }
        }

        ~MyCon()
        {
            Dispose(false);
        }
    }
}
