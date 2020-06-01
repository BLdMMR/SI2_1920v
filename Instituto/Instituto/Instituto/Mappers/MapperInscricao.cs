using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperInscricao : IMapper<Inscricao, object[]>
    {
        public void Create(Inscricao entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO Inscrição VALUES (@ano, @nota, @num_aluno, @sig_uc)";

                SqlParameter ano = new SqlParameter();
                SqlParameter nota = new SqlParameter();
                SqlParameter num_aluno = new SqlParameter();
                SqlParameter sig_uc = new SqlParameter();
                    
                cmd.Parameters.Add(ano);
                cmd.Parameters.Add(nota);
                cmd.Parameters.Add(num_aluno);
                cmd.Parameters.Add(sig_uc);

                ano.ParameterName = "@ano";
                ano.SqlDbType = SqlDbType.Int;
                nota.ParameterName = "@nota";
                nota.SqlDbType = SqlDbType.Int;
                num_aluno.ParameterName = "@num_aluno";
                num_aluno.SqlDbType = SqlDbType.Int;
                sig_uc.ParameterName = "@sig_uc";
                sig_uc.SqlDbType = SqlDbType.VarChar;

                ano.Value = entity.Ano;
                nota.Value = entity.Nota;
                num_aluno.Value = entity.Num_Aluno;
                sig_uc.Value = entity.Sig_UC;


                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<Inscricao> Read(object[] parameteres)
        {
            List<Inscricao> inscricoes = new List<Inscricao>();
            Inscricao currInscricao = new Inscricao();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Inscrição WHERE ano = @ano AND num_aluno = @num_aluno AND sig_uc = @sig_uc";

                SqlParameter anoQuery = new SqlParameter();
                SqlParameter numAlunoQuery = new SqlParameter();
                SqlParameter sigUcQuery = new SqlParameter();
                    
                cmd.Parameters.Add(anoQuery);
                cmd.Parameters.Add(numAlunoQuery);
                cmd.Parameters.Add(sigUcQuery);

                anoQuery.ParameterName = "@ano";
                anoQuery.SqlDbType = SqlDbType.Int;
                numAlunoQuery.ParameterName = "@num_aluno";
                numAlunoQuery.SqlDbType = SqlDbType.Int;
                sigUcQuery.ParameterName = "@sig_uc";
                sigUcQuery.SqlDbType = SqlDbType.VarChar;

                anoQuery.Value = parameteres[0];
                numAlunoQuery.Value = parameteres[1];
                sigUcQuery.Value = parameteres[2];

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string ano = rdr["ano"].ToString();                               // Do somthing with this rows string, for example to put them in to a list
                            string numAluno = rdr["num_aluno"].ToString();
                            string sig_uc = rdr["sig_uc"].ToString();
                            currInscricao.Ano = Int32.Parse(ano);
                            currInscricao.Num_Aluno = Int32.Parse(numAluno);
                            currInscricao.Sig_UC = sig_uc;
                        }
                    }
                }
                inscricoes.Add(currInscricao);
                ts.Complete();
            }
            return inscricoes;
        }

        public void Update(Inscricao entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Inscrição SET ano = @ano , nota = @nota , num_aluno = @num_aluno, sig_uc = @sig_uc WHERE ano = @anoInput AND num_aluno = @num_alunoInput AND sig_uc = @sig_uc_Input";

                SqlParameter anoParam = new SqlParameter();
                SqlParameter notaParam = new SqlParameter();
                SqlParameter numAlunoParam = new SqlParameter();
                SqlParameter sigUCParam = new SqlParameter();
                
                SqlParameter anoInput = new SqlParameter();
                SqlParameter numAlunoInput = new SqlParameter();
                SqlParameter sigUCInput = new SqlParameter();

                cmd.Parameters.Add(anoParam);
                cmd.Parameters.Add(notaParam);
                cmd.Parameters.Add(numAlunoParam);
                cmd.Parameters.Add(sigUCParam);
                cmd.Parameters.Add(anoInput);
                cmd.Parameters.Add(numAlunoInput);
                cmd.Parameters.Add(sigUCInput);

                anoParam.ParameterName = "@ano";
                anoParam.SqlDbType = SqlDbType.Int;

                notaParam.ParameterName = "@nota";
                notaParam.SqlDbType = SqlDbType.Int;

                numAlunoParam.ParameterName = "@num_aluno";
                numAlunoParam.SqlDbType = SqlDbType.Int;

                sigUCParam.ParameterName = "@sig_uc";
                sigUCParam.SqlDbType = SqlDbType.VarChar;

                anoInput.ParameterName = "@anoInput";
                anoInput.SqlDbType = SqlDbType.Int;

                numAlunoInput.ParameterName = "@num_alunoInput";
                numAlunoInput.SqlDbType = SqlDbType.Int;

                sigUCInput.ParameterName = "@sig_uc_Input";
                sigUCInput.SqlDbType = SqlDbType.VarChar;

                anoInput.Value = entity.Ano;
                numAlunoInput.Value = entity.Num_Aluno;
                sigUCInput.Value = entity.Sig_UC;
                anoParam.Value = entity.Ano;
                notaParam.Value = entity.Nota;
                numAlunoParam.Value = entity.Num_Aluno;
                sigUCParam.Value = entity.Sig_UC;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(Inscricao entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Inscrição WHERE ano = @ano AND num_aluno = @num_aluno AND sig_uc = @sig_uc";

                SqlParameter anoParam = new SqlParameter();
                SqlParameter numAlunoParam = new SqlParameter();
                SqlParameter sigUCParam = new SqlParameter();

                cmd.Parameters.Add(anoParam);
                cmd.Parameters.Add(numAlunoParam);
                cmd.Parameters.Add(sigUCParam);

                anoParam.ParameterName = "@ano";
                anoParam.SqlDbType = SqlDbType.Int;
                numAlunoParam.ParameterName = "@num_aluno";
                numAlunoParam.SqlDbType = SqlDbType.Int;
                sigUCParam.ParameterName = "@sig_uc";
                sigUCParam.SqlDbType = SqlDbType.VarChar;

                anoParam.Value = entity.Ano;
                numAlunoParam.Value = entity.Num_Aluno;
                sigUCParam.Value = entity.Sig_UC;

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