using FrontEndWebApplication.Repository.Interfaces;
using FrontEndWebApplication.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Habilitar el cliente Http
builder.Services.AddHttpClient();


builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IProductRepository,  ProductRepository>();

//Agregamos par�metros de autenticaci�n
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.LoginPath = "/Home/Login";
        options.AccessDeniedPath = "/Home/AccessDenied";
        options.SlidingExpiration = true;
    });

//Agregamos par�metros de sesi�n
builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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

//Damos Soporte Cors
app.UseCors(c => c
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
// Agregamos Session & uthentication
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();


app.UseExceptionHandler("/Home/Error"); // P�gina de errores
app.UseStatusCodePagesWithRedirects("/Home/Error/{0}"); // P�gina de errores con c�digo de estado
app.UseStatusCodePagesWithRedirects("/Account/Login"); // P�gina de inicio de sesi�n


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
