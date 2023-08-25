using Delivery_Backend.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

//PARA PERMITIR QUE ENVIE O RECIBA DE CUALQUIER LUGAR O APP
builder.Services.AddCors(o => o.AddPolicy("AllowAnyOrigin",
    builder =>
    {
        builder.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    }));
//FIN
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Conexion a la base de datos con la interface
builder.Services.AddDbContext<InterfacesDBContext>(
    options =>
    {
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("PostgresqlConnection")
        );
    }
);
//fin de conexion


builder.Services.AddHttpClient();

var app = builder.Build();
//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//app.UseSwagger();
//app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();


app.UseCors("AllowAnyOrigin");


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
