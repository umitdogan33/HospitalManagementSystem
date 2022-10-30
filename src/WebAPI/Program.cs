using Application;
using Application.Common.Extensions;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Infrastructure;
using Persistence;
using WebAPI.Configuration.Autofac;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacModule()));
//builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddJWT(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();