using NinjaTalentCountrys.Models;
using NinjaTalentCountrys.Services;

namespace NinjaTalentCountrys.Interfaces
{
    public interface IUser
    {
        Task<(bool isError, errorServices? error, UserResponse? result)> GetUserAsync(UserModel value);
        Task<(bool isError, errorServices? error, UserResponse? result)> PostUserAsync(UserModel value);
        Task<(bool isError, errorServices? error, UserResponse? result)> PatchUserAsync(int id, UserModel value);
    }
}
