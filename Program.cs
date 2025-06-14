//Gabriel Ocampo Salgado
//Ingenieria en sistemas computacionales
//No control: 21670055
//10/04/2025


using MagicVillaWeb;
using MagicVillaWeb.Services;
using MagicVillaWeb.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddHttpClient<IVillaService,VillaService>();
builder.Services.AddScoped<IVillaService, VillaService>();


builder.Services.AddHttpClient<INumeroVillaService, NumeroVillaService>();
builder.Services.AddScoped<INumeroVillaService, NumeroVillaService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern:"{controller=Home}/{action=Index}/{id?}");


app.Run();
