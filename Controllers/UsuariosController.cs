using Delivery_Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NinjaTalentCountrys.Interfaces;
using NinjaTalentCountrys.Models;

namespace NinjaTalentCountrys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : Controller
    {
        private readonly InterfacesDBContext _context;
        private readonly IUser _user;

        public UsuariosController(InterfacesDBContext context, IUser user)
        {
            _context = context;
            _user = user;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsuarios([FromQuery] UserModel userModel)
        {
            // Verificar si existen registros en la tabla UsuariosModel
            if (!_context.UsuariosModel.Any())
            {
                return StatusCode(500, new { message = "No se encontro datos en base de datos.", error_code = 500, status_code = 1 });
            }

            // Realizar la consulta para buscar el usuario por nombre de usuario y contraseña
            var Result = await _user.GetUserAsync(userModel);

            // Verificar si se encontró un usuario
            if (Result.isError)
            {
                var ex = Result.error;

                return StatusCode(ex.StatusCode, new { message = ex.Message, error_code = ex.ErrorCode, status_code = ex.StatusCode });
            }

            return Ok(Result.result);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<UserModel>>> PostUser([FromBody] UserModel userModel)
        {
            // Verificar si existen registros en la tabla UsuariosModel
            if (!_context.UsuariosModel.Any())
            {
                return StatusCode(500, new { message = "No se encontro datos en base de datos.", error_code = 500, status_code = 1 });
            }

            // Realizar la consulta para buscar el usuario por nombre de usuario y contraseña
            var Result = await _user.PostUserAsync(userModel);

            // Verificar si se encontró un usuario
            if (Result.isError)
            {
                var ex = Result.error;

                return StatusCode(ex.StatusCode, new { message = ex.Message, error_code = ex.ErrorCode, status_code = ex.StatusCode });
            }

            return Ok(Result.result);
        }

        [HttpPatch("User/{id}")]
        public async Task<ActionResult<IEnumerable<UserModel>>> PatchUser(int id, [FromBody] UserModel userModel)
        {
            // Verificar si existen registros en la tabla UsuariosModel
            if (!_context.UsuariosModel.Any())
            {
                return StatusCode(500, new { message = "No se encontro datos en base de datos.", error_code = 500, status_code = 1 });
            }

            // Realizar la consulta para buscar el usuario por nombre de usuario y contraseña
            var Result = await _user.PatchUserAsync(id,userModel);

            // Verificar si se encontró un usuario
            if (Result.isError)
            {
                var ex = Result.error;

                return StatusCode(ex.StatusCode, new { message = ex.Message, error_code = ex.ErrorCode, status_code = ex.StatusCode });
            }

            return Ok(Result.result);
        }

    }
}
