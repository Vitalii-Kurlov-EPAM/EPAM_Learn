using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module_02.Task_02.CatalogService.Abstractions.DB;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;
using Module_02.Task_02.CatalogService.DAL.Context;
using Module_02.Task_02.CatalogService.WebApi.ActionFilters;
using Module_02.Task_02.CatalogService.WebApi.Configurations;
using Module_02.Task_02.CatalogService.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddKeyCloakAuth(builder.Configuration);

services.AddSwagger();

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
    ConnectionString = builder.Configuration.GetConnectionString("DbConnection")
});

services.AddFluentValidationAutoValidation(configuration =>
{
    configuration.DisableDataAnnotationsValidation = false;
});
services.AddFluentValidationClientsideAdapters();

services.AddValidatorsFromAssemblyContaining<ErrorModelResponse>(ServiceLifetime.Scoped);

services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssemblyContaining<BaseHandler<IWithModificationsDbContext>>();
});


services.AddScoped<IWithModificationsDbContext, WithTrackingStudyDbContext>();
services.AddScoped<IReadOnlyDbContext, StudyReadOnlyDbContext>();

services.AddRabbitMq();


var app = builder.Build();
using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<IWithModificationsDbContext>();
    ((DbContext)dbContext).Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerService();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();