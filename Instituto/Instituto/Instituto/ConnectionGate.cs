using System;
using System.Data.SqlClient;

namespace Instituto
{
    public class ConnectionGate
    {
        public static SqlConnection CreateConnection()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Server=DESKTOP-M1V057N;Database=Instituto;User=berna;Password=";
            return conn;
        }

        public static bool TestConnection(SqlConnection conn)
        {
            try
            {
                conn.Open();
                conn.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}