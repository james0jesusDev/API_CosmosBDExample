using API_CosmosBDExample.Models;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
                    builder =>
                    {
                        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
                        //builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "WebApplication1", Version = "v1" });
});

builder.Services.AddHttpClient();
//Insertamos los servicios de Cosmos , inyeccion de la interfaz Cosmos
builder.Services.AddSingleton<ICosmosDbService>(options =>
{
    string URL = builder.Configuration.GetSection("CosmosDb")
    .GetValue<string>("Account");

    string primaryKey = builder.Configuration.GetSection("CosmosDb")
    .GetValue<string>("Key");

    string dbname = builder.Configuration.GetSection("CosmosDb")
    .GetValue<string>("DatabaseName");

    string containerName = builder.Configuration.GetSection("CosmosDb")
    .GetValue<string>("ContainerName");


    var cosmosClient = new CosmosClient(URL, primaryKey, new CosmosClientOptions() { ConnectionMode = ConnectionMode.Gateway });

    return new CosmosDbService(cosmosClient, dbname, containerName);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_CosmosBDExample v1"));
}

app.UseHttpsRedirection();
app.UseCors("MyPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
