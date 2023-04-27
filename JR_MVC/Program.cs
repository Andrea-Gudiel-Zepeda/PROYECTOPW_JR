using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(s =>

{

    s.IdleTimeout = TimeSpan.FromMinutes(20);

    s.Cookie.Name = "JR.Session"; /*You can change this variable*/

    s.Cookie.Expiration = TimeSpan.FromMinutes(20);

});



builder.Services.AddCors(options =>

{

    options.AddPolicy("AllOrigins",

        builder =>

        {

            builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();

        });

});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)

               .AddCookie(options =>

               {

                   options.Cookie.SameSite = SameSiteMode.None;

                   options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

                   options.LoginPath = "/User"; /*this option indicates where is the login page*/

               });

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/JR/error404");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=SingIn}/{id?}");

app.Run();
