
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using DA;
using DA.Repositorios;
using Flujo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IEstudianteFlujo, EstudianteFlujo>();
builder.Services.AddScoped<IEstudianteDA, EstudianteDA>();

builder.Services.AddScoped<ISeccionDA, SeccionDA>();
builder.Services.AddScoped<ISeccionFlujo, SeccionFlujo>();


builder.Services.AddScoped<IMateriaDA, MateriaDA>();
builder.Services.AddScoped<IMateriaFlujo, MateriaFlujo>();

builder.Services.AddScoped<ICondicionEspecialDA, CondicionEspecialDA>();
builder.Services.AddScoped<ICondicionEspecialFlujo, CondicionEspecialFlujo>();


builder.Services.AddScoped<IDocumentoDA, DocumentoDA>();
builder.Services.AddScoped<IDocumentoFlujo, DocumentoFlujo>();

builder.Services.AddScoped<IHorarioDA, HorarioDA>();
builder.Services.AddScoped<IHorarioFlujo, HorarioFlujo>();



builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();


// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        policy =>
        {
            policy.WithOrigins("https://localhost:7079") // Especifica el origen del proyecto frontend
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowMyOrigin"); // Aplica la política de CORS

app.UseAuthorization();

app.MapControllers();

app.Run();
