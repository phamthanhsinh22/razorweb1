using Microsoft.EntityFrameworkCore;
using razorweb.models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//đăng kí MyBlogContext như là một dịch vụ của ứng dụng
builder.Services.AddDbContext<MyBlogContext>(options=>{
    string connectString = builder.Configuration.GetConnectionString("MyBlogContext");
    options.UseSqlServer(connectString);
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

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

/*
    CREATED, READ, UPDATE, DELETE (CRUD)
    dotnet aspnet-codegenerator razorpage -m Article -dc MyBlogContext -outDir Pages/Blog -udl --referenceScriptLibraries
*/