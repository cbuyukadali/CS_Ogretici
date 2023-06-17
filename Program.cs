using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Identity.Client;

namespace CS_Ogretici
{
    public class Program
    {
        public static void Main(string[] args)
        {        
            
            var builder = WebApplication.CreateBuilder(args);

            
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(); // Cookies lerin oturum suresine bagli olarak calismasi icin eklendi.

           // builder.Services.AddAuthorization(options =>
           // {
           //     options.AddPolicy("AdminTurunde", policy => policy.RequireRole("SuperAdmin","Admin","Normal"));
           // });



            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    //Kullanici x=5 dakika herhangi bir islem yapmazsa sistem oturumu sonlandirilir.
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.Cookie.HttpOnly = true;
                    
                    //options.Cookie.IsEssential = true; //bir sayfada eklenmisti, ne ise yarar?
                    //Cookie eklentisinin sonu
                    
                    //site icinde autorize sahibi olmayan birisi giris yaptiginda login sayfasina yonlendir
                    options.LoginPath = "/Home/Login/";
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

            app.UseSession(); // Cookies lerin oturum suresine bagli olarak calismasi icin eklendi.

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization(); 

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Basliklar}/{action=Hosgeldiniz}/{id?}");

           // app.Use(async (context, next) =>
           // {
           //     await next();
           //     if (context.Response.StatusCode == 404)
           //     {
           //         context.Request.Path = "/Basliklar/Hosgeldiniz";
           //         await next();
           //     }
           // });

            app.Run();

        }

    }
}