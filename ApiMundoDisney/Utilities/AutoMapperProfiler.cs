using ApiMundoDisney.DTO;
using ApiMundoDisney.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Utilities
{
    public class AutoMapperProfiler : Profile
    {
        public AutoMapperProfiler()
        {
            //----------Personajes-------#
            CreateMap<Personaje, PersonajeDto>().ReverseMap();
            CreateMap<PersonajeCreacionDto, Personaje>();
            CreateMap<Personaje, PersonajeMuestraDto>();

            //----------Peliculas o Series------#
            CreateMap<PeliculaSerie, PeliculaSerieDto>().ReverseMap();
            CreateMap<PeliculaSerieCreacionDto, PeliculaSerie>();
            CreateMap<PeliculaSerie, PeliculaSerieMuestraDto>();

        }
    }
}
