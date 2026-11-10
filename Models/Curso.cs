using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_MatamorosDiana.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El código del curso es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código no puede superar los 20 caracteres.")]
        [Display(Name = "Código")]
        public string Codigo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre del curso es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        [Display(Name = "Nombre del curso")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad de créditos es obligatoria.")]
        [Range(1, 10, ErrorMessage = "Los créditos deben estar entre 1 y 10.")]
        [Display(Name = "Créditos")]
        public int Creditos { get; set; }

        [StringLength(50)]
        public string? Horario { get; set; }

        [Display(Name = "Carrera")]
        public int CarreraId { get; set; }

        public Carrera? Carrera { get; set; }
    }
}
