using BusinessObject.Helper;
using DataAccess.Data;
using DataAccess.IRipository.Cart;
using DataAccess.IRipository.IProducts;
using DataAccess.IRipository.IUser;
using DataAccess.Ripository.Cart;
using DataAccess.Ripository.Login;
using DataAccess.Ripository.Products;
using DataAccess.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();

builder.Services.AddHttpContextAccessor();
builder.Services.AddLibraryServices();

var mailsetting = builder.Configuration.GetSection("MailSettings");
builder.Services.Configure<EmailSetting>(mailsetting);
builder.Services.AddTransient<EmailService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddDbContext<DbBookStoreContext>(opt =>
{
    builder.Configuration.GetConnectionString("db");
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
