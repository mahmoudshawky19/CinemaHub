using CinemaHub.Repository;
using DataAccess;
using DataAccess.IRepository;
using DataAccess.Repository;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddIdentity<IdentityUser, IdentityRole>
               (options => options.SignIn.RequireConfirmedAccount = false)
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultUI()
               .AddDefaultTokenProviders();
builder.Services.AddDbContext<ApplicationDbContext>(
option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
 );


builder.Services.AddScoped < ICinemaHallReposirory, CinemaHallReposirory>();
builder.Services.AddScoped < IBookingseatsRepository, BookingseatsRepository>();
builder.Services.AddScoped < IFavoriteMovieRepository, FavoriteMovieRepository>();
builder.Services.AddScoped < IStaffRepository, StaffRepository>();
builder.Services.AddScoped < IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IActorMovieRepository, ActorMovieRepository>();
builder.Services.AddScoped<IActorRepository, ActorReposirory>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICinemaRepository, CinemaRepository>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
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
app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
            pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
