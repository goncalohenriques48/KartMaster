using KartMaster.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KartMaster.Controllers.API {
    /// <summary>
    /// Controlador responsável pela autenticação dos utilizadores e geração de tokens JWT.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor do AuthController.
        /// </summary>
        /// <param name="userManager">Gerenciador de utilizadores do Identity.</param>
        /// <param name="configuration">Configurações da aplicação (inclui chave JWT).</param>
        public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration) {
            _userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Autentica um utilizador e devolve um token JWT válido.
        /// </summary>
        /// <param name="model">Objeto contendo o nome de utilizador e a password.</param>
        /// <returns>Objeto com o token JWT e a respetiva data de expiração.</returns>
        /// <remarks>
        /// Este endpoint é utilizado para login via API. Se as credenciais forem válidas,
        /// será devolvido um JWT com as respetivas claims e roles do utilizador.
        /// </remarks>
        /// <response code="200">Token gerado com sucesso.</response>
        /// <response code="401">Credenciais inválidas.</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model) {
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized(new { message = "Credenciais inválidas" });

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             };

            // ADICIONAR AS ROLES COMO CLAIMS
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var jwtKey = _configuration["Jwt:Key"];
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }
    }
}
