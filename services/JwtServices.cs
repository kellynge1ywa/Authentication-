using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace authentication;

public class JwtServices : Ijwt
{
    private readonly JwtOptions _jwtOptions;
    public JwtServices(IOptions<JwtOptions> options)
    {
        _jwtOptions=options.Value;
    }
    public string GenerateToken(User appUser)
    {
        var userKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
        var cred=new SigningCredentials(userKey,SecurityAlgorithms.HmacSha256);

        List<Claim> claims=new List<Claim>();
        claims.Add(new Claim("Roles",appUser.Role));
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, appUser.Id.ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.FamilyName, appUser.Fullname));

         //token
        var tokenDescriptor=new SecurityTokenDescriptor()
        {
            Issuer=_jwtOptions.Issuer,
            Audience=_jwtOptions.Audience,
            Expires=DateTime.Now.AddHours(3),
            Subject=new ClaimsIdentity(claims),
            SigningCredentials=cred

        };

        var token=new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
