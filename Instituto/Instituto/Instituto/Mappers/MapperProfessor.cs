using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperProfessor : IMapper<Professor, int>
    {
        public void Create(Professor entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO Professor VALUES (@num_cc, @nome, @area_esp, @categoria, @sig_sec, @sig_dep, @coord_sec)";

                SqlParameter numCc = new SqlParameter();
                SqlParameter nome = new SqlParameter();
                SqlParameter areaEsp = new SqlParameter();
                SqlParameter categoria = new SqlParameter();
                SqlParameter sigSec = new SqlParameter();
                SqlParameter sigDep = new SqlParameter();
                SqlParameter coordSec = new SqlParameter();


                cmd.Parameters.Add(numCc);
                cmd.Parameters.Add(nome);
                cmd.Parameters.Add(areaEsp);
                cmd.Parameters.Add(categoria);
                cmd.Parameters.Add(sigSec);
                cmd.Parameters.Add(sigDep);
                cmd.Parameters.Add(coordSec);

                numCc.ParameterName = "@num_cc";
                numCc.SqlDbType = SqlDbType.VarChar;
                nome.ParameterName = "@nome";
                nome.SqlDbType = SqlDbType.VarChar;
                areaEsp.ParameterName = "@area_esp";
                areaEsp.SqlDbType = SqlDbType.Text;
                categoria.ParameterName = "@categoria";
                categoria.SqlDbType = SqlDbType.VarChar;
                sigSec.ParameterName = "@sig_sec";
                sigSec.SqlDbType = SqlDbType.VarChar;
                sigDep.ParameterName = "@sig_dep";
                sigDep.SqlDbType = SqlDbType.VarChar;
                coordSec.ParameterName = "@coord_sec";
                coordSec.SqlDbType = SqlDbType.VarChar;


                numCc.Value = entity.Num_CC;
                nome.Value = entity.Nome;
                areaEsp.Value = entity.Nome;
                categoria.Value = entity.Categoria;
                sigSec.Value = entity.Sig_Sec;
                sigDep.Value = entity.Sig_Dep;
                coordSec.Value = entity.Coord_Sec;


                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<Professor> Read(int id)
        {
            List<Professor> professores = new List<Professor>();
            Professor professor = new Professor();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Professor WHERE num_cc = @cc";

                SqlParameter ccParam = new SqlParameter();
                cmd.Parameters.Add(ccParam);
                ccParam.ParameterName = "@cc";
                ccParam.SqlDbType = SqlDbType.Int;
                ccParam.Value = id;

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string cc = rdr["num_cc"].ToString();
                            string nome = rdr["nome"].ToString();
                            string areaEsp = rdr["area_esp"].ToString();
                            string categoria = rdr["categoria"].ToString();
                            string sigSec = rdr["sig_sec"].ToString();
                            string sigDep = rdr["sig_dep"].ToString();
                            string coordSec = rdr["coord_sec"].ToString();
                            professor.Num_CC = cc;
                            professor.Nome = nome;
                            professor.Area_Esp = areaEsp;
                            professor.Categoria = categoria;
                            professor.Sig_Sec = sigSec;
                            professor.Sig_Dep = sigDep;
                            professor.Coord_Sec = coordSec;
                        }
                    }
                }
                professores.Add(professor);
                ts.Complete();
            }
            return professores;
        }

        public void Update(Professor entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Professor SET num_cc = @num_cc, nome = @nome, area_esp = @area_esp, categoria = @categoria, sig_sec = @sig_sec, sig_dep = @sig_dep, coord_sec = @coord_sec WHERE num_cc = @num_cc_Input";

                SqlParameter numCcParam = new SqlParameter();
                SqlParameter nomeParam = new SqlParameter();
                SqlParameter areaEspParam = new SqlParameter();
                SqlParameter categoriaParam = new SqlParameter();
                SqlParameter sigSecParam = new SqlParameter();
                SqlParameter sigDepParam = new SqlParameter();
                SqlParameter coordSecParam = new SqlParameter();
                SqlParameter numCCInput = new SqlParameter();

                cmd.Parameters.Add(numCcParam);
                cmd.Parameters.Add(nomeParam);
                cmd.Parameters.Add(areaEspParam);
                cmd.Parameters.Add(categoriaParam);
                cmd.Parameters.Add(sigSecParam);
                cmd.Parameters.Add(sigDepParam);
                cmd.Parameters.Add(coordSecParam);
                cmd.Parameters.Add(numCCInput);

                numCcParam.ParameterName = "@num_cc";
                numCcParam.SqlDbType = SqlDbType.VarChar;

                nomeParam.ParameterName = "@nome";
                nomeParam.SqlDbType = SqlDbType.VarChar;

                areaEspParam.ParameterName = "@area_esp";
                areaEspParam.SqlDbType = SqlDbType.Text;

                categoriaParam.ParameterName = "@categoria";
                categoriaParam.SqlDbType = SqlDbType.VarChar;

                sigSecParam.ParameterName = "@sig_sec";
                sigSecParam.SqlDbType = SqlDbType.VarChar;

                sigDepParam.ParameterName = "@sig_dep";
                sigDepParam.SqlDbType = SqlDbType.VarChar;
                
                coordSecParam.ParameterName = "@sig_dep";
                coordSecParam.SqlDbType = SqlDbType.VarChar;

                numCCInput.ParameterName = "@num_cc_Input";
                numCCInput.SqlDbType = SqlDbType.Int;
                
                numCCInput.Value = entity.Num_CC;
                numCcParam.Value = entity.Num_CC;
                nomeParam.Value = entity.Nome;
                areaEspParam.Value = entity.Area_Esp;
                categoriaParam.Value = entity.Categoria;
                sigSecParam.Value = entity.Sig_Sec;
                sigDepParam.Value = entity.Sig_Dep;
                coordSecParam.Value = entity.Coord_Sec;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(Professor entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Professor WHERE num_cc = @num_cc";

                SqlParameter numCCParam = new SqlParameter();

                cmd.Parameters.Add(numCCParam);

                numCCParam.ParameterName = "@num_cc";
                numCCParam.SqlDbType = SqlDbType.Int;

                numCCParam.Value = entity.Num_CC;

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