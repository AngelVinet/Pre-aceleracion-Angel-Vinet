using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Utilities
{
    public class Almacenador : IAlmacenador
    {
        private readonly IWebHostEnvironment _webHost;
        private readonly IHttpContextAccessor _httpContextA;

        public Almacenador(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHost = webHostEnvironment;
            _httpContextA = httpContextAccessor;
        }

        public Task BorrarImagen(string ruta, string container)
        {
            string wwwrootPath = _webHost.WebRootPath;
            if (string.IsNullOrEmpty(wwwrootPath))
            {
                throw new Exception();
            }
            var nombreArchivo = Path.GetFileName(ruta);
            string PathFinal = Path.Combine(wwwrootPath, container, nombreArchivo);
            if (File.Exists(PathFinal))
            {
                File.Delete(PathFinal);
            }
            return Task.CompletedTask;
        }

        public async Task<string> GuardarImagen(byte[] file, string contentType, string extension, string container, string name)
        {
            string wwwrootPath = _webHost.WebRootPath;
            if (string.IsNullOrEmpty(wwwrootPath))
            {
                throw new Exception();
            }
            var carpetaArchivo = Path.Combine(wwwrootPath, container);
            if (!Directory.Exists(carpetaArchivo))
            {
                Directory.CreateDirectory(carpetaArchivo);
            }
            string nombreFinal = $"{name}{extension}";
            string rutaFinal = Path.Combine(carpetaArchivo, nombreFinal);
            await File.WriteAllBytesAsync(rutaFinal, file);
            string urlActual = $"{_httpContextA.HttpContext.Request.Scheme}://{_httpContextA.HttpContext.Request.Host}";
            string dbUrl = Path.Combine(urlActual, container, nombreFinal).Replace("\\", "/");
            return dbUrl;
        }
    }
}
