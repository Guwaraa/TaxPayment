using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TaxPayment.Common.Premium;
using TaxPayment.Common.TaxSetup;
using TaxPayment.Models;
using TaxPayment.Repository.DapperDao;
using TaxPayment.Repository.GenericRepository;
using TaxPaymet.Business.KYCDetail;
using TaxPaymet.Business.Login;
using TaxPaymet.Business.Registration;
using TaxPaymet.Business.Setup.PremiumSetup;
using TaxPaymet.Business.Setup.TaxSetup;
using TaxPaymet.Business.TaxPayement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<TaxSystemContext>(options => options.UseSqlServer("DefaultConnection"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});


//Business
builder.Services.AddScoped<ITaxSetupBusiness, TaxSetupBusiness>();
builder.Services.AddScoped<IKYCDetailBusiness, KYCDetailBusiness>();
builder.Services.AddScoped<IPremiumBusiness, PremiumBusiness>();
builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
builder.Services.AddScoped<IRegistrationBusiness, RegistrationBusiness>();
builder.Services.AddScoped<ITaxPayementBusiness, TaxPayementBusiness>();


//Repository
builder.Services.AddScoped<IDapperDao, DapperDao>();
builder.Services.AddScoped<IGenericRepository, GenericRepository>();


//Mapper

var config = new MapperConfiguration(cfg =>
         {
             //Create all maps here
             cfg.CreateMap<TaxSetupViewModel, TaxSetupParam>();
             //cfg.CreateMap<PremiumViewModel, PremiumDetailsParam>();
         });
IMapper mapper = config.CreateMapper();

var app = builder.Build();
app.UseSession();


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
