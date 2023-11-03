using MessageBroker.RabbitMq.Extensions;
using Microsoft.AspNetCore.Mvc;
using Module_02.Task_01.CartingService.WebApi.ActionFilters;
using Module_02.Task_01.CartingService.WebApi.Layers.Abstractions.DB;
using Module_02.Task_01.CartingService.WebApi.Layers.DAL.Context;
using Module_02.Task_01.CartingService.WebApi.Configurations;
using Module_02.Task_01.CartingService.WebApi.BackgroundServices;
using RabbitMQ.Client;
using MessageBroker.RabbitMq.Producer.Options;
using MessageBroker.RabbitMq.Consumer.Options;
using MessageBroker.RabbitMq.Consumer.Options.ExchangeOptions;
using MessageBroker.RabbitMq.Consumer.Options.QueueOptions;
using MessageModels.Entity;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
    
services.AddControllers();
services.AddEndpointsApiExplorer();
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


services.AddSingleton<IConnectionFactory>(_ => new ConnectionFactory
{
    Uri = new Uri("amqp://guest:guest@127.0.0.1:5672")
});

services.RegisterRabbitMqConsumer();
services.ConfigureConsumerOptions<IEntityModificationMessage>(options =>
{
    options.RootExchange = new ProducerExchangeOptions<IEntityModificationMessage>
    {
        AppIdentifier = "CatalogService"
    };
    options.ConnectorExchange = new ConnectorExchangeOptions<IEntityModificationMessage>
    {
        AppIdentifier = "CartingService",
        RoutingKey = "entity.*"
    };
    options.Consumers = new List<ConsumerQueueOptions<IEntityModificationMessage>>
    {
        new()
        {
            RoutingKey = "entity.update",
            AdditionalQueueNamePart = "update",
            AppIdentifier = "CartingService",
            TtlInMilliseconds = (int)TimeSpan.FromSeconds(5).TotalMilliseconds
        }
    };

    options.Dlq = new DlqProps<IEntityModificationMessage>
    {
        Exchange = new DlqExchangeOptions<IEntityModificationMessage>
        {
            AppIdentifier = "CartingService"
        },
        Queue = new DlqQueueOptions<IEntityModificationMessage>
        {
            AppIdentifier = "CartingService",
            TtlInMilliseconds = (int)TimeSpan.FromDays(7).TotalMilliseconds
        }
    };
});
services.AddHostedService<MessageConsumeBackgroundService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
