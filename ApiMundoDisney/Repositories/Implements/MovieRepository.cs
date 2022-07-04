using ApiMundoDisney.DTO;
using ApiMundoDisney.Models;
using ApiMundoDisney.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static ApiMundoDisney.Constantes;

namespace ApiMundoDisney.Repositories.Implements
{
    public class MovieRepository : BaseRepository<PeliculaSerieCreacionDto, PeliculaSerie, PeliculaSerieDto>, IMovieRepository
    {
        private readonly DisneyDbContext _disneyDb;
        private readonly IMapper _mapper;
        private readonly IAlmacenador _almacenador;
        public MovieRepository(DisneyDbContext disneyDbContext, IMapper mapper, IAlmacenador almacenador) 
            : base(disneyDbContext, mapper)
        {
            _disneyDb = disneyDbContext;
            _mapper = mapper;
            _almacenador = almacenador;
        }

        public virtual async Task<List<PeliculaSerieMuestraDto>> GetMovies()
        {
            try
            {
                var movie = await _disneyDb.Set<PeliculaSerie>().ToListAsync();
                return _mapper.Map<List<PeliculaSerieMuestraDto>>(movie);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async override Task<PeliculaSerieDto> Insert(PeliculaSerieCreacionDto creation)
        {
            try
            {
                var movie = _mapper.Map<PeliculaSerie>(creation);
                string imgUrl = await GuardarFoto(creation.Imagen);
                movie.Imagen = imgUrl;
                await _disneyDb.PeliculasSeries.AddAsync(movie);
                await _disneyDb.SaveChangesAsync();
                var dto = _mapper.Map<PeliculaSerieDto>(movie);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async override Task<PeliculaSerieDto> Update(int id, PeliculaSerieCreacionDto creation)
        {
            try
            {
                var movie = await _disneyDb.Set<PeliculaSerie>().FirstOrDefaultAsync(p => p.Id == id);
                _mapper.Map(creation, movie);
                if (!string.IsNullOrEmpty(movie.Imagen))
                {
                    await _almacenador.BorrarImagen(movie.Imagen, ContenedorArchivos.imgPeliculaSerie);
                }
                string imgUrl = await GuardarFoto(creation.Imagen);
                movie.Imagen = imgUrl;
                _disneyDb.Entry(movie).State = EntityState.Modified;
                await _disneyDb.SaveChangesAsync();
                var dto = _mapper.Map<PeliculaSerieDto>(movie);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async override Task<bool> Delete(int id)
        {
            try
            {
                var movie = await _disneyDb.Set<PeliculaSerie>().FirstOrDefaultAsync(c => c.Id == id);
                if (!string.IsNullOrEmpty(movie.Imagen))
                {
                    await _almacenador.BorrarImagen(movie.Imagen, ContenedorArchivos.imgPeliculaSerie);
                }
                _disneyDb.Entry(movie).State = EntityState.Deleted;
                await _disneyDb.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
        }

        private async Task<string> GuardarFoto(IFormFile foto)
        {
            try
            {
                using var stream = new MemoryStream();
                await foto.CopyToAsync(stream);
                var bytes = stream.ToArray();
                return await _almacenador.GuardarImagen(bytes, foto.ContentType, Path.GetExtension(foto.FileName),
                    ContenedorArchivos.imgPeliculaSerie, Guid.NewGuid().ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
