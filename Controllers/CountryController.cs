using Delivery_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaTalentCountrys.Functions;
using NinjaTalentCountrys.Models;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            if (_context.CountryModel == null)
            {
                return NotFound("No se encontro registros en base de datos");
            }
            
            //CONSULTA GET PARA OBTENER TODOS LOS PAISES
            List<CountryModel> resultado = await _context.CountryModel.ToListAsync();
            
            //COMPRUEBA SI TIENE REGISTROS
            if (resultado.Count == 0)
            {
                return NotFound("No se encontro ningun pais agregado a la base de datos.");
            }

            return resultado;
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountry(string Id)
        {
            

            if (_context.CountryModel == null)
            {
                return NotFound("No se encontro registros de paises.");
            }

            //CONSULTA GET PARA OBTENER TODOS LOS PAISES
            List<CountryModel> resultado = await _context.CountryModel.Where(x => x.id.ToString() == Id).ToListAsync();

            //COMPRUEBA SI TIENE REGISTROS
            if (resultado.Count == 0)
            {
                return NotFound("No se encontro registros el pais con ese codigo.");
            }

            return resultado;
        }

        [HttpPost]
        public async Task<ActionResult<CountryModel>> PostCountry(CountryModel country)
        {
            try
            {

                if (_context.CountryModel == null)
                {
                    return NotFound("No se encontro registro de la tabla.");
                }

                var ValidoExistencias = await _context.CountryModel.FirstOrDefaultAsync(x => x.name == country.name);


                if (ValidoExistencias != null)
                {
                    return BadRequest("Ya existe el pais que deseas ingresar.");

                }
               
                _context.CountryModel.Add(new CountryModel()
                {
                    name = country.name,
                    alpha2code = country.alpha2code,
                    alpha3code = country.alpha3code,
                    capital = country.capital,
                    region = country.region,
                    nativename = country.nativename
                });

                await _context.SaveChangesAsync();

                var ValidoExistenciasGuardo = await _context.CountryModel.FirstOrDefaultAsync(x => x.name == country.name);

                return CreatedAtAction("GetCountry", new { id = ValidoExistenciasGuardo.id }, ValidoExistenciasGuardo);

                
            }
            catch(Exception error) {

                return Problem("Error en el servidor: " + error.Message);
            }

        }


        [HttpPatch("{id}")]
        public async Task<ActionResult<CountryModel>> PatchCountry(int id, [FromBody] CountryModel updatedCountry)
        {
            try
            {
                var validarExistencia = await _context.CountryModel.FirstOrDefaultAsync(x => x.id == id);

                if (validarExistencia == null)
                {
                    return NotFound("No se encontró el país en la base de datos.");
                }

                //Actualizar campos que vayan llenos
                if (!string.IsNullOrEmpty(updatedCountry.name))
                {
                    validarExistencia.name = updatedCountry.name;
                }

                if (!string.IsNullOrEmpty(updatedCountry.alpha2code))
                {
                    validarExistencia.alpha2code = updatedCountry.alpha2code;
                }

                if (!string.IsNullOrEmpty(updatedCountry.alpha3code))
                {
                    validarExistencia.alpha3code = updatedCountry.alpha3code;
                }

                if (!string.IsNullOrEmpty(updatedCountry.capital.ToString()))
                {
                    validarExistencia.capital = updatedCountry.capital;
                }

                if (!string.IsNullOrEmpty(updatedCountry.region))
                {
                    validarExistencia.region = updatedCountry.region;
                }


                if (!string.IsNullOrEmpty(updatedCountry.nativename.ToString()))
                {
                    validarExistencia.nativename = updatedCountry.nativename;
                }

                await _context.SaveChangesAsync();

                return Ok(validarExistencia);

            }
            catch (Exception error)
            {
                return Problem("Error en el servidor: " + error.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<CountryModel>> DeleteCountry(string id)
        {
            try
            {
                if (_context.CountryModel == null)
                {
                    return NotFound("No se encontró ningun país en la base de datos.");
                }
                var delivery_Categoria = await _context.CountryModel.FirstOrDefaultAsync(x => x.id.ToString() == id);

                if (delivery_Categoria == null)
                {
                    return NotFound("No se encontró el país en la base de datos.");
                }

                _context.CountryModel.Remove(delivery_Categoria);

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception error)
            {
                return Problem("Error en el servidor: " + error.Message);
            }
        }


    }
}
