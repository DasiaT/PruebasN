
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace NinjaTalentCountrys.Models
{
    public class UserModel
    {
        public Int64 Id { get; set; }
        public string? Username { get; set; }
        
        //[JsonIgnore]//PARA QUE NO SE INCLUYA EN LA RESPUESTA DEL JSON 
        public string? Password { get; set; }
        public string? Name { get; set; }

    }

    public class UserResponse
    {
        public List<UserModel>? Result { get; set; }
        public int Count { get; set; }
    }
}
