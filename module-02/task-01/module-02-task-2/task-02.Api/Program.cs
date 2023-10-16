using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Module_02.Task_02.CatalogService.Abstractions.DB;
using Module_02.Task_02.CatalogService.Abstractions.DB.DatabaseContext;
using Module_02.Task_02.CatalogService.BLL.CQRS.Base;
using Module_02.Task_02.CatalogService.DAL.Context;
using Module_02.Task_02.CatalogService.WebApi.ActionFilters;
using Module_02.Task_02.CatalogService.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(options => { options.CustomSchemaIds(type => type.FullName?.Split('.').Last().Replace("+", ".")); });

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


var app = builder.Build();


using (var serviceScope = app.Services.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<IWithModificationsDbContext>();
    //dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();