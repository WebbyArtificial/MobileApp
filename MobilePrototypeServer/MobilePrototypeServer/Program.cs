using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MobilePrototypeServer.Client.Pages;
using MobilePrototypeServer.Components;
using MobilePrototypeServer.Components.Account;
using MobilePrototypeServer.Data;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{

    var services = scope.ServiceProvider;

    //var local = services.GetRequiredService<ILocalStorageService>();
    //var res = await local.GetItemAsync<string>("culture");

    var contextCore = services.GetRequiredService<ApplicationDbContext>();
    //var userManager = services.GetService<UserManager<ApplicationUser>>();
    //var roleManager = services.GetService<RoleManager<IdentityRole>>();
    //var userStore = services.GetService<IUserStore<ApplicationUser>>();



    if (contextCore.Database.IsSqlServer())
    {
        await contextCore.Database.MigrateAsync();


        //IdentityRole roleAdmin = new IdentityRole()
        //{
        //    Name = "Admin",
        //    NormalizedName = "ADMIN"
        //};

        //IdentityRole roleDip = new IdentityRole()
        //{
        //    Name = "Dipendente",
        //    NormalizedName = "DIPENDENTE"
        //};
        //await roleManager.CreateAsync(roleAdmin);
        //await roleManager.CreateAsync(roleDip);


        //DbSeeder seeder = new DbSeeder(contextCore, userManager, userStore);

        //await seeder.SeedDb();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(MobilePrototypeServer.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
