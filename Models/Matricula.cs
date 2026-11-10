using System;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal_MatamorosDiana.Models
{
    public class Matricula
    {
        public int Id { get; set; }

        [Display(Name = "Estudiante")]
        public int EstudianteId { get; set; }

        public Estudiante? Estudiante { get; set; }

        [Display(Name = "Curso")]
        public int CursoId { get; set; }

        public Curso? Curso { get; set; }

        [Display(Name = "Fecha de matrícula")]
        [DataType(DataType.Date)]
        public DateTime FechaMatricula { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "El periodo académico es obligatorio.")]
        [StringLength(30)]
        [Display(Name = "Periodo académico")]
        public string PeriodoAcademico { get; set; } = string.Empty;
    }
}
