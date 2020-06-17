using ApiControleEmpresa.Entities;

namespace ApiControleEmpresa.IRepository
{
    public interface INotaFiscalRepository
    {
        void Add(NotaFiscal obj);
        int GetValueNotaFiscal(int idEmpresa);
        int GetNextValue();
    }
}
