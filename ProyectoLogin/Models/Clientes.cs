using System.ComponentModel.DataAnnotations;

namespace ProyectoLogin.Models
{
    public class Clientes
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string NameCard { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CardNumber { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string ExpMonth { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int ExpYear { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CVV { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Produc { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int Price { get; set; }
    }
}
