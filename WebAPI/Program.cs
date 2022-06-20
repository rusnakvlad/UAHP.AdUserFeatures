using Application;
using MassTransit;
using Persistence;
using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region LAYERS DEPENDENCY INJECTION
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddPersistence();
#endregion

#region RABBITMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<WebAPI.Consumers.UserConsumer>();
    x.AddConsumer<WebAPI.Consumers.AdConsumer>();

    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.ReceiveEndpoint("user-queue-aduserfeatures", e =>
        {
            e.Consumer<WebAPI.Consumers.UserConsumer>(context);
        });

        config.ReceiveEndpoint("ad-queue-aduserfeatures", e =>
        {
            e.Consumer<WebAPI.Consumers.AdConsumer>(context);
        });

        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddMassTransitHostedService();
#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
