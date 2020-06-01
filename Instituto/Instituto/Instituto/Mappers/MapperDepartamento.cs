using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperDepartamento : IMapper<Departamento, string>
    {
        public void Create(Departamento entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO Departamento VALUES (@sig_un, @descr)";

                SqlParameter sigUn = new SqlParameter();
                SqlParameter descr = new SqlParameter();
                    
                cmd.Parameters.Add(sigUn);
                cmd.Parameters.Add(descr);

                sigUn.ParameterName = "@sig_un";
                sigUn.SqlDbType = SqlDbType.VarChar;
                descr.ParameterName = "@descr";
                descr.SqlDbType = SqlDbType.Text;

                sigUn.Value = entity.Sig_Un;
                descr.Value = entity.Descr;

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<Departamento> Read(string id)
        {
            List<Departamento> departamentos = new List<Departamento>();
            Departamento departamento = new Departamento();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Departamento WHERE sig_un = @sig_un";

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
                            string descr = rdr["descr"].ToString();
                            departamento.Sig_Un = sigUn;
                            departamento.Descr = descr;
                        }
                    }
                }
                departamentos.Add(departamento);
                ts.Complete();
            }
            return departamentos;
        }

        public void Update(Departamento entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Departamento SET sig_un = @sig_un, descr = @descr WHERE sig_un = @sig_un";

                SqlParameter sigUnParam = new SqlParameter();
                SqlParameter descrParam = new SqlParameter();

                cmd.Parameters.Add(sigUnParam);
                cmd.Parameters.Add(descrParam);

                sigUnParam.ParameterName = "@sig_un";
                sigUnParam.SqlDbType = SqlDbType.VarChar;

                descrParam.ParameterName = "@descr";
                descrParam.SqlDbType = SqlDbType.Text;
                
                sigUnParam.Value = entity.Sig_Un;
                descrParam.Value = entity.Descr;

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(Departamento entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Departamento WHERE sig_un = @sig_un";

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