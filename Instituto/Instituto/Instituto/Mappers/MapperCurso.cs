using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperCurso : IMapper<Curso, string>
    {
        public void Create(Curso entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO Curso VALUES (@sig_un, @descr, @sig_dep)";

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

        public IEnumerable<Curso> Read(string id)
        {
            List<Curso> cursos = new List<Curso>();
            Curso curso = new Curso();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Curso WHERE sig_un = @sig_un";

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
                            string sigDep = rdr["sig_dep"].ToString();
                            curso.Sig_Un = sigUn;
                            curso.Descr = descr;
                            curso.Sig_Dep = sigDep;
                        }
                    }
                }
                cursos.Add(curso);
                ts.Complete();
            }
            return cursos;
        }

        public void Update(Curso entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Curso SET sig_un = @sig_un, descr = @descr, sig_dep = @sig_dep WHERE sig_un = @sig_un";

                SqlParameter sigUnParam = new SqlParameter();
                SqlParameter descrParam = new SqlParameter();
                SqlParameter sigDepParam = new SqlParameter();

                cmd.Parameters.Add(sigUnParam);
                cmd.Parameters.Add(descrParam);
                cmd.Parameters.Add(sigDepParam);

                sigUnParam.ParameterName = "@sig_un";
                sigUnParam.SqlDbType = SqlDbType.VarChar;

                descrParam.ParameterName = "@descr";
                descrParam.SqlDbType = SqlDbType.Text;

                sigDepParam.ParameterName = "@sig_dep";
                sigDepParam.SqlDbType = SqlDbType.VarChar;


                sigUnParam.Value = entity.Sig_Un;
                descrParam.Value = entity.Descr;
                sigDepParam.Value = entity.Sig_Dep;

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(Curso entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Curso WHERE sig_un = @sig_un";

                SqlParameter sigUnParam = new SqlParameter();

                cmd.Parameters.Add(sigUnParam);

                sigUnParam.ParameterName = "@sig_un";
                sigUnParam.SqlDbType = SqlDbType.Int;

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