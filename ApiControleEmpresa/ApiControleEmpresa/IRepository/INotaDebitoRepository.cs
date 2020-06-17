using ApiControleEmpresa.Entities;

namespace ApiControleEmpresa.IRepository
{
    public interface INotaDebitoRepository
    {
        void Add( Debito obj);
        int GetValueDebito(int idEmpresa , int valor);
        int GetNextValue();
    }
}
