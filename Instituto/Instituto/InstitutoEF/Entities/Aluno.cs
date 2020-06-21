using System;

namespace Instituto.Entities
{
    public class Aluno
    {
        public int Num_Aluno { get; set; }
        public string Num_CC { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string Cod_Post { get; set; }
        public string Localidade { get; set; }
        public DateTime Data_Nasc { get; set; }
    }
}