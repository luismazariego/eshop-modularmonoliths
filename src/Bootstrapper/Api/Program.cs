var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

// Add services to the container.
Assembly[] assemblies = [
    typeof(CatalogModule).Assembly, 
    typeof(BasketModule).Assembly
];
builder.Services
    .AddCarterWithAssemblies(assemblies)
    .AddMediatRWithAssemblies(assemblies)
    .AddMassTransitWithAssemblies(assemblies);

builder.Services.AddStackExchangeRedisCache(opt =>
{
   opt.Configuration = builder.Configuration.GetConnectionString("Redis");
    opt.InstanceName = "RedisCache";
});

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

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.Run();
