
using System.Data.Entity;
using Instituto.Entities;


namespace InstitutoEF
{
    public partial class Entities : DbContext {
        public Entities() 
            : base("name=Entities") {
        }
        
        protected virtual DbSet<Matricula> Matricula { get; set; }
        protected virtual DbSet<MatriculaAlunoEmAno> MatriculaAlunoEmAno { get; set; }
        protected virtual DbSet<Inscricao> Inscricao { get; set; }
        protected virtual DbSet<Ensina> Ensina { get; set; }
        protected virtual DbSet<Ano> Ano { get; set; }
        protected virtual DbSet<UCdeCurso> UCdeCurso { get; set; }
        protected virtual DbSet<UC> UC { get; set; }
        protected virtual DbSet<Professor> Professor { get; set; }
        protected virtual DbSet<Aluno> Aluno { get; set; }
        protected virtual DbSet<Curso> Curso { get; set; }
        protected virtual DbSet<Seccao> Seccao { get; set; }
        protected virtual DbSet<Departamento> Departamento { get; set; }
        
        
        public virtual int 
    }
}