using ApiMundoDisney.DTO;
using ApiMundoDisney.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiMundoDisney.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepository _movieRepository;
        private readonly ResponseDto _response;

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<PeliculaSerieDto>>> GetAll()
        {
            try
            {
                var lista = await _movieRepository.GetMovies();
                if (lista.Count() <= 0)
                {
                    _response.IsSucess = false;
                    _response.DisplayMessage = "No hay registros";
                    return NotFound(_response);
                }
                else
                {
                    _response.Result = lista;
                    _response.DisplayMessage = "Lista de Películas y Series";
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
        public virtual async Task<ActionResult<PeliculaSerieDto>> Get(int id)
        {
            try
            {
                var entidad = await _movieRepository.GetByID(id);
                if (entidad == null)
                {
                    _response.IsSucess = false;
                    _response.DisplayMessage = "Película o Serie no existe";
                    return NotFound(_response);
                }
                _response.Result = entidad;
                _response.DisplayMessage = "Información de la Película o Serie";
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
        public virtual async Task<ActionResult> Post([FromForm] PeliculaSerieCreacionDto creationDto)
        {
            try
            {
                var dto = await _movieRepository.Insert(creationDto);
                _response.Result = dto;
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
        public virtual async Task<ActionResult> Put(int id, [FromForm] PeliculaSerieCreacionDto creacionDto)
        {
            try
            {
                var entidad = await _movieRepository.GetByID(id);
                if (entidad == null)
                {
                    _response.IsSucess = false;
                    _response.DisplayMessage = "Película o Serie no existe";
                    return NotFound(_response);
                }
                var dto = await _movieRepository.Update(id, creacionDto);
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
                var entidad = await _movieRepository.GetByID(id);
                if (entidad == null)
                {
                    _response.IsSucess = false;
                    _response.DisplayMessage = "Película o Serie no existe";
                    return NotFound(_response);
                }
                bool respuesta = await _movieRepository.Delete(id);
                if (respuesta)
                {
                    _response.DisplayMessage = "Película o Serie fue eliminada";
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
