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
    public class CharacterRepository : BaseRepository<PersonajeCreacionDto, Personaje, PersonajeDto>, ICharacterRepository
    {
        private readonly DisneyDbContext _disneyDb;
        private readonly IMapper _mapper;
        private readonly IAlmacenador _almacenador;
        public CharacterRepository(DisneyDbContext disneyDbContext, IMapper mapper, IAlmacenador almacenador)
            : base(disneyDbContext, mapper)
        {
            _disneyDb = disneyDbContext;
            _mapper = mapper;
            _almacenador = almacenador;
        }

        public virtual async Task<List<PersonajeMuestraDto>> GetPersonajes()
        {
            try
            {
                var personaje = await _disneyDb.Set<Personaje>().ToListAsync();
                return _mapper.Map<List<PersonajeMuestraDto>>(personaje);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } 
        }

        public async override Task<PersonajeDto> Insert(PersonajeCreacionDto creation)
        {
            try
            {
                var personaje = _mapper.Map<Personaje>(creation);
                string imgUrl = await GuardarFoto(creation.Imagen);
                personaje.Imagen = imgUrl;
                await _disneyDb.Personajes.AddAsync(personaje);
                await _disneyDb.SaveChangesAsync();
                if(creation.PeliculaSerieId != null)
                {
                    foreach (var id in creation.PeliculaSerieId)
                    {
                        if (id != null)
                        {
                            var personajePeliculaSerie = new Personaje_PeSe()
                            {
                                PersonajeId = personaje.Id,
                                PeliculaSerieId = Int32.Parse(id)
                            };
                            await _disneyDb.Personajes_PeSe.AddAsync(personajePeliculaSerie);
                            _disneyDb.SaveChanges();
                        }

                    }
                }
                var dto = _mapper.Map<PersonajeDto>(personaje);
                return dto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async override Task<PersonajeDto> Update(int id, PersonajeCreacionDto creation)
        {
            try
            {
                var personaje = await _disneyDb.Set<Personaje>().FirstOrDefaultAsync(p => p.Id == id);
                _mapper.Map(creation, personaje);
                if (!string.IsNullOrEmpty(personaje.Imagen))
                {
                    await _almacenador.BorrarImagen(personaje.Imagen, ContenedorArchivos.imgPersonajes);
                }
                string imgUrl = await GuardarFoto(creation.Imagen);
                personaje.Imagen = imgUrl;
                _disneyDb.Entry(personaje).State = EntityState.Modified;
                await _disneyDb.SaveChangesAsync();
                var dto = _mapper.Map<PersonajeDto>(personaje);
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
                var personaje = await _disneyDb.Set<Personaje>().FirstOrDefaultAsync(c => c.Id == id);
                if (!string.IsNullOrEmpty(personaje.Imagen))
                {
                    await _almacenador.BorrarImagen(personaje.Imagen, ContenedorArchivos.imgPersonajes);
                }
                _disneyDb.Entry(personaje).State = EntityState.Deleted;
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
                    ContenedorArchivos.imgPersonajes, Guid.NewGuid().ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<bool> PeliculaSerieExiste(int id)
        {
            bool respuesta = true;
            var peliculaSerie = await _disneyDb.PeliculasSeries.FirstOrDefaultAsync(p => p.Id == id);
            if(peliculaSerie == null)
            {
                respuesta = false;
            }
            return respuesta;
        }
    }
}
