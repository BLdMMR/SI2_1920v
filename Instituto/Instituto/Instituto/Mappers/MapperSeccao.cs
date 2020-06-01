using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperSeccao : IMapper<Seccao, object[]>
    {
        public void Create(Seccao entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO Secção VALUES (@sig_un, @descr, @sig_dep)";

                SqlParameter sigUn = new SqlParameter();
                SqlParameter descr = new SqlParameter();
                SqlParameter sigDep = new SqlParameter();


                cmd.Parameters.Add(sigUn);
                cmd.Parameters.Add(descr);
                cmd.Parameters.Add(sigDep);

                sigUn.ParameterName = "@sig_un";
                sigUn.SqlDbType = SqlDbType.VarChar;
                descr.ParameterName = "@descr";
                descr.SqlDbType = SqlDbType.Text;
                sigDep.ParameterName = "@sig_dep";
                sigDep.SqlDbType = SqlDbType.VarChar;

                sigUn.Value = entity.Sig_Un;
                descr.Value = entity.Descr;
                sigDep.Value = entity.Sig_Dep;


                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<Seccao> Read(object[] parameteres)
        {
            List<Seccao> seccoes = new List<Seccao>();
            Seccao currSeccao = new Seccao();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Secção WHERE sig_un = @sig_un AND sig_dep = @sig_dep";

                SqlParameter sigUnQuery = new SqlParameter();
                SqlParameter sigDepQuery = new SqlParameter();
                
                cmd.Parameters.Add(sigUnQuery);
                cmd.Parameters.Add(sigDepQuery);

                sigUnQuery.ParameterName = "@sig_un";
                sigUnQuery.SqlDbType = SqlDbType.VarChar;
                sigDepQuery.ParameterName = "@sig_dep";
                sigDepQuery.SqlDbType = SqlDbType.VarChar;

                sigUnQuery.Value = parameteres[0];
                sigDepQuery.Value = parameteres[1];

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string sigUn = rdr["sig_un"].ToString();                               // Do somthing with this rows string, for example to put them in to a list
                            string sigDep = rdr["sig_dep"].ToString();
                            string descr = rdr["descr"].ToString();
                            currSeccao.Descr = descr;
                            currSeccao.Sig_Dep = sigDep;
                            currSeccao.Sig_Un = sigUn;
                        }
                    }
                }
                seccoes.Add(currSeccao);
                ts.Complete();
            }
            return seccoes;
        }

        public void Update(Seccao entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Secção SET sig_un = @sig_un , descr = @descr , sig_dep = @sig_dep WHERE sig_un = @sig_un_Input AND sig_dep = @sig_dep_Input";

                SqlParameter sigUnParam = new SqlParameter();
                SqlParameter sigDepParam = new SqlParameter();
                SqlParameter descrParam = new SqlParameter();
                
                SqlParameter sigUnInput = new SqlParameter();
                SqlParameter sigDepInput = new SqlParameter();

                cmd.Parameters.Add(sigUnParam);
                cmd.Parameters.Add(sigDepParam);
                cmd.Parameters.Add(descrParam);
                cmd.Parameters.Add(sigUnInput);
                cmd.Parameters.Add(sigDepInput);

                sigUnParam.ParameterName = "@sig_un";
                sigUnParam.SqlDbType = SqlDbType.VarChar;

                sigDepParam.ParameterName = "@sig_dep";
                sigDepParam.SqlDbType = SqlDbType.VarChar;

                descrParam.ParameterName = "@descr";
                descrParam.SqlDbType = SqlDbType.Text;

                sigUnInput.ParameterName = "@sig_un_Input";
                sigUnInput.SqlDbType = SqlDbType.VarChar;

                sigDepInput.ParameterName = "@sig_dep_Input";
                sigDepInput.SqlDbType = SqlDbType.VarChar;

                sigDepInput.Value = entity.Sig_Dep;
                sigUnInput.Value = entity.Sig_Un;
                sigDepParam.Value = entity.Sig_Dep;
                descrParam.Value = entity.Descr;
                sigUnParam.Value = entity.Sig_Un;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(Seccao entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Secção WHERE sig_un = @sig_un_ AND sig_dep = @sig_dep";

                SqlParameter sigUnParam = new SqlParameter();
                SqlParameter sigDepParam = new SqlParameter();

                cmd.Parameters.Add(sigUnParam);
                cmd.Parameters.Add(sigDepParam);
                

                sigUnParam.ParameterName = "@ano";
                sigUnParam.SqlDbType = SqlDbType.VarChar;
                sigDepParam.ParameterName = "@semestre";
                sigDepParam.SqlDbType = SqlDbType.VarChar;
                
                sigUnParam.Value = entity.Sig_Un;
                sigDepParam.Value = entity.Sig_Dep;

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