var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("Mongo").AddDatabase("KoggoDb");


builder.AddProject<Projects.KoggoInvestments_Api>("Api")
    .WithReference(mongo);

builder.AddProject<Projects.KoggoInvestments_Worker>("Worker")
    .WithReference(mongo)
    .WithReference("FinnApi", new Uri("https://finnhub.io/api"));

builder.Build().Run();