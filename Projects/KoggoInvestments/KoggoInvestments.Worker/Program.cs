using KoggoInvestments.Application;
using KoggoInvestments.Persistence;
using KoggoInvestments.ServiceDefaults;
using KoggoInvestments.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsecrets.json", optional: false, reloadOnChange: true);
builder.AddServiceDefaults();
builder.AddPersistence();
builder.AddApplication();

builder.Services.AddHostedService<StockInfoSyncer>();

var host = builder.Build();
host.Run();