using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using Instituto.Entities;

namespace Instituto.Mappers
{
    public class MapperAno : IMapper<Ano, object[]>
    {
        public void Create(Ano entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = ConnectionGate.GetCommand();
                cmd.CommandText = "INSERT INTO Ano VALUES (@ano, @semestre, @sig_curs)";

                SqlParameter ano = new SqlParameter();
                SqlParameter semestre = new SqlParameter();
                SqlParameter sig_curs = new SqlParameter();
                    
                cmd.Parameters.Add(ano);
                cmd.Parameters.Add(semestre);
                cmd.Parameters.Add(sig_curs);

                ano.ParameterName = "@ano";
                ano.SqlDbType = SqlDbType.Int;
                semestre.ParameterName = "@semestre";
                semestre.SqlDbType = SqlDbType.Int;
                sig_curs.ParameterName = "@sig_curs";
                sig_curs.SqlDbType = SqlDbType.VarChar;

                ano.Value = entity._Ano;
                semestre.Value = entity.Semestre;
                sig_curs.Value = entity.Sig_Curs;


                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public IEnumerable<Ano> Read(object[] parameteres)
        {
            List<Ano> anos = new List<Ano>();
            Ano currAno = new Ano();
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM Ano WHERE ano = @ano AND semestre = @semestre AND sig_curs = @sig_curs";

                SqlParameter anoQuery = new SqlParameter();
                SqlParameter semestreQuery = new SqlParameter();
                SqlParameter sig_cursQuery = new SqlParameter();
                    
                cmd.Parameters.Add(anoQuery);
                cmd.Parameters.Add(semestreQuery);
                cmd.Parameters.Add(sig_cursQuery);

                anoQuery.ParameterName = "@ano";
                anoQuery.SqlDbType = SqlDbType.Int;
                semestreQuery.ParameterName = "@semestre";
                semestreQuery.SqlDbType = SqlDbType.Int;
                sig_cursQuery.ParameterName = "@sig_curs";
                sig_cursQuery.SqlDbType = SqlDbType.VarChar;

                anoQuery.Value = parameteres[0];
                semestreQuery.Value = parameteres[1];
                sig_cursQuery.Value = parameteres[2];

                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string ano = rdr["ano"].ToString();                               // Do somthing with this rows string, for example to put them in to a list
                            string semestre = rdr["semestre"].ToString();
                            string sig_curs = rdr["sig_curs"].ToString();
                            currAno._Ano = Int32.Parse(ano);
                            currAno.Semestre = Int32.Parse(semestre);
                            currAno.Sig_Curs = sig_curs;
                        }
                    }
                }
                anos.Add(currAno);
                ts.Complete();
            }
            return anos;
        }

        public void Update(Ano entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Ano SET ano = @ano , semestre = @semestre , sig_curs = @sig_curs WHERE ano = @anoInput AND semestre = @semestreInput AND sig_curs = @sig_curs_Input";

                SqlParameter anoParam = new SqlParameter();
                SqlParameter semestreParam = new SqlParameter();
                SqlParameter sigCursParam = new SqlParameter();
                
                SqlParameter anoInput = new SqlParameter();
                SqlParameter semestreInput = new SqlParameter();
                SqlParameter sigCursInput = new SqlParameter();

                cmd.Parameters.Add(anoParam);
                cmd.Parameters.Add(semestreParam);
                cmd.Parameters.Add(sigCursParam);
                cmd.Parameters.Add(anoInput);
                cmd.Parameters.Add(semestreInput);
                cmd.Parameters.Add(sigCursInput);

                anoParam.ParameterName = "@ano";
                anoParam.SqlDbType = SqlDbType.Int;

                semestreParam.ParameterName = "@semestre";
                semestreParam.SqlDbType = SqlDbType.Int;

                sigCursParam.ParameterName = "@sig_curs";
                sigCursParam.SqlDbType = SqlDbType.VarChar;

                anoInput.ParameterName = "@anoInput";
                anoInput.SqlDbType = SqlDbType.Int;

                semestreInput.ParameterName = "@semestreInput";
                semestreInput.SqlDbType = SqlDbType.Int;

                sigCursInput.ParameterName = "@sig_curs_Input";
                sigCursInput.SqlDbType = SqlDbType.VarChar;

                anoInput.Value = entity._Ano;
                semestreInput.Value = entity.Semestre;
                sigCursInput.Value = entity.Sig_Curs;
                anoParam.Value = entity._Ano;
                semestreParam.Value = entity.Semestre;
                sigCursParam.Value = entity.Sig_Curs;
                
                using (var cn = ConnectionGate.SetConnection())
                {
                    ConnectionGate.OpenConnection();
                    cmd.ExecuteNonQuery();
                }
                ts.Complete();
            }
        }

        public void Delete(Ano entity)
        {
            using (var ts = new TransactionScope(TransactionScopeOption.Required))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM Ano WHERE ano = @ano AND semestre = @semestre AND sig_curs = @sig_curs";

                SqlParameter anoParam = new SqlParameter();
                SqlParameter semestreParam = new SqlParameter();
                SqlParameter sigCursParam = new SqlParameter();

                cmd.Parameters.Add(anoParam);
                cmd.Parameters.Add(semestreParam);
                cmd.Parameters.Add(sigCursParam);

                anoParam.ParameterName = "@ano";
                anoParam.SqlDbType = SqlDbType.Int;
                semestreParam.ParameterName = "@semestre";
                semestreParam.SqlDbType = SqlDbType.Int;
                sigCursParam.ParameterName = "@sig_curs";
                sigCursParam.SqlDbType = SqlDbType.VarChar;

                anoParam.Value = entity._Ano;
                semestreParam.Value = entity.Semestre;
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