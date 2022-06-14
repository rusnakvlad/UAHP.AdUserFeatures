using Application;
using MassTransit;
using Persistence;
using WebAPI.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region LAYERS DEPENDENCY INJECTION
builder.Services.AddApplication();
builder.Services.AddPersistence();
#endregion

#region RABBITMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserConsumer>();
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(new Uri("rabbitmq://localhost"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.ReceiveEndpoint("user-queue", e =>
        {
            e.Consumer<UserConsumer>(context);
        });
        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddMassTransitHostedService();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
