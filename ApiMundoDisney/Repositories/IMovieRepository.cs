using ApiMundoDisney.DTO;
using ApiMundoDisney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Repositories
{
    public interface IMovieRepository : IBaseRepository<PeliculaSerieCreacionDto, PeliculaSerie, PeliculaSerieDto >
    {
        public Task<List<PeliculaSerieMuestraDto>> GetMovies();
    }
}
