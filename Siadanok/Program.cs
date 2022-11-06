using Microsoft.EntityFrameworkCore.Design;
using DataBase;
using Microsoft.EntityFrameworkCore;
using CRUD.Interfaces;
using CRUD;
using CRUD.Implementation;
using Siadanok.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EFDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddScoped<DataManager>();
builder.Services.AddScoped<Service>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<EFDBContext>();
    //InitdData.Initialize(services);
    InitdData.Init(context);
}

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
