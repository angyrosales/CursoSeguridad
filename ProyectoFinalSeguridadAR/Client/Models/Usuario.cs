using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalSeguridadAR.Client.Models
{
    public class Usuario
    {
        [Required]
        [MinLength(2, ErrorMessage = "Nombre Invalido")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜ\s]+$", ErrorMessage = "El nombre solo puede contener letras.")]
        public string Nombre { get; set; } = string.Empty;
        [Range(18, 80)]
        public int Edad { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Display(Name = "Confirmar Email")]
        [Required]
        [EmailAddress]
        [Compare(nameof(Email),ErrorMessage ="No coincide con el Email")]
        public string RepetirEmail { get; set; } = string.Empty;
        [MinLength(10, ErrorMessage = "Número Invalido. Debe tener al menos 10 caracteres.")]
        [MaxLength(13, ErrorMessage = "Número Invalido. No debe exceder los 13 caracteres.")]
        [Phone(ErrorMessage ="Número de Telefomo invalido")]
        public string Telefomo { get; set; } = string.Empty;
        [Required]
        [RegularExpression("^[0-9*]{16}$", ErrorMessage = "Formato incorrecto")]
        public string Tarjeta { get; set; } = string.Empty;
    }
}
