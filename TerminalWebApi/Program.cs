using Microsoft.EntityFrameworkCore;
using SahmabZoneCalculator.Service;
using TerminalWebApi;
using TerminalWebApi.Service.Interfaces;

var myAllowSpecificOrigins = "_mySpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ITerminalService, TerminalService>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: myAllowSpecificOrigins,
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:4200")
//            .AllowAnyMethod()
//            .AllowAnyHeader();
//        });
//});
var app = builder.Build();

//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseCors(myAllowSpecificOrigins);

app.UseDefaultFiles();

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();