using ADOTaskbook.DataHelper;

var builder = WebApplication.CreateBuilder(args);
//Adds appsettings.json as a Configuration Source
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("myConnection");

builder.Services.AddScoped(_ => new ConnectionHelper(connectionString));

var app = builder.Build();

//var connectionHelper = new ConnectionHelper(connectionString);


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();
//app.MapRazorPages();

app.Run();
