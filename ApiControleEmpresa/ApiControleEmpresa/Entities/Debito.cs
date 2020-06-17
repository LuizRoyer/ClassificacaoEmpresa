using System;

namespace ApiControleEmpresa.Entities
{
    public class Debito
    {       
        public int IdDebito { get; set; }
        public int Item { get; set; }
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public DateTime Data { get; set; }
    }
}
