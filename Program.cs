using authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IUser,UserServices>();
builder.Services.AddScoped<Ijwt, JwtServices>();

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

builder.Services.AddDbContext<AppDbContext>(options=>{
    options.UseMySQL(builder.Configuration.GetConnectionString("myConnections"));
});

builder.AddSwaggerExtension();
builder.AddAuth();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// app.UseMigrations();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();


