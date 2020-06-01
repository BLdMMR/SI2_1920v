using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperAluno : IMapper<Aluno, int>
    {
        public void Create(Aluno aluno)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO Aluno VALUES (@num_cc, @nome, @endereço, @cod_post, @localidade, @data_nasc)";

                SqlParameter numCc = new SqlParameter();
                SqlParameter nome = new SqlParameter();
                SqlParameter endereco = new SqlParameter();
                SqlParameter codPost = new SqlParameter();
                SqlParameter localidade = new SqlParameter();
                SqlParameter dataNasc = new SqlParameter();


                cmd.Parameters.Add(numCc);
                cmd.Parameters.Add(nome);
                cmd.Parameters.Add(endereco);
                cmd.Parameters.Add(codPost);
                cmd.Parameters.Add(localidade);
                cmd.Parameters.Add(dataNasc);

                numCc.ParameterName = "@num_cc";
                numCc.SqlDbType = SqlDbType.VarChar;
                nome.ParameterName = "@nome";
                nome.SqlDbType = SqlDbType.VarChar;
                endereco.ParameterName = "@endereço";
                endereco.SqlDbType = SqlDbType.VarChar;
                codPost.ParameterName = "@cod_post";
                codPost.SqlDbType = SqlDbType.VarChar;
                localidade.ParameterName = "@localidade";
                localidade.SqlDbType = SqlDbType.VarChar;
                dataNasc.ParameterName = "@data_nasc";
                dataNasc.SqlDbType = SqlDbType.DateTime;


                numCc.Value = aluno.Num_CC;
                nome.Value = aluno.Nome;
                endereco.Value = aluno.Nome;
                codPost.Value = aluno.Cod_Post;
                localidade.Value = aluno.Localidade;
                dataNasc.Value = aluno.Data_Nasc;


                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<Aluno> Read(int num_cc_param)
        {
            List<Aluno> alunos = new List<Aluno>();
            Aluno aluno = new Aluno();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Aluno WHERE num_cc = @cc";

                SqlParameter ccParam = new SqlParameter();
                cmd.Parameters.Add(ccParam);
                ccParam.ParameterName = "@cc";
                ccParam.SqlDbType = SqlDbType.Int;
                ccParam.Value = num_cc_param;

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string numAluno = rdr["num_aluno"].ToString();                               // Do somthing with this rows string, for example to put them in to a list
                            string cc = rdr["num_cc"].ToString();
                            string nome = rdr["nome"].ToString();
                            string endereco = rdr["endereço"].ToString();
                            string codPost = rdr["cod_post"].ToString();
                            string localidade = rdr["localidade"].ToString();
                            string dataNasc = rdr["data_nasc"].ToString();
                            aluno.Num_Aluno = Int32.Parse(numAluno);
                            aluno.Num_CC = cc;
                            aluno.Nome = nome;
                            aluno.Endereco = endereco;
                            aluno.Cod_Post = codPost;
                            aluno.Localidade = localidade;
                            aluno.Data_Nasc = DateTime.Parse(dataNasc);
                        }
                    }
                }
                alunos.Add(aluno);
                ts.Complete();
            }
            return alunos;
        }

        public void Update(Aluno aluno)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Aluno SET num_cc = @num_cc, nome = @nome, endereço = @endereco, cod_post = @cod_post, localidade = @localidade, data_nasc = @data_nasc WHERE num_aluno = @num_aluno";

                SqlParameter numCcParam = new SqlParameter();
                SqlParameter nomeParam = new SqlParameter();
                SqlParameter enderecoParam = new SqlParameter();
                SqlParameter codPostParam = new SqlParameter();
                SqlParameter localidadeParam = new SqlParameter();
                SqlParameter dataNascParam = new SqlParameter();
                SqlParameter numAlunoParam = new SqlParameter();

                cmd.Parameters.Add(numCcParam);
                cmd.Parameters.Add(nomeParam);
                cmd.Parameters.Add(enderecoParam);
                cmd.Parameters.Add(codPostParam);
                cmd.Parameters.Add(localidadeParam);
                cmd.Parameters.Add(dataNascParam);
                cmd.Parameters.Add(numAlunoParam);

                numCcParam.ParameterName = "@num_cc";
                numCcParam.SqlDbType = SqlDbType.VarChar;

                nomeParam.ParameterName = "@nome";
                nomeParam.SqlDbType = SqlDbType.VarChar;

                enderecoParam.ParameterName = "@endereco";
                enderecoParam.SqlDbType = SqlDbType.VarChar;

                codPostParam.ParameterName = "@cod_post";
                codPostParam.SqlDbType = SqlDbType.VarChar;

                localidadeParam.ParameterName = "@localidade";
                localidadeParam.SqlDbType = SqlDbType.VarChar;

                dataNascParam.ParameterName = "@data_nasc";
                dataNascParam.SqlDbType = SqlDbType.DateTime;

                numAlunoParam.ParameterName = "@num_aluno";
                numAlunoParam.SqlDbType = SqlDbType.Int;

                numCcParam.Value = aluno.Num_CC;
                nomeParam.Value = aluno.Nome;
                enderecoParam.Value = aluno.Endereco;
                codPostParam.Value = aluno.Cod_Post;
                localidadeParam.Value = aluno.Localidade;
                dataNascParam.Value = aluno.Data_Nasc;
                numAlunoParam.Value = aluno.Num_Aluno;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(Aluno aluno)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Aluno WHERE num_aluno = @num_aluno";

                SqlParameter numAlunoParam = new SqlParameter();

                cmd.Parameters.Add(numAlunoParam);

                numAlunoParam.ParameterName = "@num_aluno";
                numAlunoParam.SqlDbType = SqlDbType.Int;

                numAlunoParam.Value = aluno.Num_Aluno;

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