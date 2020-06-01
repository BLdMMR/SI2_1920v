using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperEnsina : IMapper<Ensina, object[]>
    {
        public void Create(Ensina entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO Ensina VALUES (@sig_uc, @prof_cc, @ano)";

                SqlParameter sigUc = new SqlParameter();
                SqlParameter profCC = new SqlParameter();
                SqlParameter ano = new SqlParameter();
                    
                cmd.Parameters.Add(sigUc);
                cmd.Parameters.Add(profCC);
                cmd.Parameters.Add(ano);

                sigUc.ParameterName = "@sig_uc";
                sigUc.SqlDbType = SqlDbType.VarChar;
                profCC.ParameterName = "@prof_cc";
                profCC.SqlDbType = SqlDbType.VarChar;
                ano.ParameterName = "@ano";
                ano.SqlDbType = SqlDbType.Int;

                sigUc.Value = entity.Sig_UC;
                profCC.Value = entity.Prof_CC;
                ano.Value = entity.Ano;


                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<Ensina> Read(object[] parameteres)
        {
            List<Ensina> ensinas = new List<Ensina>();
            Ensina currEnsina = new Ensina();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Ensina WHERE sig_uc = @sig_uc AND prof_cc = @prof_cc";

                SqlParameter sigUcQuery = new SqlParameter();
                SqlParameter profCCQuery = new SqlParameter();
                    
                cmd.Parameters.Add(sigUcQuery);
                cmd.Parameters.Add(profCCQuery);

                sigUcQuery.ParameterName = "@sig_uc";
                sigUcQuery.SqlDbType = SqlDbType.VarChar;
                profCCQuery.ParameterName = "@prof_cc";
                profCCQuery.SqlDbType = SqlDbType.VarChar;

                sigUcQuery.Value = parameteres[0];
                profCCQuery.Value = parameteres[1];

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string sigUC = rdr["ano"].ToString();                               // Do somthing with this rows string, for example to put them in to a list
                            string profCC = rdr["semestre"].ToString();
                            currEnsina.Sig_UC = sigUC;
                            currEnsina.Prof_CC = profCC;
                        }
                    }
                }
                ensinas.Add(currEnsina);
                ts.Complete();
            }
            return ensinas;
        }

        public void Update(Ensina entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Ensina SET sig_uc = @sig_uc , prof_cc = @prof_cc WHERE sig_uc = @sig_uc_new AND prof_cc = @prof_cc_new";

                SqlParameter sigUcParam = new SqlParameter();
                SqlParameter profCCParam = new SqlParameter();
                
                SqlParameter sig_uc_new = new SqlParameter();
                SqlParameter prof_cc_new = new SqlParameter();

                cmd.Parameters.Add(sigUcParam);
                cmd.Parameters.Add(profCCParam);
                cmd.Parameters.Add(sig_uc_new);
                cmd.Parameters.Add(prof_cc_new);

                sigUcParam.ParameterName = "@sig_uc";
                sigUcParam.SqlDbType = SqlDbType.VarChar;

                profCCParam.ParameterName = "@prof_cc";
                profCCParam.SqlDbType = SqlDbType.VarChar;

                sig_uc_new.ParameterName = "@sig_uc_new";
                sig_uc_new.SqlDbType = SqlDbType.VarChar;

                prof_cc_new.ParameterName = "@prof_cc_new";
                prof_cc_new.SqlDbType = SqlDbType.VarChar;

                

                sig_uc_new.Value = entity.Sig_UC;
                prof_cc_new.Value = entity.Prof_CC;
                sigUcParam.Value = entity.Sig_UC;
                profCCParam.Value = entity.Prof_CC;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(Ensina entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Ensina WHERE sig_uc = @sig_uc AND prof_cc = @prof_cc";

                SqlParameter sigUcParam = new SqlParameter();
                SqlParameter profCCParam = new SqlParameter();

                cmd.Parameters.Add(sigUcParam);
                cmd.Parameters.Add(profCCParam);

                sigUcParam.ParameterName = "@sig_uc";
                sigUcParam.SqlDbType = SqlDbType.VarChar;
                profCCParam.ParameterName = "@prof_cc";
                profCCParam.SqlDbType = SqlDbType.VarChar;

                sigUcParam.Value = entity.Sig_UC;
                profCCParam.Value = entity.Prof_CC;

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