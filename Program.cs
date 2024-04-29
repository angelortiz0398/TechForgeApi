
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text.Json.Serialization;
using TechForge.Negocio;
using TechForge.Repositorio;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
var builder = WebApplication.CreateBuilder(args);
//builder.WebHost.UseKestrel(options =>
//{
//    options.Limits.MaxRequestBodySize = 2147483647; //50MB
//    options.Limits.MaxRequestBufferSize = 2147483647; //50MB
//    options.Limits.MaxResponseBufferSize = 2147483647;
//    options.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(10);
//    options.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(10);
//});
builder.Services.AddControllers()
    .AddJsonOptions(x => {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("Contacto", new OpenApiInfo
    {
        Title = "Contactos de TechForge",
        Description = "Seccion para gestionar los contactos"
    });

    // Configuración para incluir comentarios XML de documentación
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    // Limita la cantidad de información generada
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
}
);

builder.Services.AddScoped<ContactoRepository>();
builder.Services.AddScoped<ContactoBusiness>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDataProtection();
builder.Services.AddProblemDetails();
var app = builder.Build();
app.UseExceptionHandler();
app.UseStatusCodePages();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseExceptionHandler("/error-development");
}
else
{
    // Middleware para manejo de errores en producción
    app.UseExceptionHandler("/error");
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/Contacto/swagger.json", "Contacto");
});
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{namespace}/{controller=Home}/{action=Index}/{id?}");
app.Run();
