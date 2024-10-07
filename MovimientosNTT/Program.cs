using Microsoft.EntityFrameworkCore;
using MovimientosNTT.Data;
using MovimientosNTT.Interfaces;
using MovimientosNTT.Repository;
using MovimientosNTT.Requests;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<ICuentaRepository, CuentaRepository>();
builder.Services.AddScoped<IClienteRequest, ClienteRequest>();
builder.Services.AddScoped<IMovimientoRepository, MovimientoRepository>();
builder.Services.AddHttpClient("ClientesClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7113/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
