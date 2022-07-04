using ApiMundoDisney.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.DTO
{
    public class PeliculaSerieCreacionDto
    {
        [Required(ErrorMessage = "Debe ingresar una imagen")]
        [ExtensionFileAttribute(TipoArchivo.Image)]
        [WeightFile(1024)]
        public IFormFile Imagen { get; set; }

        [Required(ErrorMessage = "Debe ingresar el título de la película")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de creación de la película")]
        public string FechaCreacion { get; set; }

        [Required(ErrorMessage = "Debe ingresar la calificación de la película")]
        [Range(1,5, ErrorMessage = "La clasificación debe ser entre 1 y 5")]
        public int Calificacion { get; set; }
    }
}
