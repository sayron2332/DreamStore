using DreamStore.Api;
using DreamStore.Core;
using DreamStore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//add  lowerCase for routes
builder.Services.LowerCaseRoutes();
//add swagger with bearer login scheme
builder.Services.AddSwaggerWithJwt();

var connString = builder.Configuration.GetConnectionString("DefaultConnection");

//add database
builder.Services.AddAppDbContext(connString!);

//add add repository 
builder.Services.AddRepository();

//add jwt bearer authentication
builder.Services.AddJwtBearer(builder.Configuration);

//add core services
builder.Services.AddCoreServices();
builder.Services.AddAutoMappers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty;  
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    await SeedData.Initialize(serviceProvider);
}

app.Run();
