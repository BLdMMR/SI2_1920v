using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperMatriculaAlunoEmAno : IMapper<MatriculaAlunoEmAno, object[]>
    {
        public void Create(MatriculaAlunoEmAno entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO MatriculaAlunoEmAno VALUES (@ano, @semestre, @sig_curs, @num_aluno)";

                SqlParameter numAluno = new SqlParameter();
                SqlParameter sig_curs = new SqlParameter();
                SqlParameter semestre = new SqlParameter();
                SqlParameter ano = new SqlParameter();
                    
                cmd.Parameters.Add(numAluno);
                cmd.Parameters.Add(sig_curs);
                cmd.Parameters.Add(semestre);
                cmd.Parameters.Add(ano);

                numAluno.ParameterName = "@ano";
                numAluno.SqlDbType = SqlDbType.Int;
                sig_curs.ParameterName = "@sig_curs";
                sig_curs.SqlDbType = SqlDbType.VarChar;
                semestre.ParameterName = "@semestre";
                semestre.SqlDbType = SqlDbType.Int;
                ano.ParameterName = "@ano";
                ano.SqlDbType = SqlDbType.Int;

                numAluno.Value = entity.Num_Aluno;
                sig_curs.Value = entity.Sig_Curs;
                semestre.Value = entity.Semestre;
                ano.Value = entity.Ano;

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<MatriculaAlunoEmAno> Read(object[] parameteres)
        {
            List<MatriculaAlunoEmAno> matriculas = new List<MatriculaAlunoEmAno>();
            MatriculaAlunoEmAno currMatricula = new MatriculaAlunoEmAno();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM MatriculaAlunoEmAno WHERE num_aluno = @num_aluno AND sig_curs = @sig_curs AND ano = @ano AND semestre = @semestre";

                SqlParameter numAlunoQuery = new SqlParameter();
                SqlParameter sig_cursQuery = new SqlParameter();
                SqlParameter anoQuery = new SqlParameter();
                SqlParameter semestreQuery = new SqlParameter();
                    
                cmd.Parameters.Add(numAlunoQuery);
                cmd.Parameters.Add(sig_cursQuery);
                cmd.Parameters.Add(anoQuery);
                cmd.Parameters.Add(semestreQuery);

                numAlunoQuery.ParameterName = "@num_aluno";
                numAlunoQuery.SqlDbType = SqlDbType.Int;
                sig_cursQuery.ParameterName = "@sig_curs";
                sig_cursQuery.SqlDbType = SqlDbType.VarChar;
                anoQuery.ParameterName = "@ano";
                anoQuery.SqlDbType = SqlDbType.Int;
                semestreQuery.ParameterName = "@semestre";
                semestreQuery.SqlDbType = SqlDbType.Int;

                numAlunoQuery.Value = parameteres[0];
                sig_cursQuery.Value = parameteres[1];
                anoQuery.Value = parameteres[2];
                semestreQuery.Value = parameteres[3];

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string numAluno = rdr["num_aluno"].ToString();   
                            string sig_curs = rdr["sig_curs"].ToString();
                            string ano = rdr["ano"].ToString();  
                            string semestre = rdr["semestre"].ToString();  
                            currMatricula.Num_Aluno = Int32.Parse(numAluno);
                            currMatricula.Sig_Curs = sig_curs;
                            currMatricula.Ano = Int32.Parse(ano);
                            currMatricula.Semestre = Int32.Parse(semestre);
                        }
                    }
                }
                matriculas.Add(currMatricula);
                ts.Complete();
            }
            return matriculas;
        }

        public void Update(MatriculaAlunoEmAno entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE MatriculaAlunoEmAno SET num_aluno = @num_aluno , sig_curs = @sig_curs , semestre = @semestre, ano = @ano WHERE num_aluno = @num_alunoInput AND sig_curs = @sig_cursInput AND ano = @anoInput AND semestre = @semestreInput";

                SqlParameter numAlunoParam = new SqlParameter();
                SqlParameter sigCursParam = new SqlParameter();
                SqlParameter anoParam = new SqlParameter();
                SqlParameter semestreParam = new SqlParameter();
                
                SqlParameter numAlunoInput = new SqlParameter();
                SqlParameter sigCursInput = new SqlParameter();
                SqlParameter anoInput = new SqlParameter();
                SqlParameter semestreInput = new SqlParameter();

                cmd.Parameters.Add(numAlunoParam);
                cmd.Parameters.Add(sigCursParam);
                cmd.Parameters.Add(anoParam);
                cmd.Parameters.Add(semestreParam);
                cmd.Parameters.Add(anoInput);
                cmd.Parameters.Add(numAlunoInput);
                cmd.Parameters.Add(sigCursInput);
                cmd.Parameters.Add(semestreInput);

                numAlunoParam.ParameterName = "@num_aluno";
                numAlunoParam.SqlDbType = SqlDbType.Int;

                sigCursParam.ParameterName = "@sig_curs";
                sigCursParam.SqlDbType = SqlDbType.VarChar;
                
                anoParam.ParameterName = "@ano";
                anoParam.SqlDbType = SqlDbType.Int;
                
                semestreParam.ParameterName = "@semestre";
                semestreParam.SqlDbType = SqlDbType.Int;
                
                anoInput.ParameterName = "@anoInput";
                anoInput.SqlDbType = SqlDbType.Int;
                
                semestreInput.ParameterName = "@semestreInput";
                semestreInput.SqlDbType = SqlDbType.Int;

                numAlunoInput.ParameterName = "@num_aluno_Input";
                numAlunoInput.SqlDbType = SqlDbType.Int;

                sigCursInput.ParameterName = "@sig_curs_Input";
                sigCursInput.SqlDbType = SqlDbType.VarChar;

                numAlunoInput.Value = entity.Num_Aluno;
                sigCursInput.Value = entity.Sig_Curs;
                anoInput.Value = entity.Ano;
                semestreInput.Value = entity.Semestre;
                numAlunoParam.Value = entity.Num_Aluno;
                sigCursParam.Value = entity.Sig_Curs;
                anoParam.Value = entity.Ano;
                semestreParam.Value = entity.Semestre;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(MatriculaAlunoEmAno entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM MatriculaAlunoEmAno WHERE num_aluno = @num_aluno AND sig_curs = @sig_curs AND ano = @ano AND semestre = @semestre";

                SqlParameter numAlunoParam = new SqlParameter();
                SqlParameter sigCursParam = new SqlParameter();
                SqlParameter anoParam = new SqlParameter();
                SqlParameter semestreParam = new SqlParameter();

                cmd.Parameters.Add(numAlunoParam);
                cmd.Parameters.Add(sigCursParam);
                cmd.Parameters.Add(anoParam);
                cmd.Parameters.Add(semestreParam);

                numAlunoParam.ParameterName = "@num_aluno";
                numAlunoParam.SqlDbType = SqlDbType.Int;
                sigCursParam.ParameterName = "@sig_curs";
                sigCursParam.SqlDbType = SqlDbType.VarChar;
                anoParam.ParameterName = "@ano";
                anoParam.SqlDbType = SqlDbType.Int;
                semestreParam.ParameterName = "@semestre";
                semestreParam.SqlDbType = SqlDbType.Int;

                numAlunoParam.Value = entity.Num_Aluno;
                sigCursParam.Value = entity.Sig_Curs;
                anoParam.Value = entity.Ano;
                semestreParam.Value = entity.Semestre;

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