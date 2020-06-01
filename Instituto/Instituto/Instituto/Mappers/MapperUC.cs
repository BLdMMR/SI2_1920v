using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperUC : IMapper<UC, int>
    {
        public void Create(UC entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO UC VALUES (@sig_un, @num_cred, @descr)";

                SqlParameter sigUn = new SqlParameter();
                SqlParameter numCred = new SqlParameter();
                SqlParameter descr = new SqlParameter();
                cmd.Parameters.Add(sigUn);
                cmd.Parameters.Add(numCred);
                cmd.Parameters.Add(descr);

                sigUn.ParameterName = "@sig_un";
                sigUn.SqlDbType = SqlDbType.VarChar;
                numCred.ParameterName = "@num_cred";
                numCred.SqlDbType = SqlDbType.Int;
                descr.ParameterName = "@descr";
                descr.SqlDbType = SqlDbType.Text;
                
                sigUn.Value = entity.Sig_Un;
                numCred.Value = entity.Num_Cred;
                descr.Value = entity.Descr;

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<UC> Read(int id)
        {
            List<UC> ucs = new List<UC>();
            UC uc = new UC();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM UC WHERE sig_un = @sig_un";

                SqlParameter sigUnParam = new SqlParameter();
                cmd.Parameters.Add(sigUnParam);
                sigUnParam.ParameterName = "@sig_un";
                sigUnParam.SqlDbType = SqlDbType.VarChar;
                sigUnParam.Value = id;

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string sigUn = rdr["sig_un"].ToString();                               // Do somthing with this rows string, for example to put them in to a list
                            string numCred = rdr["num_cred"].ToString();
                            string descr = rdr["descr"].ToString();
                            uc.Num_Cred = Int32.Parse(numCred);
                            uc.Sig_Un = sigUn;
                            uc.Descr = descr;
                        }
                    }
                }
                ucs.Add(uc);
                ts.Complete();
            }
            return ucs;
        }

        public void Update(UC entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE UC SET sig_un = @sig_un, num_cred = @num_cred, descr = @descr WHERE sig_un = @sig_un_input";

                SqlParameter sigUnParam = new SqlParameter();
                SqlParameter numCredParam = new SqlParameter();
                SqlParameter descrParam = new SqlParameter();
                SqlParameter sigUnInput = new SqlParameter();

                cmd.Parameters.Add(sigUnParam);
                cmd.Parameters.Add(numCredParam);
                cmd.Parameters.Add(descrParam);
                cmd.Parameters.Add(sigUnInput);

                sigUnParam.ParameterName = "@sig_un";
                sigUnParam.SqlDbType = SqlDbType.VarChar;

                numCredParam.ParameterName = "@num_cred";
                numCredParam.SqlDbType = SqlDbType.Int;

                descrParam.ParameterName = "@descr";
                descrParam.SqlDbType = SqlDbType.Text;

                sigUnInput.ParameterName = "@sig_un_input";
                sigUnInput.SqlDbType = SqlDbType.VarChar;

                sigUnInput.Value = entity.Sig_Un;
                sigUnParam.Value = entity.Sig_Un;
                numCredParam.Value = entity.Num_Cred;
                descrParam.Value = entity.Descr;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(UC entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM UC WHERE sig_un = @sig_un";

                SqlParameter sigUnParam = new SqlParameter();

                cmd.Parameters.Add(sigUnParam);

                sigUnParam.ParameterName = "@sig_un";
                sigUnParam.SqlDbType = SqlDbType.VarChar;

                sigUnParam.Value = entity.Sig_Un;

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }
    }
}