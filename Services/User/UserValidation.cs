using Delivery_Backend.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NinjaTalentCountrys.Interfaces;
using NinjaTalentCountrys.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace NinjaTalentCountrys.Services.User
{
    
    public class UserValidation
    {
        public bool UserValidations(UserModel user)
        {
            bool validations = true;

            if (string.IsNullOrEmpty(user.Username)) 
            {
                validations = false;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                validations = false;
            }

            return validations;
        }
    }

    public class UserValidationExist
    {
        private readonly InterfacesDBContext _context;
        public UserValidationExist(InterfacesDBContext context)
        {
            _context = context;
        }
        public bool UserValidationsExist(UserModel user)
        {
            var empleado = _context.UsuariosModel.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            
            if (empleado == null)
            {
                return (false);
            }

            return true;
        }
    }


    public class UserValidationAdd
    {
        public bool UserValidationsAdd(UserModel user)
        {
            bool validations = true;

            if (string.IsNullOrEmpty(user.Username))
            {
                validations = false;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                validations = false;
            }
            if (string.IsNullOrEmpty(user.Name))
            {
                validations = false;
            }

            return validations;
        }
    }

    public class UserValidationFilter
    {
        public bool UserValidationsFilter(UserModel user)
        {
            bool validations = false;
            
            if (user.Id != 0)
            {
                validations = true;
            }
            if (!string.IsNullOrEmpty(user.Username))
            {
                validations = true;
            }
            if (!string.IsNullOrEmpty(user.Password))
            {
                validations = true;
            }
            if (!string.IsNullOrEmpty(user.Name))
            {
                validations = true;
            }

            return validations;
        }
    }


    public class UserList
    {
        private readonly InterfacesDBContext _context;
        public UserList(InterfacesDBContext context)
        {
            _context = context;
        }
        public async Task<List<UserModel>> UsersList( UserModel user)
        {
            if (user.Id != 0)
            {
                var empleados = await _context.UsuariosModel
                    .Where(x => x.Id == user.Id)
                    .ToListAsync();

                return empleados;
            }

            if (!string.IsNullOrEmpty(user.Username))
            {
                var empleados = await _context.UsuariosModel
                    .Where(x => x.Username.Contains(user.Username))
                    .ToListAsync();

                return empleados;
            }

            if (!string.IsNullOrEmpty(user.Name))
            {
                var empleados = await _context.UsuariosModel
                    .Where(x => x.Name.Contains(user.Name))
                    .ToListAsync();

                return empleados;
            }
           
            return new List<UserModel>();
        }
    }

    public class UserValidationExistUserName
    {
        private readonly InterfacesDBContext _context;
        public UserValidationExistUserName(InterfacesDBContext context)
        {
            _context = context;
        }
        public bool UserValidationsExistUserName(UserModel user)
        {
            var empleado = _context.UsuariosModel.FirstOrDefault(x => x.Username == user.Username);

            if (empleado == null)
            {
                return false;
            }

            return true;
        }
    }
}
