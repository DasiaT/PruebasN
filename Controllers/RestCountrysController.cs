
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NinjaTalentCountrys.Functions;
using NinjaTalentCountrys.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Security.Policy;

namespace NinjaTalentCountrys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestCountrysController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public RestCountrysController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        //API PARA TRAER TODOS PAISES
        [HttpGet]
        [Route("/api/GetCountriesAll")]
        //[ProducesResponseType(typeof(Country), 200)]
        
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var client = _clientFactory.CreateClient();

                HttpResponseMessage response = await client.GetAsync("https://restcountries.com/v3.1/all");

                if (response.IsSuccessStatusCode)
                {
                    var countries = await response.Content.ReadFromJsonAsync<Country[]>();

                    return Ok(countries);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error en la solicitud a la API.");
                }
            }
            catch (Exception errorMessage) {

                return BadRequest("Mensaje de servidor de api:  " + errorMessage.Message);

            }
        }

        //API PARA TRAER LOS PAISES POR NOMBRES
        [HttpGet]
        [Route("/api/GetCountriesByName/{Name}")]
        public async Task<IActionResult> GetCountriesByName(string Name)
        {
            try
            {
                //PARA VALIDAR QUE NO VENGA VACIO O CON ESPACIOS
                if (Name == "" || Name == " ")
                {
                    return BadRequest("El nombre no debe ir vacio ");
                }
                //PARA VALIDAR QUE NO SEA NUMEROS
                var resultado = double.TryParse(Name, out double result);

                if (resultado)
                {
                    return BadRequest("El nombre no puede ser un numero.");
                }

                var client = _clientFactory.CreateClient();

                HttpResponseMessage response = await client.GetAsync("https://restcountries.com/v3.1/name/" + Name);
                
                //SI LA RESPUESTA ES CORRECTA
                if (response.IsSuccessStatusCode)
                {
                    var countries = await response.Content.ReadFromJsonAsync<Country[]>();

                    return Ok(countries);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "No se encontro el pais con ese nombre!");
                }
            }
            catch (Exception errorMessage)
            {

                return Problem("Mensaje de servidor de api:  " + errorMessage.Message);
            }
        }

        //CONVERTIR SVG A BASE64 LA IMAGEN DE LA URL
        [HttpGet]
        [Route("/api/GetCountriesByNameSvgToBase64/{Name}")]
        public async Task<IActionResult> GetCountriesSvgToBase64(string Name)
        {
            try
            {
                //PARA VALIDAR QUE NO VENGA VACIO O CON ESPACIOS
                if (Name == "" || Name == " ")
                {
                    return BadRequest("El nombre no debe ir vacio ");
                }
                //PARA VALIDAR QUE NO SEA NUMEROS
                var resultado = double.TryParse(Name, out double result);

                if (resultado)
                {
                    return BadRequest("El nombre no puede ser un numero.");
                }

                var client = _clientFactory.CreateClient();

                HttpResponseMessage response = await client.GetAsync("https://restcountries.com/v3.1/name/" + Name);
                
                //SI LA RESPUESTA ES CORRECTA
                if (response.IsSuccessStatusCode)
                {
                    var countries = await response.Content.ReadFromJsonAsync<Country[]>();

                    string Svg = countries[0].Flags.Svg.ToString();

                    SvgToBase64 valores = new SvgToBase64();

                    string banderaConvertida = await valores.ConvertSvgUrlToBase64Async(Svg);

                    return Ok(new { Pais = Name, imagen = banderaConvertida });
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "No se encontro el pais con ese nombre!");
                }
            }
            catch (Exception errorMessage)
            {

                return Problem("Mensaje de servidor de api:  " + errorMessage.Message);
            }
        }



        //POST que permita transferir la información de un país (Country) del api al modelo del microservicio para almacenarlo
        [HttpPost]
        [Route("/api/PostCountriesByName/{Name}")]
        public async Task<IActionResult> PostCountriesByName(string Name)
        {
            try
            {
                //PARA VALIDAR QUE NO VENGA VACIO O CON ESPACIOS
                if (Name == "" || Name == " ")
                {
                    return BadRequest("El nombre no debe ir vacio ");
                }
                //PARA VALIDAR QUE NO SEA NUMEROS
                var resultado = double.TryParse(Name, out double result);

                if (resultado)
                {
                    return BadRequest("El nombre no puede ser un numero.");
                }

                var client = _clientFactory.CreateClient();

                HttpResponseMessage response = await client.GetAsync("https://restcountries.com/v3.1/name/" + Name);

                //SI LA RESPUESTA ES CORRECTA
                if (response.IsSuccessStatusCode)
                {
                    var countries = await response.Content.ReadFromJsonAsync<Country[]>();

                    var datosParaAlmalcenar = new
                    {
                        name = countries[0].Name.Common.ToString(),
                        alpha2code = countries[0].Cca2.ToString(),
                        alpha3code = countries[0].Cca3.ToString(),
                        capital = countries[0].Capital,
                        region = countries[0].Region.ToString(),
                        nativename = new string[] { countries[0].Name.Official.ToString(), countries[0].Name.Common.ToString() }
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(datosParaAlmalcenar);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var responseBase = await client.PostAsync("http://www.pruebasn.somee.com/api/Country", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return Ok(json);
                    }
                    else
                    {
                        return Problem("Hubo un problema al agregar el país");
                    }
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "No se encontro el pais con ese nombre!");
                }
            }
            catch (Exception errorMessage)
            {

                return Problem("Mensaje de servidor de api:  " + errorMessage.Message);
            }
        }

    }
}
