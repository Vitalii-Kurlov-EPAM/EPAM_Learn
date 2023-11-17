using Microsoft.AspNetCore.Mvc;
using Module_02.Task_01.CartingService.WebApi.ActionFilters;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;
using Module_02.Task_01.CartingService.WebApi.Configurations;
using Module_02.Task_01.CartingService.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
    
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddKeyCloakAuth(builder.Configuration);
services.AddVersioning();
services.AddSwagger();
services.AddFluentValidation();
services.AddMediatR(c => { c.RegisterServicesFromAssemblyContaining<IDbContext>(); });

services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressMapClientErrors = true;
});

services.Configure<MvcOptions>(options =>
{
    options.Filters.Add(typeof(ModelValidationActionFilter));
    options.Filters.Add(typeof(ExceptionHandlerActionFilter));
});

services.AddSingleton<IDbConnectionSettings>(_ => new DbConnectionSettings
{
    ConnectionString = builder.Configuration.GetConnectionString("lite-db-connection")
});

services.AddScoped<IDbContext, DbContext>();

services.AddRabbitMq();

var app = builder.Build();

app.UseJwtAccessTokenLogger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
