using NinjaTalentCountrys.Services;

namespace NinjaTalentCountrys.Interfaces
{
    public interface IError
    {
        errorServices getBadRequestException(string message, int errorCode);
    }
}
