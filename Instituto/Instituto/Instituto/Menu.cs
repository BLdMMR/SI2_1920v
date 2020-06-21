using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ADO.Net.Client.Annotations;
using Instituto.Entities;
using Instituto.Mappers;

namespace Instituto
{
    public class Menu
    {
        private enum Options
        {
            Invalid = -1,
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
            EliminarAlunoETodaAInfo,
            Unknown
            
        }
        private Dictionary<Options, String> optionNames = new Dictionary<Options, string>();
         

        private delegate void Method();

        private Dictionary<Options, Method> menuOptions;

        private Array ops;

        public Menu()
        {
            ConnectionGate.SetConnection();
            //Console.WriteLine(ConnectionGate.TestConnection());
            
            Method aux = () =>
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            };
            menuOptions = new Dictionary<Options, Method>();
            menuOptions.Add(Options.Exit, () => {});
            menuOptions.Add(Options.InserirDepartamento, InserirDepartamento + aux);
            menuOptions.Add(Options.RemoverDepartamento, RemoverDepartamento + aux);        
            menuOptions.Add(Options.AtualizarDepartamento, AtualizarDepartamento + aux);
            menuOptions.Add(Options.InserirSeccao, InserirSeccao + aux);
            menuOptions.Add(Options.RemoverSeccao, RemoverSeccao + aux);
            menuOptions.Add(Options.AtualizarSeccao, AtualizarSeccao + aux);
            menuOptions.Add(Options.InserirUC, InserirUC + aux);
            menuOptions.Add(Options.RemoverUC, RemoverUC + aux);
            menuOptions.Add(Options.AtualizarUC, AtualizarUC + aux);
            menuOptions.Add(Options.InserirUCEmCurso, InserirUCEmCurso + aux);
            menuOptions.Add(Options.RemoverUCDeCurso, RemoverUCDeCurso + aux);
            menuOptions.Add(Options.CriarEstruturaDeCurso, CriarEstruturaDeCusro + aux);
            menuOptions.Add(Options.MatricularAlunoEmCurso, MatricularAlunoEmCurso + aux);
            menuOptions.Add(Options.InscreverAlunoEmUC, InscreverAlunoEmUC + aux);
            menuOptions.Add(Options.AtribuirNotaDeUCaAlunoEmAno, AtribuirNotaDeUCaAlunoEmAno + aux);
            menuOptions.Add(Options.ListarInscEmUCEmDetAno, ListarInscEmUCEmDetAno + aux );
            menuOptions.Add(Options.EliminarAlunoETodaAInfo, EliminarAlunoETodaAInfo + aux);
            //menuOptions.Add(Options.Exit, null);
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
            
            ops = menuOptions.Keys.ToArray();

        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine("Testing Connection... \n\n\n");
            if (ConnectionGate.TestConnection())
            {
                Console.WriteLine("Connection Successful\n\nPress any key to continue...");
                Console.ReadKey();
                Options userInput = Options.Unknown;
                while (userInput != Options.Exit)
                {
                    Console.Clear();
                    userInput = DisplayMenu();
                    Console.Clear();
                    try
                    {
                        menuOptions[userInput]();
                    }
                    catch (KeyNotFoundException e)
                    {
                        Console.WriteLine("Option unknown: " + e.Message + " \n\nPress any key to try again");
                        Console.ReadKey();
                    }
                    catch (NotImplementedException ex)
                    {
                        Console.WriteLine("Currently unavailable... Sorry... :\\ \n\nPress any key to continue...");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("Connection unsuccessful... \n\n Press any key to exit...");
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
             Aluno aluno = new Aluno();
             Console.WriteLine("Inserir NÚMERO DE ALUNO a remover");
             aluno.Num_Aluno = Int32.Parse(Console.ReadLine());
             object[] parameters = {"num_aluno", aluno.Num_Aluno};
             Matricula matricula = new Matricula();
             matricula.Num_Aluno = aluno.Num_Aluno;
             MapperMatricula mapperMatricula = new MapperMatricula();
             mapperMatricula.Delete(matricula);
         
         

        }

        private void ListarInscEmUCEmDetAno()
        {
            Inscricao insc = new Inscricao();
            insc.Ano = 2020;
            Console.WriteLine("Inserir SIGLA ÚNICA da Unidade Curricular");
            insc.Sig_UC = Console.ReadLine();

            object[] parameters = {"ano", insc.Ano, "sig_uc", insc.Sig_UC};            
            MapperInscricao map = new MapperInscricao();
            
            IEnumerable<Inscricao> ret = map.Read(parameters);

            foreach (var param in ret)
            {
                Console.WriteLine(param.Num_Aluno);
            }

        }

        private void AtribuirNotaDeUCaAlunoEmAno()
        {
            string command = "p_atribuirNotaAAlunoEmUC";
            Console.WriteLine("Inserir NÚMERO DE ALUNO");
            int num_aluno = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Inserir SIGLA ÚNICA da Unidade Curricular");
            string sig_uc = Console.ReadLine(); 
            Console.WriteLine("Inserir ANO de Inscrição");
            int ano_insc = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Inserir NOTA a atribuir");
            int nota = Int32.Parse(Console.ReadLine());

            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@param_num_aluno", num_aluno);
            parameters[1] = new SqlParameter("@param_sig_uc", sig_uc);
            parameters[2] = new SqlParameter("@param_ano", ano_insc);
            parameters[3] = new SqlParameter("@param_nota", nota);

            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            if (ret != -1) Console.WriteLine("Nota atribuída a aluno");
            else Console.WriteLine("ATENÇÂO: Nota NÃO atribuída a aluno");

        }

        private void InscreverAlunoEmUC()
        {
            string command = "p_inscreverAlunoEmUC";
            Console.WriteLine("Inserir NÚMERO DE ALUNO");
            int num_aluno = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Inserir SIGLA ÚNICA da Unidade Curricular");
            string sig_uc = Console.ReadLine(); 
            Console.WriteLine("Inserir ANO de Inscrição");
            int ano_insc = Int32.Parse(Console.ReadLine());
            
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@param_num_aluno", num_aluno);
            parameters[1] = new SqlParameter("@param_sig_uc", sig_uc);
            parameters[2] = new SqlParameter("@param_ano", ano_insc);

            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            if (ret != -1) Console.WriteLine("Aluno inscrito na UC");
            else Console.WriteLine("ATENÇÂO: Aluno NÃO inscrito na UC");

        }

        private void MatricularAlunoEmCurso()
        {
            string command = "p_matricularAlunoEmCurso";
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
            
            SqlParameter[] parameters = new SqlParameter[7];
            parameters[0] = new SqlParameter("@param_num_cc", num_cc);
            parameters[1] = new SqlParameter("@param_nome", nome);
            parameters[2] = new SqlParameter("@param_endereco", endereco);
            parameters[3] = new SqlParameter("@param_cod_post", cod_post);
            parameters[4] = new SqlParameter("@param_localidade", localidade);
            parameters[5] = new SqlParameter("@param_data_nasc", data_nasc);
            parameters[6] = new SqlParameter("@param_sig_curs", sig_curs);
            
            
            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            if (ret != -1) Console.WriteLine("Aluno matriculado no Curso");
            else Console.WriteLine("ATENÇÂO: Aluno NÃO matriculado no Curso");
        }

        private void CriarEstruturaDeCusro()
        {
            string command = "p_criarCurso";
            Console.WriteLine("Inserir SIGLA ÚNICA do Curso");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO do Curso");
            string descr = Console.ReadLine(); 
            Console.WriteLine("Inserir SIGLA ÚNICA do Departamento onde o Curso se deve inserir");
            string sig_un_dep = Console.ReadLine();
            Console.WriteLine("Inserir NÚMERO DE ANOS do Curso");
            int num_anos = Int32.Parse(Console.ReadLine());
            
            SqlParameter[] parameters = new SqlParameter[4];
            parameters[0] = new SqlParameter("@param_sig_un", sig_un);
            parameters[1] = new SqlParameter("@param_descr", descr);
            parameters[2] = new SqlParameter("@param_sig_dep", sig_un_dep);
            parameters[3] = new SqlParameter("@num_anos", num_anos);
            
            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            if (ret != -1) Console.WriteLine("Estrutura do Curso criada");
            else Console.WriteLine("ATENÇÂO: Estrutura do Curso NÃO criada");
        }

        private void RemoverUCDeCurso()
        {
            throw new NotImplementedException();
        }
        
//dbo.p_inserirUCInCurso @param_sig_uc varchar(6), @param_num_cred int, @param_descr text, @param_sig_curs varchar(6), @param_semestre int
        private void InserirUCEmCurso()
        {
            string command = "p_inserirUCemCurso";
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
            
            SqlParameter[] parameters = new SqlParameter[5];
            parameters[0] = new SqlParameter("@param_sig_uc", sig_uc);
            parameters[1] = new SqlParameter("@param_num_cred", num_cred);
            parameters[2] = new SqlParameter("@param_descr", descr);
            parameters[3] = new SqlParameter("@param_sig_curs", sig_un_curs);
            parameters[4] = new SqlParameter("@param_semestre", semestre);

            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            if (ret != -1) Console.WriteLine("Unidade Curricular inserida no Curso ");
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
            string command = "p_atualizarSeccao";
            Console.WriteLine("Inserir SIGLA ÚNICA da Secção");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO da Secção");
            string descr = Console.ReadLine();
            Console.WriteLine("Inserir SIGLA ÚNICA do Departamento onde a secção se encontra inserida");
            string sig_un_dep = Console.ReadLine();
            
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@param_sig_un", sig_un);
            parameters[1] = new SqlParameter("@param_descr", descr);
            parameters[2] = new SqlParameter("@param_sig_dep", sig_un_dep);

            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            if (ret != -1) Console.WriteLine("Secção atualizada");
            else Console.WriteLine("ATENÇÂO: Secção NÂO atualizada");   
        }

        private void RemoverSeccao()
        {
            string command = "p_removerSeccao";
            Console.WriteLine("Inserir SIGLA ÚNICA da Secção");
            string sig_un = Console.ReadLine();
            
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@param_sig_un", sig_un);
            
            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            if (ret != -1) Console.WriteLine("Secção removida");
            else Console.WriteLine("ATENÇÂO: Secção NÂO removida"); 
        }

        private void InserirSeccao()
        {
            string command = "p_inserirSeccao";
            Console.WriteLine("Inserir SIGLA ÚNICA da Secção");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO da Secção");
            string descr = Console.ReadLine();
            Console.WriteLine("Inserir SIGLA ÚNICA do Departamento onde a secção se encontra inserida");
            string sig_un_dep = Console.ReadLine();
            
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@param_sig_un", sig_un);
            parameters[1] = new SqlParameter("@param_descr", descr);
            parameters[2] = new SqlParameter("@param_sig_dep", sig_un_dep);

            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            if (ret != -1) Console.WriteLine("Secção inserida");
            else Console.WriteLine("ATENÇÂO: Secção NÂO inserida");   
        }

        private void AtualizarDepartamento()
        {
            string command = "p_atualizarDepartamento";
            Console.WriteLine("Inserir SIGLA ÚNICA do departemento");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO do departemento");
            string descr = Console.ReadLine();
            
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@param_sig_un", sig_un);
            parameters[1] = new SqlParameter("@param_descr", descr);

            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            if (ret != -1) Console.WriteLine("Departamento atualizado");
            else Console.WriteLine("ATENÇÂO: Departamento NÂO atualizado");            
        }

        private void RemoverDepartamento()
        {
            string command = "p_removerDepartamento";
            Console.WriteLine("Inserir SIGLA ÚNICA do departemento");
            string sig_un = Console.ReadLine();
            
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@param_sig_un", sig_un);
            
            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
     
            if (ret != -1) Console.WriteLine("Departamento removido");
            else Console.WriteLine("ATENÇÂO: Departamento NÂO removido");
        }

        private void InserirDepartamento()
        {
            /***
             * Lê valores a inserir
             ***/ 
            string command = "p_inserirDepartemento";
            Console.WriteLine("Inserir SIGLA UNICA do departamento:\n");
            string sig_un = Console.ReadLine();
            Console.WriteLine("Inserir DESCRIÇÃO do departamento:\n");
            string descr = Console.ReadLine();
            
            /***
             * Cria Parâmetros para passar no procedimento armazenado
             ***/
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@param_sig_un", sig_un);
            parameters[1] = new SqlParameter("@param_descr", descr);
            
            /***
             * Executa o procedimento armazenado
             ***/
            int ret = ConnectionGate.ExecuteStoredProcedure(command, parameters);
            
            /***
             * Vê op valor de retorno e age de acordo
             ***/
            if (ret != -1) Console.WriteLine("Departamento inserido");
            else Console.WriteLine("ATENÇÂO: Departamento NÂO inserido");
            
        }
    }
}