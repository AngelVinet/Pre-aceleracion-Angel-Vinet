using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Validation
{
    public class ExtensionFileAttribute : ValidationAttribute
    {
        private readonly string[] _tiposValidos;

        public ExtensionFileAttribute(string[] tiposValidos)
        {
            _tiposValidos = tiposValidos;
        }

        public ExtensionFileAttribute(TipoArchivo tipoArchivo)
        {
            if(tipoArchivo == TipoArchivo.Image)
            {
                _tiposValidos = new[] { "image/png", "image/jpeg", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var formFile = value as IFormFile;
            if (!_tiposValidos.Contains(formFile.ContentType))
            {
                return new ValidationResult($"Los tipos de imagenes validos son {string.Join(",", _tiposValidos)}");
            }
            return ValidationResult.Success;
        }

    }
}
