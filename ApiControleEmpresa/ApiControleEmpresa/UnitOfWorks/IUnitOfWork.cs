using ApiControleEmpresa.IRepository;

namespace ApiControleEmpresa.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IEmpresaRepository EmpresaRepository();
        INotaDebitoRepository NotaDebitoRepository();
        INotaFiscalRepository NotaFiscalRepository();
        void Commit();
        void Rollback();
    }
}
