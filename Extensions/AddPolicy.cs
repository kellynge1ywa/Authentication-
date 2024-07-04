namespace authentication;

public static class AddPolicy
{
    public static WebApplicationBuilder AdminPolicy(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", options =>
            {
                options.RequireAuthenticatedUser();
                options.RequireClaim("Roles", "Admin");

            });

        });
        return builder;

    }

}
