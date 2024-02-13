using Delivery_Backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NinjaTalentCountrys.Interfaces;
using NinjaTalentCountrys.Models;
using System.ComponentModel;

namespace NinjaTalentCountrys.Services.User
{

    public class UserServices : IUser
    {
        private readonly InterfacesDBContext _context;
        private readonly ErrorService _errorService;
        private readonly UserValidation _userValidation;
        private readonly UserValidationExist _userValidationExist;
        private readonly ILogger<UserServices> _logger;
        private readonly UserValidationAdd _userValidationAdd;
        private readonly UserValidationFilter _userValidationFilter;
        private readonly UserList _userList;
        private readonly UserValidationExistUserName _userValidationExistUserName;
        public UserServices(InterfacesDBContext context, ErrorService error, UserValidation userValidation, UserValidationExist validationExist, 
            ILogger<UserServices> logger, UserValidationAdd userValidationAdd,  UserValidationFilter userValidationFilter, UserList userList,
            UserValidationExistUserName userValidationExistUserName)
        {
            _context = context;
            _errorService = error;
            _userValidation = userValidation;   
            _userValidationExist = validationExist;
            _logger = logger;
            _userValidationAdd = userValidationAdd;
            _userValidationFilter = userValidationFilter;
            _userList = userList;
            _userValidationExistUserName = userValidationExistUserName;
        }

        public async Task<(bool isError, errorServices? error, UserResponse? result)> GetUserAsync(UserModel value)
        {
            bool BaseNull = !_context.UsuariosModel.Any();

            if (BaseNull)
            {
                errorServices errorServices = _errorService.getBadRequestException("No hay datos de usuario en base de datos.", 1);

                return (true, errorServices, null);
            }
            
            bool validationfilters = _userValidationFilter.UserValidationsFilter(value);

            if (!validationfilters)
            {
                List<UserModel> empleados = await _context.UsuariosModel.ToListAsync();

                UserResponse? results = new();

                if (empleados != null)
                {
                    results.Result = empleados;
                    results.Count = empleados.Count();
                }

                return (false, null, results);
            }

            var empleado = await _userList.UsersList(value);

            UserResponse? result = new();

            if (empleado != null)
            {
                result.Result = empleado;
                result.Count = empleado.Count;
            }


            return (false, null, result);
        }

        public async Task<(bool isError, errorServices? error, UserResponse? result)> PostUserAsync(UserModel value)
        {
            bool BaseNull = !_context.UsuariosModel.Any();

            if (BaseNull)
            {
                errorServices errorServices = _errorService.getBadRequestException("No hay datos de usuario en base de datos.", 1);

                return (true, errorServices, null);
            }

            bool validation = _userValidation.UserValidations(value);

            if (!validation) 
            {
                errorServices errorServices = _errorService.getBadRequestException("Ingrese contraseña y usuario para continuar.", 1);

                return (true, errorServices, null);
            }

            if (value.Name != null)
            {
                bool  validationAdd = _userValidationAdd.UserValidationsAdd(value);
                
                if (!validationAdd)
                {
                    errorServices errorServices = _errorService.getBadRequestException("Para crear un usuario debe agregar un nombre, usuario y contraseña.", 3);

                    return (true, errorServices, null);
                }
                bool validationExistUserName = _userValidationExistUserName.UserValidationsExistUserName(value);
                
                if (validationExistUserName) 
                {
                    errorServices errorServices = _errorService.getBadRequestException("El usuario que quieres crear ya existe.", 4);

                    return (true, errorServices, null);
                }
                _context.UsuariosModel.Add(value);

                _context.SaveChanges();

            }

            bool validationExist = _userValidationExist.UserValidationsExist(value);

            if (!validationExist)
            {
                errorServices errorServices = _errorService.getBadRequestException("Usuario o contraseña no validos.", 2);

                return (true, errorServices, null);
            }

            var empleado = await _context.UsuariosModel.Where(x => x.Username == value.Username && x.Password == value.Password).ToListAsync();

            UserResponse? result = new();

            if (empleado != null) {
                result.Result = empleado;
                result.Count = empleado.Count;
            }
            

            return (false, null, result);
        }

        public async Task<(bool isError, errorServices? error, UserResponse? result)> PatchUserAsync(int id, UserModel value)
        {
            bool BaseNull = !_context.UsuariosModel.Any();

            if (BaseNull)
            {
                errorServices errorServices = _errorService.getBadRequestException("No hay datos de usuario en base de datos.", 1);

                return (true, errorServices, null);
            }

            bool validation = _userValidation.UserValidations(value);

            if (!validation)
            {
                errorServices errorServices = _errorService.getBadRequestException("Ingrese contraseña y usuario para continuar.", 1);

                return (true, errorServices, null);
            }

            if (value.Name != null)
            {
                bool validationAdd = _userValidationAdd.UserValidationsAdd(value);

                if (!validationAdd)
                {
                    errorServices errorServices = _errorService.getBadRequestException("Para crear un usuario debe agregar un nombre, usuario y contraseña.", 3);

                    return (true, errorServices, null);
                }

                var UserSearch = await _context.UsuariosModel.FirstOrDefaultAsync(x => x.Username == value.Username);
                
                if (UserSearch != null)
                {
                    if (value?.Name != null && UserSearch?.Name != null)
                    {
                        UserSearch.Name = value.Name;
                    }

                    if (value?.Password != null && UserSearch?.Password != null)
                    {
                        UserSearch.Password = value.Password;
                    }

                    _context.UsuariosModel.Update(UserSearch);

                    _context.SaveChanges();
                }
            }

            var empleado = await _context.UsuariosModel.Where(x => x.Username == value.Username && x.Password == value.Password).ToListAsync();

            UserResponse? result = new();

            if (empleado != null)
            {
                result.Result = empleado;
                result.Count = empleado.Count;
            }


            return (false, null, result);
        }
    }
}
