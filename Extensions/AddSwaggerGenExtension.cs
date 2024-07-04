using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace authentication;

public static class AddSwaggerGenExtension
{
      public static WebApplicationBuilder AddSwaggerExtension(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Provide a valid token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement{
                {
                    new OpenApiSecurityScheme{
                        Reference=new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id=JwtBearerDefaults.AuthenticationScheme
                        }
                    }, new string[]{}
                }
            });
               });
        return builder;
    }


}
