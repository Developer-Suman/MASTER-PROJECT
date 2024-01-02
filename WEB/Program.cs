using Application;
using Infrastructure;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigurationManager configuration = builder.Configuration;
builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure(configuration);

var app = builder.Build();



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


