using ApiMundoDisney.DTO;
using ApiMundoDisney.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ApiMundoDisney.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly ResponseDto _response;
        public AuthenticationController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _response = new ResponseDto();
        }
        //Función de Registro de usuarios
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Registro(UsuarioCreationDto usuario)
        {
            var userExists = await _userManager.FindByNameAsync(usuario.Usuario);
            if (userExists != null)
            {
                _response.IsSucess = false;
                _response.DisplayMessage = "El usuario ya existe";
                return BadRequest(_response);
            }
            var user = new Usuario
            {
                UserName = usuario.Usuario,
                Email = usuario.Email,
                IsActive = true
            };
            Match matchNumeros = Regex.Match(usuario.Password, @"\d");
            Match matchEspeciales = Regex.Match(usuario.Password, @"[ñÑ\-_¿.#¡]");
            Match matchMayuscula = Regex.Match(usuario.Password, @"[A-Z]");
            if(!matchNumeros.Success || !matchEspeciales.Success || !matchMayuscula.Success){
                _response.IsSucess = false;
                _response.DisplayMessage = "La contraseña debe tener una mayúscula, un número y un caracter no alfanumérico";
                return BadRequest(_response);
            }
            var result = await _userManager.CreateAsync(user, usuario.Password);
            if (!result.Succeeded)
            {
                _response.IsSucess = false;
                _response.DisplayMessage = "No se ha podido crear al usuario";
                return BadRequest(_response);                
            }
            _response.DisplayMessage = "Se ha registrado el usuario";
            return Ok(_response);
        }

        //Función de Ingreso de usuarios
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UsuarioInicioDto usuarioInicio)
        {
            var result = await _signInManager.PasswordSignInAsync(usuarioInicio.Usuario, usuarioInicio.Password, 
                false, false);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(usuarioInicio.Usuario);
                if (currentUser.IsActive)
                {
                    var token = await GetToken(currentUser);
                    return Ok(token);
                }
                else
                {
                    _response.IsSucess = false;
                    _response.DisplayMessage = "El usuario no esta activo";
                    return Unauthorized(_response);
                }
            }
            _response.IsSucess = false;
            _response.DisplayMessage = "Usuario ingresado es inválido";
            return BadRequest(_response);
        }

        private async Task<TokenDto> GetToken(Usuario usuario)
        {
            var userRoles = await _userManager.GetRolesAsync(usuario);
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            authClaims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            var authSignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KeySecretaSuperLargaDeApiMundoDisney"));
            var token = new JwtSecurityToken(
                issuer: "https://localhost:44340",
                audience: "https://localhost:44340",
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSignInKey, SecurityAlgorithms.HmacSha256));
            return new TokenDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ValidTo = token.ValidTo
            };
        }
    }
}
