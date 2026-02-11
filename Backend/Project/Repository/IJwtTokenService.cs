//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//public interface IJwtTokenService
//{
//    string GenerateToken(string username, string role);
//}

//public class JwtTokenService : IJwtTokenService
//{
//    private readonly IConfiguration _config;

//    public JwtTokenService(IConfiguration config)
//    {
//        _config = config;
//    }

//    public string GenerateToken(string username, string role)
//    {
//        var jwtConfig = _config.GetSection("Jwt");
//        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]));
//        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//        var claims = new[]
//        {
//            new Claim(ClaimTypes.Name, username),
//            new Claim(ClaimTypes.Role, role)
//        };

//        var token = new JwtSecurityToken(
//            issuer: jwtConfig["Issuer"],
//            audience: jwtConfig["Audience"],
//            claims: claims,
//            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtConfig["DurationInMinutes"])),
//            signingCredentials: creds
//        );

//        return new JwtSecurityTokenHandler().WriteToken(token);
//    }
//}
