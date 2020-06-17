using ApiControleEmpresa.Entities;
using ApiControleEmpresa.Service;
using ApiControleEmpresa.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiControleEmpresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {

        [HttpPost("[action]")]
        public IActionResult SaveNotas([FromBody]Notas Notas,
           [FromServices] IUnitOfWork unitOfWork)
        {
            return new EmpresaService().Save(Notas, unitOfWork);
        }
        [HttpGet("[action]")]
        public List<Empresa> SelectEmpresas(
         [FromServices] IUnitOfWork unitOfWork)
        {
            return new EmpresaService().SelecionarEmpresas(unitOfWork);
        }
    }
}