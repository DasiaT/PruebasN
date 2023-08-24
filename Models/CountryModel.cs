using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace NinjaTalentCountrys.Models
{
    public class CountryModel
    {
        [Key]
        public Int64? id { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [DataType(DataType.Text, ErrorMessage = "El nombre debe ser de tipo texto.")]
        public string? name { get; set; }

        [Required(ErrorMessage = "El campo Alpha2Code es requerido.")]
        [DataType(DataType.Text, ErrorMessage = "El Alpha2Code debe ser de tipo texto.")]
        [StringLength(2, MinimumLength = 2, ErrorMessage = "El campo Alpha2Code debe tener exactamente 2 caracteres.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "El campo Alpha2Code solo debe contener letras.")]
        public string? alpha2code { get; set; }

        [Required(ErrorMessage = "El campo Alpha3Code es requerido.")]
        [DataType(DataType.Text, ErrorMessage = "El Alpha3Code debe ser de tipo texto.")]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "El campo Alpha3Code debe tener exactamente 2 caracteres.")]
        [RegularExpression("^[A-Za-z]+$", ErrorMessage = "El campo Alpha3Code solo debe contener letras.")]
        public string? alpha3code { get; set; }

        [Required(ErrorMessage = "El campo Capital es requerido.")]
        [DataType(DataType.Text, ErrorMessage = "El Capital debe ser de tipo texto.")]
        public string[]? capital { get; set; }

        [Required(ErrorMessage = "El campo Region es requerido.")]
        [DataType(DataType.Text, ErrorMessage = "El Region debe ser de tipo texto.")]
        public string? region { get; set; }

        [Required(ErrorMessage = "El campo NativeName es requerido.")]
        [DataType(DataType.Text, ErrorMessage = "El NativeName debe ser de tipo texto.")]
        public string[]? nativename { get; set; }

        

    }
}
