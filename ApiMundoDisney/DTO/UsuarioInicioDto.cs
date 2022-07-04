using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.DTO
{
    public class UsuarioInicioDto
    {
        [Required(ErrorMessage = "Debe Ingresar un nombre de usuario")]
        [MinLength(6, ErrorMessage = "El nombre de usuario debe tener mínimo 6 caracteres")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Debe ingresar una Contraseña")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener mínimo 6 caracteres")]
        public string Password { get; set; }
    }
}
