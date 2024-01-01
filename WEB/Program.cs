using Application;
using Infrastructure;
using Presentation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

ConfigurationManager configuration = builder.Configuration;
builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(configuration);

builder.Services.AddCors();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.AddEndPoint();
app.UseAuthentication();
app.UseAuthorization();


app.Run();


