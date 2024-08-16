var builder = WebApplication.CreateBuilder(args);

// Configurar CORS para permitir solicitudes desde la aplicación React
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Agregar MongoDbContext como un servicio
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddControllers();

var app = builder.Build();

// Habilitar CORS para las solicitudes entrantes
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
