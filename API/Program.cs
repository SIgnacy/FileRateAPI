using API.Extensions;
using API.Middlewares.ExceptionHandlers;
using Application;
using Domain.Repositories;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Persistence.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddFileRateContext(
    builder.Configuration.GetConnectionString("SqlServer"));

builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IKeywordRepository, KeywordRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(cfg 
    => cfg.RegisterServicesFromAssembly(ApplicationLayerAssemblyReference.Assembly));

builder.Services.AddExceptionHandler<NotFoundExceptionHandler>();
builder.Services.AddExceptionHandler<ArgumentExceptionHandler>();
builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddProblemDetails();


var app = builder.Build();

app.UseExceptionHandler();

app.MapEndpoints();

app.UseAntiforgery();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
