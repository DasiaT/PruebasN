using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace NinjaTalentCountrys.Models
{
    public class CountryModel
    {
        [Key]
        public Int64 id { get; set; }
        public string? name { get; set; }
        //public string? Alpha2Code { get; set; }
        //public string? Alpha3Code { get; set; }
        //public string? Capital { get; set; }
        //public string? Region { get; set; }
        //public string? NativeName { get; set; }
       
    }
}
