using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.DTO
{
    public class PeliculaSerieDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Imagen { get; set; }

        [Required]
        public string Titulo { get; set; }
        
        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public int Calificacion { get; set; }
    }
}
