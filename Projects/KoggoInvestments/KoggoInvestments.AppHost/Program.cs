var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("Mongo");
builder.AddProject<Projects.KoggoInvestments_Api>("Api")
    .WithReference(mongo);
builder.AddProject<Projects.KoggoInvestments_Worker>("Worker")
    .WithReference(mongo);

builder.Build().Run();