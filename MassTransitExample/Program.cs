using MassTransit;
using MassTransitExample;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
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

        // Configuração do Consumer
        cfg.ReceiveEndpoint("my-queue", e =>
        {
            e.ConfigureConsumer<MyMessageConsumer>(context);
        });
    });

    // Registrando o Consumer
    x.AddConsumer<MyMessageConsumer>();
});

// Registrar o MessageProducer como um serviço Singleton
builder.Services.AddScoped<MessageProducer>();

var app = builder.Build();

// Configuração do pipeline de requisição
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
