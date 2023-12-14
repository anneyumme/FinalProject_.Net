using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinalProject_.Net.Service;

public class TokenService
{
    readonly string _secretKey = "eyJhbGciOiJIUzI1NiJ9.eyJSb2xlIjoiQWRtaW4iLCJJc3N1ZXIiOiJJc3N1ZXIiLCJVc2VybmFtZSI6IkphdmFJblVzZSIsImV4cCI6MTcwMjU1NTk3MCwiaWF0IjoxNzAyNTU1OTcwfQ.CElAbppwCbsgo7JBnRPIrFXKgsNd0IhcDTm4JbGG3pQ";
    readonly string _issuer = "Ninh Dong";
    readonly string _audience = "Bao Anh";
    public string GenerateToken(string Fname, string Lname, string username, string address  , string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey); // Use a secure key

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {

                new Claim("FirstName", Fname),
                new Claim("LastName", Lname),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.StreetAddress, address),
                new Claim(ClaimTypes.Role, "Saler"),
                new Claim(ClaimTypes.Email, email)
            }),
            Expires = DateTime.UtcNow.AddMinutes(100),
            Issuer= _issuer,
            Audience= _audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey)),
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidAudience = _audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        try
        {
            tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            // Token is valid
            return true;
        }
        catch
        {
            // Token is invalid
            return false;
        }
    }
}