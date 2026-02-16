using Keycloak.AuthServices.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
Assembly[] assemblies = [
    typeof(CatalogModule).Assembly, 
    typeof(BasketModule).Assembly,
    typeof(OrderingModule).Assembly,
];
builder.Services
    .AddCarterWithAssemblies(assemblies)
    .AddMediatRWithAssemblies(assemblies)
    .AddMassTransitWithAssemblies(builder.Configuration, assemblies);

builder.Services.AddStackExchangeRedisCache(opt =>
{
   opt.Configuration = builder.Configuration.GetConnectionString("Redis");
    opt.InstanceName = "RedisCache";
});

builder.Services.AddKeycloakWebApiAuthentication(builder.Configuration);
builder.Services.AddAuthorization();

// Application services
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);

builder.Services
    .AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(opt => { });
app.UseAuthentication();
app.UseAuthorization();

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.Run();
