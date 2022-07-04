using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Utilities
{
    public interface IAlmacenador
    {
        public Task<string> GuardarImagen(byte[] file, string contentType, string extension, string container, string name);
        public Task BorrarImagen(string ruta, string container);
    }
}
