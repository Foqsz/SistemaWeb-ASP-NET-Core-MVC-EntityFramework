using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using System.Configuration;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<SalesWebMvcContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("SalesWebMvcContext"),
    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("SalesWebMvcContext")),
    builder => builder.MigrationsAssembly("SalesWebMvc")));

builder.Services.AddScoped<SeedingService>();
builder.Services.AddScoped<SellerService>();
builder.Services.AddScoped<DepartamentService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} 

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Injetando o SeedingService no pipeline de solicitação
// Aqui nós estamos dizendo para o aplicativo executar um pedaço de código somente se uma certa condição for verdadeira.
app.MapWhen(context =>
{
    // Aqui, estamos verificando em qual ambiente o aplicativo está rodando, se é "desenvolvimento" ou não.
    var env = context.RequestServices.GetRequiredService<IWebHostEnvironment>();

    // Se o aplicativo estiver em ambiente de "desenvolvimento"...
    if (env.IsDevelopment())
    { 
        // Aqui, estamos dizendo para o aplicativo pegar um serviço que semeia dados e chamar um método nele chamado "Seed".
        var seedingService = context.RequestServices.GetRequiredService<SeedingService>();
        seedingService.Seed();
    }

    // Aqui estamos dizendo que, independentemente de estar em ambiente de desenvolvimento ou não,
    // não vamos interromper o fluxo normal do aplicativo, então estamos retornando "false".
    return false;
}, app => { });
  
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
