using MassTransit;
using MassTransitExample;

var builder = WebApplication.CreateBuilder(args);

// Adicionar servi�os ao cont�iner.
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Configura��o do Consumer
        cfg.ReceiveEndpoint("my-queue", e =>
        {
            e.ConfigureConsumer<MyMessageConsumer>(context);
        });
    });

    // Registrando o Consumer
    x.AddConsumer<MyMessageConsumer>();
});

// Registrar o MessageProducer como um servi�o Singleton
builder.Services.AddScoped<MessageProducer>();

var app = builder.Build();

// Configura��o do pipeline de requisi��o
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
