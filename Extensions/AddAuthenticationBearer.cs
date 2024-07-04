using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace authentication;

public static class AddAuthenticationBearer
{
     public static WebApplicationBuilder AddAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new()
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = builder.Configuration["JwtOptions:Audience"],
                ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:SecretKey"]))
            };
        });
        return builder;
    }

}
