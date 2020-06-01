using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperMatricula : IMapper<Matricula, object[]>
    {
        public void Create(Matricula entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO Matricula VALUES (@num_aluno, @sig_curs, @data_inic, @data_conc, @media)";

                SqlParameter numAluno = new SqlParameter();
                SqlParameter sig_curs = new SqlParameter();
                SqlParameter dataInic = new SqlParameter();
                SqlParameter dataConc = new SqlParameter();
                SqlParameter media = new SqlParameter();
                    
                cmd.Parameters.Add(numAluno);
                cmd.Parameters.Add(sig_curs);
                cmd.Parameters.Add(dataInic);
                cmd.Parameters.Add(dataConc);
                cmd.Parameters.Add(media);

                numAluno.ParameterName = "@ano";
                numAluno.SqlDbType = SqlDbType.Int;
                sig_curs.ParameterName = "@sig_curs";
                sig_curs.SqlDbType = SqlDbType.VarChar;
                dataInic.ParameterName = "@data_inic";
                dataInic.SqlDbType = SqlDbType.DateTime;
                dataConc.ParameterName = "@data_conc";
                dataConc.SqlDbType = SqlDbType.DateTime;
                media.ParameterName = "@media";
                media.SqlDbType = SqlDbType.Float;

                numAluno.Value = entity.Num_Aluno;
                sig_curs.Value = entity.Sig_Curs;
                dataInic.Value = entity.Data_Inic;
                dataConc.Value = entity.Data_Conc;
                media.Value = entity.Media;


                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<Matricula> Read(object[] parameteres)
        {
            List<Matricula> matriculas = new List<Matricula>();
            Matricula currMatricula = new Matricula();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Matricula WHERE num_aluno = @num_aluno AND sig_curs = @sig_curs";

                SqlParameter numAlunoQuery = new SqlParameter();
                SqlParameter sig_cursQuery = new SqlParameter();
                    
                cmd.Parameters.Add(numAlunoQuery);
                cmd.Parameters.Add(sig_cursQuery);

                numAlunoQuery.ParameterName = "@num_aluno";
                numAlunoQuery.SqlDbType = SqlDbType.Int;
                sig_cursQuery.ParameterName = "@sig_curs";
                sig_cursQuery.SqlDbType = SqlDbType.VarChar;

                numAlunoQuery.Value = parameteres[0];
                sig_cursQuery.Value = parameteres[1];

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string numAluno = rdr["num_aluno"].ToString();   
                            string sig_curs = rdr["sig_curs"].ToString();
                            currMatricula.Num_Aluno = Int32.Parse(numAluno);
                            currMatricula.Sig_Curs = sig_curs;
                        }
                    }
                }
                matriculas.Add(currMatricula);
                ts.Complete();
            }
            return matriculas;
        }

        public void Update(Matricula entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Matricula SET num_aluno = @num_aluno , sig_curs = @sig_curs , data_inic = @data_inic, data_conc = @data_conc, média = @media WHERE num_aluno = @num_aluno_Input AND sig_curs = @sig_curs_Input";

                SqlParameter numAlunoParam = new SqlParameter();
                SqlParameter sigCursParam = new SqlParameter();
                SqlParameter dataInicParam = new SqlParameter();
                SqlParameter dataConcParam = new SqlParameter();
                SqlParameter mediaParam = new SqlParameter();
                
                SqlParameter numAlunoInput = new SqlParameter();
                SqlParameter sigCursInput = new SqlParameter();

                cmd.Parameters.Add(numAlunoParam);
                cmd.Parameters.Add(sigCursParam);
                cmd.Parameters.Add(dataInicParam);
                cmd.Parameters.Add(dataConcParam);
                cmd.Parameters.Add(mediaParam);
                cmd.Parameters.Add(numAlunoInput);
                cmd.Parameters.Add(sigCursInput);

                numAlunoParam.ParameterName = "@num_aluno";
                numAlunoParam.SqlDbType = SqlDbType.Int;

                sigCursParam.ParameterName = "@sig_curs";
                sigCursParam.SqlDbType = SqlDbType.VarChar;
                
                dataInicParam.ParameterName = "@data_inic";
                dataInicParam.SqlDbType = SqlDbType.DateTime;
                
                dataConcParam.ParameterName = "@data_conc";
                dataConcParam.SqlDbType = SqlDbType.DateTime;
                
                mediaParam.ParameterName = "@media";
                mediaParam.SqlDbType = SqlDbType.Float;

                numAlunoInput.ParameterName = "@num_aluno_Input";
                numAlunoInput.SqlDbType = SqlDbType.Int;

                sigCursInput.ParameterName = "@sig_curs_Input";
                sigCursInput.SqlDbType = SqlDbType.VarChar;

                numAlunoInput.Value = entity.Num_Aluno;
                sigCursInput.Value = entity.Sig_Curs;
                numAlunoParam.Value = entity.Num_Aluno;
                sigCursParam.Value = entity.Sig_Curs;
                dataInicParam.Value = entity.Data_Inic;
                dataConcParam.Value = entity.Data_Conc;
                mediaParam.Value = entity.Media;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(Matricula entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Matricula WHERE num_aluno = @num_aluno AND sig_curs = @sig_curs";

                SqlParameter numAlunoParam = new SqlParameter();
                SqlParameter sigCursParam = new SqlParameter();

                cmd.Parameters.Add(numAlunoParam);
                cmd.Parameters.Add(sigCursParam);

                numAlunoParam.ParameterName = "@num_aluno";
                numAlunoParam.SqlDbType = SqlDbType.Int;
                sigCursParam.ParameterName = "@sig_curs";
                sigCursParam.SqlDbType = SqlDbType.VarChar;

                numAlunoParam.Value = entity.Num_Aluno;
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