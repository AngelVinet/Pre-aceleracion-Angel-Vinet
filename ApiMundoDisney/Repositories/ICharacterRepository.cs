using ApiMundoDisney.DTO;
using ApiMundoDisney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public interface ICharacterRepository : IBaseRepository<PersonajeCreacionDto, Personaje, PersonajeDto>
    {
        public Task<List<PersonajeMuestraDto>> GetPersonajes();
        public Task<bool> PeliculaSerieExiste(int id);
    }
}
