namespace ApiControleEmpresa.Entities
{
    public class Notas
    {
        public int IdEmpresa { get; set; }
        public NotaItem[] ListaNotas { get; set; }
        public int TipoNota { get; set; }    

    }
}
