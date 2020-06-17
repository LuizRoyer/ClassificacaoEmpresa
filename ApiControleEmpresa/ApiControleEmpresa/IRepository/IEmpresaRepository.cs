using ApiControleEmpresa.Entities;
using System.Collections.Generic;

namespace ApiControleEmpresa.IRepository
{
    public interface IEmpresaRepository
    {
        List<Empresa> GetAll();
    }
}
