﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Instituto
{
    public class Menu
    {
        private enum Options
        {
            Invalid = -1,
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
            AtribuirNotaDeUCaAlunoEmAno
        }

        private delegate void Method();

        private Dictionary<Options, Method> menuOptions;

        public Menu()
        {
            ConnectionGate.SetConnection();
            Console.WriteLine(ConnectionGate.TestConnection());
            /*
            Method aux = () =>
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            };
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
            menuOptions.Add(Options.AtribuirNotaDeUCaAlunoEmAno, AtribuirNotaDeUCaAlunoEmAno + aux);*/
            
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