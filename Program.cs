using Delivery_Backend.Models;
using Microsoft.EntityFrameworkCore;
using NinjaTalentCountrys.Interfaces;
using NinjaTalentCountrys.Models;
using NinjaTalentCountrys.Services;
using NinjaTalentCountrys.Services.User;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ErrorService>();
builder.Services.AddScoped<UserValidation>();
builder.Services.AddScoped<UserValidationExist>();
builder.Services.AddScoped<UserValidationFilter>();
builder.Services.AddScoped<UserList>();
builder.Services.AddScoped<UserValidationAdd>();
builder.Services.AddScoped<UserValidationExistUserName>();
builder.Services.AddScoped<IError, ErrorService>();
builder.Services.AddScoped<IUser,UserServices>();

// Add services to the container.
//builder.Services.AddScoped<IUser, UserServices>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Conexion a la base de datos con la interface
builder.Services.AddDbContext<InterfacesDBContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlConnection"))
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


app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

//PARA PERMITIR QUE ENVIE O RECIBA DE CUALQUIER LUGAR O APP
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
});
//FIN

app.MapControllers();

app.Run();

