using ApiMundoDisney.DTO;
using ApiMundoDisney.Models;
using ApiMundoDisney.Repositories;
using ApiMundoDisney.Repositories.Implements;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiMundoDisney.Controllers
{
    [Route("api/characters")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly ICharacterRepository _characterRespository;
        protected ResponseDto _response;

        public CharactersController(ICharacterRepository characterRepository)
        {
            _characterRespository = characterRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<PersonajeMuestraDto>>> GetAll()
        {
            try
            {
                var lista = await _characterRespository.GetPersonajes();
                if (lista.Count() <= 0)
                {
                    _response.IsSucess = false;
                    _response.DisplayMessage = "No hay registros";
                    return NotFound(_response);
                }
                else
                {
                    _response.Result = lista;
                    _response.DisplayMessage = "Lista de Personajes";
                    return Ok(_response);
                }
                
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.DisplayMessage = "Error al obtener la información";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }

        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<PersonajeDto>> Get(int id)
        {
            try
            {
                var entidad = await _characterRespository.GetByID(id);
                if (entidad == null)
                {
                    _response.IsSucess = false;
                    _response.DisplayMessage = "Personaje no existe";
                    return NotFound(_response);
                }
                _response.Result = entidad;
                _response.DisplayMessage = "Información del Personaje";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.DisplayMessage = "Error al obtener la información";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }

        }

        [HttpPost]
        public virtual async Task<ActionResult> Post([FromForm]PersonajeCreacionDto creationDto)
        {
           try
           {
                if(creationDto.PeliculaSerieId != null)
                {
                    foreach (var id in creationDto.PeliculaSerieId)
                    {
                        if (creationDto.PeliculaSerieId.Count >= 1 && id != null)
                        {
                            if (!Regex.IsMatch(id, @"^[0-9]+$"))
                            {
                                _response.IsSucess = false;
                                _response.DisplayMessage = "El id de la película o serie debe ser un número";
                                return BadRequest(_response);
                            }
                            var existe = await _characterRespository.PeliculaSerieExiste(Int32.Parse(id));
                            if (!existe)
                            {
                                _response.IsSucess = false;
                                _response.DisplayMessage = $"No existe la película o serie con el id = {id}";
                                return BadRequest(_response);
                            }

                        }
                    }
                }
                
                var personaje = await _characterRespository.Insert(creationDto);
                _response.Result = personaje;
                _response.DisplayMessage = "Se ha guardado el registro";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.DisplayMessage = "Error al grabar el registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

        [HttpPut("{id}")]
        public virtual async Task<ActionResult> Put(int id, [FromForm]PersonajeCreacionDto creacionDto)
        {
            try
            {
                var entidad = await _characterRespository.GetByID(id);
                if (entidad == null)
                {
                    _response.IsSucess = false;
                    _response.DisplayMessage = "Personaje no existe";
                    return NotFound(_response);
                }
                var dto = await _characterRespository.Update(id, creacionDto);
                _response.Result = dto;
                _response.DisplayMessage = "Se han guardado los cambios";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.DisplayMessage = "Error no se pudo modificar la información";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }

        }

        [HttpDelete("{id}")]
        public virtual async Task<ActionResult> Delete(int id)
        {
            try
            {
                var entidad = await _characterRespository.GetByID(id);
                if (entidad == null)
                {
                    _response.IsSucess = false;
                    _response.DisplayMessage = "Personaje no existe";
                    return NotFound(_response);
                }

                bool respuesta = await _characterRespository.Delete(id);
                if (respuesta)
                {
                    _response.DisplayMessage = "Personaje fue eliminado";
                    return Ok(_response);
                }
                else
                {
                    _response.IsSucess = respuesta;
                    _response.DisplayMessage = "No se pudo eliminar el registro";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.IsSucess = false;
                _response.DisplayMessage = "Error al eliminar el registro";
                _response.ErrorMessages = new List<string> { ex.ToString() };
                return BadRequest(_response);
            }
        }

    }
}
