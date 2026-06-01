var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 1. Agregar el servicio de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL donde correrá Angular
              .AllowAnyMethod()                    // Permite GET, POST, DELETE, etc.
              .AllowAnyHeader();                   // Permite cualquier cabecera
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

// 2. Usar la política de CORS (Colócalo antes de app.MapControllers())
app.UseCors("PermitirAngular");

app.MapControllers();

app.Run();
