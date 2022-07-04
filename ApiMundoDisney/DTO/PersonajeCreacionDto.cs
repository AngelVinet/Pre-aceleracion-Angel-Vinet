using ApiMundoDisney.Models;
using ApiMundoDisney.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.DTO
{
    public class PersonajeCreacionDto
    {
        [Required(ErrorMessage = "Debe ingresar una imagen")]
        [ExtensionFileAttribute(TipoArchivo.Image)]
        [WeightFile(1024)]
        public IFormFile Imagen { get; set; }

        [Required(ErrorMessage = "Debe ingresar el nombre del personaje")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe ingresar la edad del personaje")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar solo números")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "Debe ingresar el peso del personaje")]
        [Range(0, int.MaxValue, ErrorMessage = "Debe ingresar solo números")]
        public int Peso { get; set; }

        [Required(ErrorMessage = "Debe ingresar la historia del personaje")]
        public string Historia { get; set; }

        //[RegularExpression("/^([0-9])*$/", ErrorMessage = "Debe ingresar números")]
        public List<string> PeliculaSerieId { get; set; }
    }
}
