﻿using ApiMundoDisney.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.DTO
{
    public class PersonajeDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Imagen { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public int Edad { get; set; }

        [Required]
        public int Peso { get; set; }

        [Required]
        public string Historia { get; set; }

    }
}
