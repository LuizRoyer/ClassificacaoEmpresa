using System;

namespace ApiControleEmpresa.Entities
{
    public class NotaFiscal
    {
        public int IdNotaFiscal { get; set; }
        public int Item { get; set; }
        public int IdEmpresa { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public DateTime Data { get; set; }
    }
}
