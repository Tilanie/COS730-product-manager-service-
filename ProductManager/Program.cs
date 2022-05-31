using ProductManager.Logic;
using ProductManager.Models;
using ProductManager.Services;


var builder = WebApplication.CreateBuilder(args);
var settings = builder.Configuration.GetSection("CosmosDb").Get<Settings>();

builder.Services.AddSingleton<IProductLogic, ProductLogic>();
builder.Services.AddSingleton<IQueueLogic, QueueLogic>();
builder.Services.AddSingleton<ITaskLogic, TaskLogic>();
builder.Services.AddSingleton<IUserLogic, UserLogic>();
builder.Services.AddSingleton<ICosmosDbService>(InitializeCosmosClientInstanceAsync(settings).GetAwaiter().GetResult());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

/*
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader()
      .AllowAnyMethod()
      .AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();

 static async Task<CosmosDbService> InitializeCosmosClientInstanceAsync(Settings settings)
{
    string databaseName = settings.DatabaseName;
    string containerName = settings.ContainerName;
    string account = settings.Account;
    string key = settings.Key;
    Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
    CosmosDbService cosmosDbService = new CosmosDbService(client, databaseName, containerName);
    Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
    await database.Database.CreateContainerIfNotExistsAsync(containerName, "/DataLayerType");

    return cosmosDbService;
}

