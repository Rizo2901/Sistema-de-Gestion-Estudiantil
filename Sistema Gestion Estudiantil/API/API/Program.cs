
using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos.Servicios.Helpers;
using DA;
using DA.Repositorios;
using Flujo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using QuestPDF.Infrastructure;
using Servicios.Helpers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Configuración JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["JwtSettings:SecretKey"])
        )
    };
});

// ... configuración JWT y servicios ...

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Sistema Escolar",
        Version = "v1"
    });

    // Configuración de seguridad JWT para Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IActividadDA , ActividadDA>();
builder.Services.AddScoped<IActividadFlujo, ActividadFlujo>();

builder.Services.AddScoped<IAlergiaDA, AlergiaDA>();
builder.Services.AddScoped<IAlergiaFlujo, AlergiaFlujo>();


builder.Services.AddScoped<IAtencionMedicaDA, AtencionMedicaDA>();
builder.Services.AddScoped<IAtencionMedicaFlujo, AtencionMedicaFlujo>();


builder.Services.AddScoped<IAdaptacionCurricularDA, AdaptacionCurricularDA>();
builder.Services.AddScoped<IAdaptacionCurricularFlujo, AdaptacionCurricularFlujo>();

builder.Services.AddScoped<IAdministradorDA, AdministradorDA>();
builder.Services.AddScoped<IAdministradorFlujo, AdministradorFlujo>();

builder.Services.AddScoped<IAsistenciaProfesorDA, AsistenciaProfesorDA>();
builder.Services.AddScoped<IAsistenciaProfesorFlujo, AsistenciaProfesorFlujo>();

builder.Services.AddScoped<IArchivoServiceFlujo, ArchivoServiceFlujo>();
builder.Services.AddScoped<IArchivoService, ArchivoService>();
builder.Services.AddScoped<IAutenticacionFlujo, AutenticacionFlujo>();

builder.Services.AddScoped<ICalificacionDA, CalificacionDA>();
builder.Services.AddScoped<ICalificacionFlujo, CalificacionFlujo>();

builder.Services.AddScoped<ICategoriaAlergiaDA, CategoriaAlergiaDA>();
builder.Services.AddScoped<ICategoriaAlergiaFlujo, CategoriaAlergiaFlujo>();

builder.Services.AddScoped<ICategoriaDiscapacidadDA, CategoriaDiscapacidadDA>();
builder.Services.AddScoped<ICategoriaDiscapacidadFlujo, CategoriaDiscapacidadFlujo>();

builder.Services.AddScoped<ICategoriaCondicionEspecialDA, CategoriaCondicionEspecialDA>();
builder.Services.AddScoped<ICategoriaCondicionEspecialFlujo, CategoriaCondicionEspecialFlujo>();


builder.Services.AddScoped<ICategoriaSituacionFamiliarDA, CategoriaSituacionFamiliarDA>();
builder.Services.AddScoped<ICategoriaSituacionFamiliarFlujo, CategoriaSituacionFamiliarFlujo>();

builder.Services.AddScoped<ICorreoHelper, CorreoHelper>();

builder.Services.AddScoped<IFamiliarDA, FamiliarDA>();
builder.Services.AddScoped<IFamiliarFlujo, FamiliarFlujo>();

builder.Services.AddScoped<IComportamientoDA, ComportamientoDA>();
builder.Services.AddScoped<IComportamientoFlujo, ComportamientoFlujo>();


builder.Services.AddScoped<ICondicionEspecialDA, CondicionEspecialDA>();
builder.Services.AddScoped<ICondicionEspecialFlujo, CondicionEspecialFlujo>();

builder.Services.AddScoped<IDocumentoDA, DocumentoDA>();
builder.Services.AddScoped<IDocumentoFlujo, DocumentoFlujo>();

builder.Services.AddScoped<IDiscapacidadDA, DiscapacidadDA>();
builder.Services.AddScoped<IDiscapacidadFlujo, DiscapacidadFlujo>();

builder.Services.AddScoped<IEnfermedadFlujo, EnfermedadFlujo>();
builder.Services.AddScoped<IEnfermedadDA, EnfermedadDA>();

builder.Services.AddScoped<IEstudianteFlujo, EstudianteFlujo>();
builder.Services.AddScoped<IEstudianteDA, EstudianteDA>();

builder.Services.AddScoped<IEstudianteAlergiaDA, EstudianteAlergiaDA>();
builder.Services.AddScoped<IEstudianteAlergiaFlujo, EstudianteAlergiaFlujo>();

builder.Services.AddScoped<IEstudianteDiscapacidadFlujo, EstudianteDiscapacidadFlujo >();
builder.Services.AddScoped<IEstudianteDiscapacidadDA, EstudianteDiscapacidadDA>();

builder.Services.AddScoped<IEstudianteMedicamentoDA, EstudianteMedicamentoDA>();
builder.Services.AddScoped<IEstudianteMedicamentoFlujo, EstudianteMedicamentoFlujo>();


builder.Services.AddScoped<IProfesorDA, ProfesorDA>();
builder.Services.AddScoped<IProfesorFlujo, ProfesorFlujo>();


builder.Services.AddScoped<IRolDA, RolDA>();
builder.Services.AddScoped<IRolFlujo, RolFlujo>();

builder.Services.AddScoped<IReunionDA, ReunionDA>();
builder.Services.AddScoped<IReunionFlujo, ReunionFlujo>();

builder.Services.AddScoped<IReunionAdministrativaDA, ReunionAdministrativaDA>();
builder.Services.AddScoped<IReunionAdministrativaFlujo, ReunionAdministrativaFlujo>();

builder.Services.AddScoped<IReporteDA, ReporteDA>();
builder.Services.AddScoped<IReporteFlujo, ReporteFlujo>();


builder.Services.AddScoped<ISeccionDA, SeccionDA>();
builder.Services.AddScoped<ISeccionFlujo, SeccionFlujo>();

builder.Services.AddScoped<ISeccionProfesorDA, SeccionProfesorDA>();
builder.Services.AddScoped<ISeccionProfesorFlujo, SeccionProfesorFlujo>();

builder.Services.AddScoped<ISituacionFamiliarDA, SituacionFamiliarDA>();
builder.Services.AddScoped<ISituacionFamiliarFlujo, SituacionFamiliarFlujo>();

builder.Services.AddScoped<IMateriaDA, MateriaDA>();
builder.Services.AddScoped<IMateriaFlujo, MateriaFlujo>();

builder.Services.AddScoped<IMedicamentoDA, MedicamentoDA>();
builder.Services.AddScoped<IMedicamentoFlujo, MedicamentoFlujo>();

builder.Services.AddScoped<IMateriaProfesorDA, MateriaProfesorDA>();
builder.Services.AddScoped<IMateriaProfesorFlujo, MateriaProfesorFlujo>();

builder.Services.AddScoped<IHorarioDA, HorarioDA>();
builder.Services.AddScoped<IHorarioFlujo, HorarioFlujo>();

builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<IUsuarioFlujo, UsuarioFlujo>();



builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();


QuestPDF.Settings.License = LicenseType.Community;

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        policy =>
        {
            policy.AllowAnyOrigin()
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

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
