using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Business;
using ProjectManagement.Business.Services;
using ProjectManagement.Business.Services.Interfaces;
using ProjectManagement.Data;
using ProjectManagement.Data.Repositories;
using ProjectManagement.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ProjectContext>((options) =>
{
    options.UseCosmos(
builder.Configuration.GetValue<string>("Azure:CosmosDb:Uri"),
builder.Configuration.GetValue<string>("Azure:CosmosDb:Key"),
builder.Configuration.GetValue<string>("Azure:CosmosDb:DatabaseName")
);
});
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();

builder.Services.AddIdentity<User, IdentityRole<string>>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
})
    .AddEntityFrameworkStores<ProjectContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(120);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}");

});


app.Run();
