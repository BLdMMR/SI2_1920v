﻿
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Mime;
using Instituto.Entities;


namespace InstitutoEF
{
    public partial class Entities : DbContext
    {
        static string Bernardo =
            "data source=DESKTOP-DCAMG6R\\MSSQLSERVER01;initial catalog=Instituto;trusted_connection=true";
        static string BernardoDesktop = 
            "data source=DESKTOP-M1V057N;initial catalog=Instituto;trusted_connection=true";
        public Entities()
            : base(BernardoDesktop)
        {
        }

        public virtual DbSet<Matricula> Matricula { get; set; }
        public virtual DbSet<MatriculaAlunoEmAno> MatriculaAlunoEmAno { get; set; }
        public virtual DbSet<Inscricao> Inscricao { get; set; }
        public virtual DbSet<Ensina> Ensina { get; set; }
        public virtual DbSet<Ano> Ano { get; set; }
        public virtual DbSet<UCdeCurso> UCdeCurso { get; set; }
        public virtual DbSet<UC> UC { get; set; }
        public virtual DbSet<Professor> Professor { get; set; }
        public virtual DbSet<Aluno> Aluno { get; set; }
        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Seccao> Seccao { get; set; }
        public virtual DbSet<Departamento> Departamento { get; set; }

        public virtual int p_inserirDepartamento(string param_sig_un, string param_descr)
        {
            ObjectParameter sig_un = param_sig_un != null
                ? new ObjectParameter("param_sig_un", param_sig_un)
                : new ObjectParameter("param_sig_un", typeof(string));
            ObjectParameter descr = param_descr != null
                ? new ObjectParameter("param_descr", param_descr)
                : new ObjectParameter("param_descr", typeof(string));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_inserirDepartamento", sig_un, descr);
        }

        public virtual int p_removerDepartamento(string param_sig_un)
        {
            ObjectParameter sig_un = param_sig_un != null
                ? new ObjectParameter("param_sig_un", param_sig_un)
                : new ObjectParameter("param_sig_un", typeof(string));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_removerDepartamento", sig_un);
        }

        public virtual int p_atualizarDepartamento(string param_sig_un, string param_descr)
        {
            ObjectParameter sig_un = param_sig_un != null
                ? new ObjectParameter("param_sig_un", param_sig_un)
                : new ObjectParameter("param_sig_un", typeof(string));
            ObjectParameter descr = param_descr != null
                ? new ObjectParameter("param_descr", param_descr)
                : new ObjectParameter("param_descr", typeof(string));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_atualizarDepartamento", sig_un,
                descr);
        }

        public virtual int p_inserirSeccao(string param_sig_un, string param_descr, string param_sig_dep)
        {
            ObjectParameter sig_un = param_sig_un != null
                ? new ObjectParameter("param_sig_un", param_sig_un)
                : new ObjectParameter("param_sig_un", typeof(string));
            ObjectParameter descr = param_descr != null
                ? new ObjectParameter("param_descr", param_descr)
                : new ObjectParameter("param_descr", typeof(string));
            ObjectParameter sig_dep = param_sig_dep != null
                ? new ObjectParameter("param_sig_dep", param_sig_dep)
                : new ObjectParameter("param_sig_dep", typeof(string));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_inserirSeccao", sig_un, descr,
                sig_dep);
        }

        public virtual int p_removerSeccao(string param_sig_un)
        {
            ObjectParameter sig_un = param_sig_un != null
                ? new ObjectParameter("param_sig_un", param_sig_un)
                : new ObjectParameter("param_sig_un", typeof(string));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_removerSeccao", sig_un);
        }

        public virtual int p_atualizarSeccao(string param_sig_un, string param_descr, string param_sig_dep)
        {
            ObjectParameter sig_un = param_sig_un != null
                ? new ObjectParameter("param_sig_un", param_sig_un)
                : new ObjectParameter("param_sig_un", typeof(string));
            ObjectParameter descr = param_descr != null
                ? new ObjectParameter("param_descr", param_descr)
                : new ObjectParameter("param_descr", typeof(string));
            ObjectParameter sig_dep = param_sig_dep != null
                ? new ObjectParameter("param_sig_dep", param_sig_dep)
                : new ObjectParameter("param_sig_dep", typeof(string));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_atualizarSeccao", sig_un, descr,
                sig_dep);
        }

        public virtual int p_inserirUCEmCurso(string param_sig_uc, int param_num_cred, string param_descr,
            string param_sig_curs,
            int param_semestre)
        {
            ObjectParameter sig_uc = param_sig_uc != null
                ? new ObjectParameter("param_sig_uc", param_sig_uc)
                : new ObjectParameter("param_sig_uc", typeof(string));
            ObjectParameter num_cred = param_num_cred != -1
                ? new ObjectParameter("param_num_cred", param_num_cred)
                : new ObjectParameter("param_num_cred", typeof(int));
            ObjectParameter descr = param_descr != null
                ? new ObjectParameter("param_descr", param_descr)
                : new ObjectParameter("param_descr", typeof(string));
            ObjectParameter sig_curs = param_sig_curs != null
                ? new ObjectParameter("param_sig_dep", param_sig_curs)
                : new ObjectParameter("param_sig_dep", typeof(string));
            ObjectParameter semestre = param_semestre != -1
                ? new ObjectParameter("param_semestre", param_semestre)
                : new ObjectParameter("param_semestre", typeof(int));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_inserirUCemCurso", sig_uc, num_cred,
                descr, sig_curs, semestre);
        }

        public virtual int p_criarEstruturaDeCurso(string param_sig_un, string param_descr, string param_sig_dep,
            int param_num_anos)
        {
            ObjectParameter sig_un = param_sig_un != null
                ? new ObjectParameter("param_sig_un", param_sig_un)
                : new ObjectParameter("param_sig_un", typeof(string));
            ObjectParameter descr = param_descr != null
                ? new ObjectParameter("param_descr", param_descr)
                : new ObjectParameter("param_descr", typeof(string));
            ObjectParameter sig_dep = param_sig_dep != null
                ? new ObjectParameter("param_sig_dep", param_sig_dep)
                : new ObjectParameter("param_sig_dep", typeof(string));
            ObjectParameter num_anos = param_num_anos != -1
                ? new ObjectParameter("num_anos", param_num_anos)
                : new ObjectParameter("num_anos", typeof(int));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_criarCurso", sig_un, descr, sig_dep,
                num_anos);
        }

        public virtual int p_matricularAlunoEmCurso(string param_num_cc, string param_nome, string param_endereco,
            string param_cod_post, string param_localidade, DateTime param_data_nasc, string param_sig_curs)
        {
            ObjectParameter num_cc = param_num_cc != null
                ? new ObjectParameter("param_num_cc", param_num_cc)
                : new ObjectParameter("param_num_cc", typeof(string));
            ObjectParameter nome = param_nome != null
                ? new ObjectParameter("param_nome", param_nome)
                : new ObjectParameter("param_nome", typeof(string));
            ObjectParameter endereco = param_endereco != null
                ? new ObjectParameter("param_endereco", param_endereco)
                : new ObjectParameter("param_endereco", typeof(string));
            ObjectParameter sig_curs = param_sig_curs != null
                ? new ObjectParameter("param_sig_curs", param_sig_curs)
                : new ObjectParameter("param_sig_curs", typeof(string));
            ObjectParameter cod_post = param_cod_post != null
                ? new ObjectParameter("param_cod_post", param_cod_post)
                : new ObjectParameter("param_cod_post", typeof(string));
            ObjectParameter localidade = param_localidade != null
                ? new ObjectParameter("param_localidade", param_localidade)
                : new ObjectParameter("param_localidade", typeof(string));
            ObjectParameter data_nasc = param_data_nasc != DateTime.MinValue
                ? new ObjectParameter("param_data_nasc", param_data_nasc)
                : new ObjectParameter("param_data_nasc", typeof(DateTime));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_matricularAlunoEmCurso",
                num_cc, nome, endereco, cod_post, localidade, data_nasc, sig_curs);
        }

        public virtual int p_inscreverAlunoEmUC(int param_num_aluno, string param_sig_uc, int param_ano)
        {
            ObjectParameter num_aluno = param_num_aluno != -1
                ? new ObjectParameter("param_num_aluno", param_num_aluno)
                : new ObjectParameter("param_num_aluno", typeof(int));
            ObjectParameter sig_uc = param_sig_uc != null
                ? new ObjectParameter("param_sig_uc", param_sig_uc)
                : new ObjectParameter("param_sig_uc", typeof(string));
            ObjectParameter ano = param_ano != -1
                ? new ObjectParameter("num_anos", param_ano)
                : new ObjectParameter("num_anos", typeof(int));

            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_inscreverAlunoEmUC", num_aluno, ano,
                sig_uc);
        }

        public virtual int p_atribuirNotaAAlunoEmUC(int param_num_aluno, string param_sig_uc, int param_ano_insc, int param_nota)
        {
            ObjectParameter num_aluno = param_num_aluno != -1
                ? new ObjectParameter("param_num_aluno", param_num_aluno)
                : new ObjectParameter("param_num_aluno", typeof(int));
            ObjectParameter sig_uc = param_sig_uc != null
                ? new ObjectParameter("param_sig_uc", param_sig_uc)
                : new ObjectParameter("param_sig_uc", typeof(string));
            ObjectParameter ano_insc = param_ano_insc != -1
                ? new ObjectParameter("param_ano", param_ano_insc)
                : new ObjectParameter("param_ano", typeof(int));
            ObjectParameter nota = param_nota != -1
                ? new ObjectParameter("num_anos", param_nota)
                : new ObjectParameter("num_anos", typeof(int));
            return ((IObjectContextAdapter) this).ObjectContext.ExecuteFunction("p_atribuirNotaAAlunoEmUC", num_aluno,
                sig_uc, ano_insc, nota);
        }
        [DbFunction("Entities", "p_listarInscEmUCEmDetAno")]
        public virtual IQueryable<f_listaInsc_Result> p_listarInscEmUCEmDetAno(string param_sig_uc)
        {
            ObjectParameter sig_uc = param_sig_uc != null
                ? new ObjectParameter("param_sig_uc", param_sig_uc)
                : new ObjectParameter("param_sig_uc", typeof(string));
            return ((IObjectContextAdapter) this).ObjectContext.CreateQuery<f_listaInsc_Result>(
                "[Entities].[p_listarInscEmUCEmDetAno](@param_sig_uc)", sig_uc);
        }

        public virtual int p_eliminarAlunoETodaAInfo(int param_num_aluno)
        {
            return -1;
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

    }
}