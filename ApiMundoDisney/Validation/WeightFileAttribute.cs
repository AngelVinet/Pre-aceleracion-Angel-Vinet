using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Validation
{
    public class WeightFileAttribute : ValidationAttribute
    {
        private readonly double _pesoArchivoKb;
        public WeightFileAttribute(double pesoArchivoKb)
        {
            _pesoArchivoKb = pesoArchivoKb;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var formFile = value as IFormFile;
            if(formFile.Length / 1024 > _pesoArchivoKb)
            {
                return new ValidationResult($"El peso máximó para el archivo que envias es de {_pesoArchivoKb} KB" +
                        $"sin embargo has enviado un archivo de {formFile.Length / 1024} KB");
            }
            return ValidationResult.Success;
        }
    }
}
