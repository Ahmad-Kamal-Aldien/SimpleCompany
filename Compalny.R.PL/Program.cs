using Company.R.BLL;
using Company.R.BLL.Interfaces;
using Company.R.BLL.Repositorys;
using Company.R.DAL.DBContexts;
using Company.R.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Compalny.R.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CompanyDBContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("defaultconnection"));
            });
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


            //To Use All AutoMapper Profile
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CompanyDBContext>()
                .AddDefaultTokenProviders();


            builder.Services.ConfigureApplicationCookie(c =>
            {
                c.LoginPath = "/Account/SignIn";
                //c.AccessDeniedPath = "/Account/AccessDenied";
            });

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



            //app.MapControllerRoute(
            //	name: "default",
            //	pattern: "{controller=Account}/{action=SignUp}/{id?}");

            app.Run();
        }
    }
}
