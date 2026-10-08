using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("QLMuaHangVaNCC_UNETI06_TI17A1HNContext") ?? throw new InvalidOperationException("Connection string 'QLMuaHangVaNCC_UNETI06_TI17A1HNContext' not found.");

builder.Services.AddDbContext<QLMuaHangVaNCC_UNETI06_TI17A1HNContext>(options => options.UseSqlServer(connectionString));


// Add services to the container.
builder.Services.AddControllersWithViews();

// Session dùng cho đăng nhập và phân quyền (PhanQuyenAttribute)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();          // phải đặt SAU UseRouting, TRƯỚC UseAuthorization
app.UseAuthorization();


app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();