using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Discount.Grpc.Protos;
using MassTransit;
using RabbitMQ.Client;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
IConfiguration Configuration = builder.Configuration;
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
    (options =>
    {
        options.Address = new Uri(Configuration["GrpcSettings:DiscountUrl"]);
    });
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cnf) =>
    {
        cnf.Host(Configuration.GetValue<string>("EventBusSettings:HostAddress"));
    });
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddMassTransitHostedService();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
