using KoggoInvestments.Application;
using KoggoInvestments.Persistence;
using KoggoInvestments.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.AddServiceDefaults();
builder.Configuration.AddJsonFile("appsecrets.json", optional: false, reloadOnChange: true);
builder.AddApplication();
builder.AddPersistence();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapDefaultEndpoints();
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