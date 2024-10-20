var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("Mongo", port: 52158)
    .WithDataBindMount("/mongodata")
    .AddDatabase("KoggoDb");


builder.AddProject<Projects.KoggoInvestments_Api>("Api")
    .WithReference(mongo);

builder.AddProject<Projects.KoggoInvestments_Worker>("Worker")
    .WithReference(mongo)
    .WithReference("FinnApi", new Uri("https://finnhub.io/"))
    .WithReference("PolyApi", new Uri("https://api.polygon.io/"));

builder.Build().Run();