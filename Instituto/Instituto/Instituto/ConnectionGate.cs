using System;
using System.Data;
using System.Data.SqlClient;

namespace Instituto
{
    public class ConnectionGate
    {
        
        private static SqlCommand command = new SqlCommand();
        
        public static SqlCommand SetConnection()
        {
            SqlConnection conn = new SqlConnection("data source=DESKTOP-DCAMG6R\\MSSQLSERVER01;initial catalog=Instituto;trusted_connection=true");
            command.Connection = conn;
            return command;
        }

        public static bool TestConnection()
        {
            try {
                command.Connection.Open();
                command.Connection.Close();
            } catch {
                return false;
            }
            return true;
        }

        public static bool OpenConnection()
        {
            try
            {
                command.Connection.Open();
                return true;
            }
            catch
            {
                Console.WriteLine("Unable to open connection");
                return false;
            }
        }

        public static bool CloseConnection()
        {
            try
            {
                command.Connection.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("Unable to close connection");
                return false;
            }
        }

        public static bool BeginTransaction(IsolationLevel isolationLevel)
        {
            try
            {
                command.Transaction = command.Connection.BeginTransaction(isolationLevel);
                return true;
            }
            catch
            {
                Console.WriteLine("Unable to begin transaction");
                return false;
            }
        }

        public static bool Commit()
        {
            try
            {
                command.Transaction.Commit();
                return true;
            }
            catch
            {
                Console.WriteLine("Unable to commit...");
                return false;
            }
        }

        public static bool WorstCaseScenario()
        {
            try
            {
                command.Transaction.Rollback();
                return true;
            }
            catch
            {
                Console.WriteLine("Oh crap, you are screwed, couldn't rollback...");
                return false;
            }
            
            }

        public static int ExecuteStoredProcedure(string commandText, SqlParameter[] parameters)
        {
            int ret = 0;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = commandText;
            if (!(parameters is null))
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }

                try
                {
                    OpenConnection();
                    BeginTransaction(IsolationLevel.ReadUncommitted);
                    ret = command.ExecuteNonQuery();
                    Commit();
                    CloseConnection();
                    command.Parameters.Clear();
                }
                catch
                {
                    WorstCaseScenario();
                    CloseConnection();
                    Console.WriteLine("Something went very wrong...");
                    command.Parameters.Clear();
                    return -1;
                }
            }
            return ret;
        }
        

    }
}