using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TaxPayment.Common.TaxSetup;
using TaxPayment.Models;
using TaxPayment.Repository.DapperDao;
using TaxPayment.Repository.GenericRepository;
using TaxPaymet.Business.Setup.TaxSetup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<TaxSystemContext>(options => options.UseSqlServer("DefaultConnection"));


//Business
builder.Services.AddScoped<ITaxSetupBusiness, TaxSetupBusiness>();


//Repository
builder.Services.AddScoped<IDapperDao, DapperDao>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();


//Mapper

var config = new MapperConfiguration(cfg =>
         {
             //Create all maps here
             cfg.CreateMap<TaxSetupViewModel, TaxSetupParam>();
         });
IMapper mapper = config.CreateMapper();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
