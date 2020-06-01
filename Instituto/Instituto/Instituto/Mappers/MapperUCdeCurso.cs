using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperUCdeCurso : IMapper<UCdeCurso, object[]>
    {
        public void Create(UCdeCurso entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO UCdeCurso VALUES (@sig_un, @sig_curs, @ano, @semestre)";

                SqlParameter sigUn = new SqlParameter();
                SqlParameter sigCurs = new SqlParameter();
                SqlParameter ano = new SqlParameter();
                SqlParameter semestre = new SqlParameter();


                cmd.Parameters.Add(sigUn);
                cmd.Parameters.Add(sigCurs);
                cmd.Parameters.Add(ano);
                cmd.Parameters.Add(semestre);

                sigUn.ParameterName = "@sig_un";
                sigUn.SqlDbType = SqlDbType.VarChar;
                sigCurs.ParameterName = "@sig_curs";
                sigCurs.SqlDbType = SqlDbType.VarChar;
                ano.ParameterName = "@ano";
                ano.SqlDbType = SqlDbType.Int;
                semestre.ParameterName = "@semestre";
                semestre.SqlDbType = SqlDbType.Int;

                sigUn.Value = entity.Sig_Un;
                sigCurs.Value = entity.Sig_Curs;
                ano.Value = entity.Ano;
                semestre.Value = entity.Semestre;


                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<UCdeCurso> Read(object[] parameteres)
        {
            List<UCdeCurso> ucs = new List<UCdeCurso>();
            UCdeCurso uc = new UCdeCurso();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM UCdeCurso WHERE sig_un = @sig_un AND sig_curs = @sig_curs";

                SqlParameter sigUnQuery = new SqlParameter();
                SqlParameter sigCursQuery = new SqlParameter();
                
                cmd.Parameters.Add(sigUnQuery);
                cmd.Parameters.Add(sigCursQuery);

                sigUnQuery.ParameterName = "@sig_un";
                sigUnQuery.SqlDbType = SqlDbType.VarChar;
                sigCursQuery.ParameterName = "@sig_curs";
                sigCursQuery.SqlDbType = SqlDbType.VarChar;

                sigUnQuery.Value = parameteres[0];
                sigCursQuery.Value = parameteres[1];

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string sigUn = rdr["sig_un"].ToString();                               // Do somthing with this rows string, for example to put them in to a list
                            string sigCurs = rdr["sig_curs"].ToString();
                            string ano = rdr["ano"].ToString();
                            string semestre = rdr["semestre"].ToString();
                            uc.Ano = Int32.Parse(ano);
                            uc.Sig_Curs = sigCurs;
                            uc.Sig_Un = sigUn;
                            uc.Semestre = Int32.Parse(semestre);
                        }
                    }
                }
                ucs.Add(uc);
                ts.Complete();
            }
            return ucs;
        }

        public void Update(UCdeCurso entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE UCdeCurso SET sig_un = @sig_un , sig_curs = @sig_curs , ano = @ano, semestre = @semestre WHERE sig_un = @sig_un_Input AND sig_curs = @sig_curs_Input";

                SqlParameter sigUnParam = new SqlParameter();
                SqlParameter sigCursParam = new SqlParameter();
                SqlParameter anoParam = new SqlParameter();
                SqlParameter semestreParam = new SqlParameter();
                
                SqlParameter sigUnInput = new SqlParameter();
                SqlParameter sigCursInput = new SqlParameter();

                cmd.Parameters.Add(sigUnParam);
                cmd.Parameters.Add(sigCursParam);
                cmd.Parameters.Add(anoParam);
                cmd.Parameters.Add(semestreParam);
                cmd.Parameters.Add(sigUnInput);
                cmd.Parameters.Add(sigCursInput);

                sigUnParam.ParameterName = "@sig_un";
                sigUnParam.SqlDbType = SqlDbType.VarChar;

                sigCursParam.ParameterName = "@sig_dep";
                sigCursParam.SqlDbType = SqlDbType.VarChar;

                anoParam.ParameterName = "@ano";
                anoParam.SqlDbType = SqlDbType.Int;
                
                semestreParam.ParameterName = "@semestre";
                semestreParam.SqlDbType = SqlDbType.Int;

                sigUnInput.ParameterName = "@sig_un_Input";
                sigUnInput.SqlDbType = SqlDbType.VarChar;

                sigCursInput.ParameterName = "@sig_curs_Input";
                sigCursInput.SqlDbType = SqlDbType.VarChar;

                sigCursInput.Value = entity.Sig_Curs;
                sigUnInput.Value = entity.Sig_Un;
                sigCursParam.Value = entity.Sig_Curs;
                anoParam.Value = entity.Ano;
                sigUnParam.Value = entity.Sig_Un;
                semestreParam.Value = entity.Semestre;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(UCdeCurso entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM UCdeCurso WHERE sig_un = @sig_un AND sig_curs = @sig_curs";

                SqlParameter sigUnParam = new SqlParameter();
                SqlParameter sigCursParam = new SqlParameter();

                cmd.Parameters.Add(sigUnParam);
                cmd.Parameters.Add(sigCursParam);
                

                sigUnParam.ParameterName = "@sig_un";
                sigUnParam.SqlDbType = SqlDbType.VarChar;
                sigCursParam.ParameterName = "@sig_curs";
                sigCursParam.SqlDbType = SqlDbType.VarChar;
                
                sigUnParam.Value = entity.Sig_Un;
                sigCursParam.Value = entity.Sig_Curs;

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