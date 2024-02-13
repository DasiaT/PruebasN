using NinjaTalentCountrys.Interfaces;

namespace NinjaTalentCountrys.Services
{
    public class errorServices : Exception
    {
        public int StatusCode { get; set; }
        public int ErrorCode { get; set; }

        public errorServices(string message, int statusCode, int errorCode) : base(message)
        {

            StatusCode = statusCode;

            ErrorCode = errorCode;
        }

    }

    public class ErrorService : IError
    {
        public errorServices getBadRequestException(string message, int errorCode)
        {

            return new errorServices(message, StatusCodes.Status400BadRequest, errorCode);
        }
    }
}