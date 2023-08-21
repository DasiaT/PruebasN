using Delivery_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaTalentCountrys.Models;
using System.Diagnostics.Metrics;

namespace NinjaTalentCountrys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly InterfacesDBContext _context;

        public CountryController(InterfacesDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountry()
        {

            if (_context.Country == null)
            {
                return NotFound("No se encontro registros");
            }
            //CONSULTA GET PARA OBTENER TODOS LOS PAISES
            List<CountryModel> resultado = await _context.Country.ToListAsync();
            //COMPRUEBA SI TIENE REGISTROS
            if (resultado.Count == 0)
            {
                return NotFound("No se encontro registros");
            }

            return resultado;
        }
    }
}
