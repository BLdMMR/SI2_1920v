using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace InstitutoEF
{
    public class Menu
    {

        private enum Options
        {
            Unknown = -1,
            Exit,    
            InserirDepartamento,
            RemoverDepartamento,
            AtualizarDepartamento,
            InserirSeccao,
            RemoverSeccao,
            AtualizarSeccao,
            InserirUC,
            RemoverUC,
            AtualizarUC,
            InserirUCEmCurso,
            RemoverUCDeCurso,
            CriarEstruturaDeCurso,
            MatricularAlunoEmCurso,
            InscreverAlunoEmUC,
            AtribuirNotaDeUCaAlunoEmAno,
            ListarInscEmUCEmDetAno,
            EliminarAlunoETodaAInfo
        }

        private delegate void Method();

        private Dictionary<Options, String> optionNames;
        private Dictionary<Options, Method> options;
        private Entities dbContext;
        private Array ops;

        public Menu()
        {
            dbContext = new Entities();
            Method aux = () =>
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            };
            options = new Dictionary<Options, Method>();
            options.Add(Options.Exit, () => { });
            options.Add(Options.InserirDepartamento, InserirDepartamento + aux);
            options.Add(Options.RemoverDepartamento, RemoverDepartamento + aux);
            options.Add(Options.AtualizarDepartamento, AtualizarDepartamento + aux);
            options.Add(Options.InserirSeccao, InserirSeccao + aux);
            options.Add(Options.RemoverSeccao, RemoverSeccao + aux);
            options.Add(Options.AtualizarSeccao, AtualizarSeccao + aux);
            options.Add(Options.InserirUC, InserirUC + aux);
            options.Add(Options.RemoverUC, RemoverUC + aux);
            options.Add(Options.AtualizarUC, AtualizarUC + aux);
            options.Add(Options.InserirUCEmCurso, InserirUCEmCurso + aux);
            options.Add(Options.RemoverUCDeCurso, RemoverUCDeCurso + aux);
            options.Add(Options.CriarEstruturaDeCurso, CriarEstruturaDeCusro + aux);
            options.Add(Options.MatricularAlunoEmCurso, MatricularAlunoEmCurso + aux);
            options.Add(Options.InscreverAlunoEmUC, InscreverAlunoEmUC + aux);
            options.Add(Options.AtribuirNotaDeUCaAlunoEmAno, AtribuirNotaDeUCaAlunoEmAno + aux);
            options.Add(Options.ListarInscEmUCEmDetAno, ListarInscEmUCEmDetAno + aux);
            options.Add(Options.EliminarAlunoETodaAInfo, EliminarAlunoETodaAInfo + aux);
            
            optionNames.Add(Options.Exit, "Exit");
            optionNames.Add(Options.InserirDepartamento, "Inserir Departamento");
            optionNames.Add(Options.RemoverDepartamento, "Remover Departamento");
            optionNames.Add(Options.AtualizarDepartamento, "Atualizar Departamento");
            optionNames.Add(Options.InserirSeccao, "Inserir Secção");
            optionNames.Add(Options.RemoverSeccao, "Remover Secção");
            optionNames.Add(Options.AtualizarSeccao, "Atualizar Secção");
            optionNames.Add(Options.InserirUC, "Inserir Unidade Curricular");
            optionNames.Add(Options.RemoverUC, "Remover Unidade Curricular");
            optionNames.Add(Options.AtualizarUC, "Atualizar Unidade Curricular");
            optionNames.Add(Options.InserirUCEmCurso, "Inserir Unidade Curricular num Curso");
            optionNames.Add(Options.RemoverUCDeCurso, "Remover Unidade Curricular de um Curso");
            optionNames.Add(Options.CriarEstruturaDeCurso, "Criar estrutura geral de um Curso");
            optionNames.Add(Options.MatricularAlunoEmCurso, "Matricular um Aluno num Curso");
            optionNames.Add(Options.InscreverAlunoEmUC, "Inscrever um Aluno numa Unidade Curricular");
            optionNames.Add(Options.AtribuirNotaDeUCaAlunoEmAno, "Atribuir nota de uma Unidade Curricular a um Aluno");
            optionNames.Add(Options.ListarInscEmUCEmDetAno, "Listar Inscrições numa Unidade Curricular no Ano Corrente");
            
            ops = options.Keys.ToArray();
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Testing Connection... \n\n\n");
            bool flag = true;
            try
            {
                using (SqlConnection conn = new SqlConnection(dbContext.Database.Connection.ConnectionString))
                {
                    conn.Open();
                    conn.Close();
                }
            }
            catch
            {
                flag = false;
            }

            if (flag)
            {
                Console.WriteLine("Connection Successfull\n\n\nPress any key to continue");
                Console.ReadKey();
                Options userInput = Options.Unknown;
                while (userInput != Options.Exit)
                {
                    Console.Clear();
                    userInput = DisplayMenu();
                    Console.Clear();
                    try
                    {
                        options[userInput]();
                    }
                    catch (KeyNotFoundException e)
                    {
                        Console.WriteLine("Option unknown. ->" + e.Message + ". \n\nPress any key to continue");
                        Console.ReadKey();
                    }
                    catch (NotImplementedException e)
                    {
                        Console.WriteLine("Currently unavailable... Sorry... :\\ \n\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("Connection Unsuccessfull\n\n\n Press any key to exit...");
                Console.ReadKey();
            }
        }

        private Options DisplayMenu()
        {
            Options op = Options.Unknown;
            Console.Clear();
            try
            {
                Console.WriteLine("MENU INSTITUTO DB");
                // for (int i = 1; i < ops.Length; i++)
                // {
                //     Console.WriteLine(i + ": " + ops.GetValue(i).ToString());
                // }

                bool first = true;
                int j = 0;
                foreach (var optionName in optionNames)
                {
                    if (first)
                    {
                        first = false;
                        continue;
                    }
                    ++j;
                    Console.WriteLine(j + ": " + optionName.Value);
                }

                Console.WriteLine("0: Exit");
                var result = Console.ReadLine();
                op = (Options) Enum.Parse(typeof(Options), result);
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid Option: ->" + e.Message);
                Console.WriteLine("Press any key to try again...\n\n");
                Console.ReadKey();
            }

            return op;
        }
        

        private void EliminarAlunoETodaAInfo()
        {
            throw new NotImplementedException();
        }

        private void ListarInscEmUCEmDetAno()
        {
            throw new NotImplementedException();
        }

        private void AtribuirNotaDeUCaAlunoEmAno()
        {
            throw new NotImplementedException();
        }

        private void InscreverAlunoEmUC()
        {
            throw new NotImplementedException();
        }

        private void MatricularAlunoEmCurso()
        {
            throw new NotImplementedException();
        }

        private void CriarEstruturaDeCusro()
        {
            throw new NotImplementedException();
        }

        private void RemoverUCDeCurso()
        {
            throw new NotImplementedException();
        }

        private void InserirUCEmCurso()
        {
            throw new NotImplementedException();
        }

        private void AtualizarUC()
        {
            throw new NotImplementedException();
        }

        private void RemoverUC()
        {
            throw new NotImplementedException();
        }

        private void InserirUC()
        {
            throw new NotImplementedException();
        }

        private void AtualizarSeccao()
        {
            throw new NotImplementedException();
        }

        private void RemoverSeccao()
        {
            throw new NotImplementedException();
        }

        private void InserirSeccao()
        {
            throw new NotImplementedException();
        }

        private void AtualizarDepartamento()
        {
            throw new NotImplementedException();
        }

        private void RemoverDepartamento()
        {
            throw new NotImplementedException();
        }

        private void InserirDepartamento()
        {
            throw new NotImplementedException();
        }
    }
}