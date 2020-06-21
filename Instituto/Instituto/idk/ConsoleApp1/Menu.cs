using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using ConsoleApp1;
using Instituto.Entities;

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

        private Dictionary<Options, String> optionNames = new Dictionary<Options, string>();
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
            Inscricao insc = dbContext.Inscricao.Create();
            insc.Ano = 2020;
            Console.WriteLine("Inserir SIGLA ÚNICA da Unidade Curricular");
            insc.Sig_UC = Console.ReadLine();

            foreach (var inscResult in dbContext.p_listarInscEmUCEmDetAno(insc.Sig_UC))
            {
                Console.WriteLine(inscResult.num_aluno + "|" + inscResult.num_cc + "|");
            }

        }

        private void AtribuirNotaDeUCaAlunoEmAno()
        {
            Console.WriteLine("Inserir NÚMERO DE ALUNO");
            int num_aluno = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Inserir SIGLA ÚNICA da Unidade Curricular");
            string sig_uc = Console.ReadLine(); 
            Console.WriteLine("Inserir ANO de Inscrição");
            int ano_insc = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Inserir NOTA a atribuir");
            int nota = Int32.Parse(Console.ReadLine());
            
            if (dbContext.p_atribuirNotaAAlunoEmUC(num_aluno, sig_uc, ano_insc, nota) >= 0) 
                Console.WriteLine("Nota atribuída a aluno");
            else Console.WriteLine("ATENÇÂO: Nota NÃO atribuída a aluno");
        }

        private void InscreverAlunoEmUC()
        {
            Console.WriteLine("Inserir NÚMERO DE ALUNO");
            int num_aluno = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Inserir SIGLA ÚNICA da Unidade Curricular");
            string sig_uc = Console.ReadLine(); 
            Console.WriteLine("Inserir ANO de Inscrição");
            int ano_insc = Int32.Parse(Console.ReadLine());
            
            if (dbContext.p_inscreverAlunoEmUC(num_aluno, sig_uc, ano_insc) >= 0) 
                Console.WriteLine("Aluno inscrito na UC");
            else Console.WriteLine("ATENÇÂO: Aluno NÃO inscrito na UC");
        }

        private void MatricularAlunoEmCurso()
        {
            Console.WriteLine("Inserir NÚMERO DO CARTÃO DE CIDADÃO do Aluno");
            string num_cc = Console.ReadLine();
            Console.WriteLine("Inserir NOME do Aluno");
            string nome = Console.ReadLine(); 
            Console.WriteLine("Inserir ENDEREÇO do Aluno");
            string endereco = Console.ReadLine();
            Console.WriteLine("Inserir CÓDIGO POSTAL do Aluno");
            string cod_post = Console.ReadLine();
            Console.WriteLine("Inserir LOCALIDADE do Aluno");
            string localidade = Console.ReadLine(); 
            Console.WriteLine("Inserir DATA DE NASCIMENTO do Aluno");
            Console.Write("\nAno:");
            int ano_nasc = Int32.Parse(Console.ReadLine());
            Console.Write("\nMês:");
            int mes_nasc = Int32.Parse(Console.ReadLine());
            Console.Write("\nDia:");
            int dia_nasc = Int32.Parse(Console.ReadLine());
            DateTime data_nasc = new DateTime(ano_nasc, mes_nasc, dia_nasc);
            Console.WriteLine("Inserir SIGLA DO CURSO onde inscrever o Aluno");
            string sig_curs = Console.ReadLine();
            
            if (dbContext.p_matricularAlunoEmCurso(num_cc, nome, endereco, cod_post, localidade, data_nasc, sig_curs) >= 0)
                Console.WriteLine("Aluno matriculado no Curso");
            else Console.WriteLine("ATENÇÂO: Aluno NÃO matriculado no Curso");
        }

        private void CriarEstruturaDeCusro()
        {
            Console.WriteLine("Inserir SIGLA ÚNICA do Curso");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO do Curso");
            string descr = Console.ReadLine(); 
            Console.WriteLine("Inserir SIGLA ÚNICA do Departamento onde o Curso se deve inserir");
            string sig_un_dep = Console.ReadLine();
            Console.WriteLine("Inserir NÚMERO DE ANOS do Curso");
            int num_anos = Int32.Parse(Console.ReadLine());
            
            if (dbContext.p_criarEstruturaDeCurso(sig_un, descr, sig_un_dep, num_anos) >= 0) 
                Console.WriteLine("Estrutura do Curso criada");
            else Console.WriteLine("ATENÇÂO: Estrutura do Curso NÃO criada");
        }

        private void RemoverUCDeCurso()
        {
            throw new NotImplementedException();
        }

        private void InserirUCEmCurso()
        {
            Console.WriteLine("Inserir SIGLA ÚNICA da Unidade Curricular");
            string sig_uc = Console.ReadLine();
            Console.WriteLine("Inserir NÚMERO DE CRÁDITOS da Unidade Curricular");
            int num_cred = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Inserir DESCRIÇÃO da Unidade Curricular");
            string descr = Console.ReadLine();
            Console.WriteLine("Inserir SIGLA ÚNICA do Curso onde a Unidade Curricular se deve inserir");
            string sig_un_curs = Console.ReadLine();
            Console.WriteLine("Inserir Número do Semestre correspondente à Unidade Curricular");
            int semestre = Int32.Parse(Console.ReadLine());
            
            if (dbContext.p_inserirUCEmCurso(sig_uc, num_cred, descr, sig_un_curs, semestre) >= 0) 
                Console.WriteLine("Unidade Curricular inserida no Curso ");
            else Console.WriteLine("ATENÇÂO: Unidade Curricular NÂO inserida no Curso");   
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
            Console.WriteLine("Inserir SIGLA ÚNICA da Secção");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO da Secção");
            string descr = Console.ReadLine();
            Console.WriteLine("Inserir SIGLA ÚNICA do Departamento onde a secção se encontra inserida");
            string sig_un_dep = Console.ReadLine();
            
            if (dbContext.p_atualizarSeccao(sig_un, descr, sig_un_dep) >= 0)
                Console.WriteLine("Secção atualizada");
            else Console.WriteLine("ATENÇÂO: Secção NÂO atualizada");  
        }

        private void RemoverSeccao()
        {
            Console.WriteLine("Inserir SIGLA ÚNICA da Secção");
            string sig_un = Console.ReadLine();
            
            if (dbContext.p_removerSeccao(sig_un) >= 0)
                Console.WriteLine("Secção removida");
            else Console.WriteLine("ATENÇÂO: Secção NÂO removida");
        }

        private void InserirSeccao()
        {
            Console.WriteLine("Inserir SIGLA ÚNICA da Secção");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO da Secção");
            string descr = Console.ReadLine();
            Console.WriteLine("Inserir SIGLA ÚNICA do Departamento onde a secção se encontra inserida");
            string sig_un_dep = Console.ReadLine();
            
            if (dbContext.p_inserirSeccao(sig_un, descr, sig_un_dep) >= 0) 
                Console.WriteLine("Secção inserida");
            else Console.WriteLine("ATENÇÂO: Secção NÂO inserida");   
        }

        private void AtualizarDepartamento()
        {
            Console.WriteLine("Inserir SIGLA ÚNICA do departemento");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO do departemento");
            string descr = Console.ReadLine();
            
            if (dbContext.p_atualizarDepartamento(sig_un, descr) >= 0) 
                Console.WriteLine("Departamento atualizado");
            else Console.WriteLine("ATENÇÂO: Departamento NÂO atualizado");  
        }

        private void RemoverDepartamento()
        {
            Console.WriteLine("Inserir SIGLA ÚNICA do departemento");
            string sig_un = Console.ReadLine();
             if (dbContext.p_removerDepartamento(sig_un) >= 0)
                 Console.WriteLine("Departamento removido");
             else Console.WriteLine("ATENÇÂO: Departamento NÂO removido");
            
        }

        private void InserirDepartamento()
        {
            Console.WriteLine("Inserir SIGLA UNICA do departamento:\n");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO do departamento:\n");
            string descr = Console.ReadLine();
            if (dbContext.p_inserirDepartamento(sig_un, descr) >= 0) 
                Console.WriteLine("Departamento inserido");
            else Console.WriteLine("ATENÇÂO: Departamento NÂO inserido");
        }
    }
}