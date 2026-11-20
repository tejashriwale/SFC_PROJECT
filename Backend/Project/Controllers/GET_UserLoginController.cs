using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Model.Domain;
using Project.Model.DTO;
using Project.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GET_UserLoginController : ControllerBase
    {
        private readonly IGET_UserProfileSearchRepository repository;
        private readonly IMapper mapper;
        private readonly ILogger<GET_UserLoginController> logger;

        private readonly SfcJwtSettings jwt;

        public GET_UserLoginController(IGET_UserProfileSearchRepository repository, IMapper mapper, ILogger<GET_UserLoginController> logger, SfcJwtSettings jwt)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
            this.jwt = jwt;
        }
        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult>getall(GET_UserProfileSearch input)
        {
            try
            {
                // Log request details
                logger.LogInformation("=== Login Request Started ===");
                                logger.LogInformation($"Password: {(string.IsNullOrEmpty(input?.PASSWORD) ? "NULL/EMPTY" : "PROVIDED")}");
                                                logger.LogInformation($"Content-Type: {Request.ContentType}");

                if (input == null || string.IsNullOrEmpty(input.EMAIL) || string.IsNullOrEmpty(input.PASSWORD))
                {
                    logger.LogWarning("Invalid input - Email or Password is null/empty");
                    return Ok(new List<GET_UserProfileSearchDto>()); // Return empty array for invalid input
                }

                var domain = await repository.GetAll(input);
                logger.LogInformation($"Repository returned {domain?.Count ?? 0} results");

                var dto = mapper.Map<List<GET_UserProfileSearchDto>>(domain);
                logger.LogInformation($"Mapped to {dto?.Count ?? 0} DTOs");
                logger.LogInformation("=== Login Request Completed ===");

                foreach (var u in dto)
                {
                    u.Password = ""; // never send the stored password to the client
                    if (!string.IsNullOrEmpty(u.UAID))
                        u.Token = CreateToken(u);
                }
                return Ok(dto);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in GET_UserLogin: {Message}", ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }
    
        private string CreateToken(GET_UserProfileSearchDto u)
        {
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key)), SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("uaid", u.UAID ?? ""),
                new Claim(ClaimTypes.Name, u.Username ?? ""),
                new Claim("companyID", u.companyID ?? ""),
                new Claim("role", u.UROLEID ?? "")
            };
            var token = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims,
                expires: DateTime.UtcNow.AddMinutes(jwt.DurationMinutes), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
