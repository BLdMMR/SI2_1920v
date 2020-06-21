using System;

namespace Instituto.Entities
{
    public class Matricula
    {
        public int Num_Aluno { get; set; }
        public string Sig_Curs{ get; set; }
        public DateTime Data_Inic { get; set; }
        public DateTime Data_Conc { get; set; }
        public float Media { get; set; }
    }
}