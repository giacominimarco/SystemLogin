using Microsoft.EntityFrameworkCore;
using ProjetoLogin.API.Data;
using ProjetoLogin.API.Repositories;
using ProjetoLogin.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Registrar o serviço UserService e o repositório UserRepository
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configurar o contexto do banco de dados (se necessário)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddControllers();  // Habilitar controllers
builder.Services.AddOpenApi();

var app = builder.Build();

// Configurar a pipeline de requisições
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers(); // Mapear controladores

app.Run();