using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TransportManagemnetSystem.Areas.Admin.Interface;
using TransportManagemnetSystem.Areas.Admin.Repositories;
using TransportManagemnetSystem.Data;
using TransportManagemnetSystem.Models;
using TransportManagemnetSystem.Security;
using TransportManagemnetSystem.Utilities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<TransportDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ContextConnection")));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<TransportDbContext>().AddDefaultTokenProviders().AddDefaultUI();
//builder.Services.Configure<IUserClaimsPrincipalFactory>
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsDetails>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<ConfigureSmsModel>(builder.Configuration.GetSection("SmsSettings"));



builder.Services.AddAuthentication();
//builder.Services.AddMvc();
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
    options.Filters.Add(typeof(CustomAuthorizeAttribute));
    //options.Filters.Add(new AuthorizeFilter());

});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "TransportCookieName" + DateTime.Now.ToString("yyyyMMdd");
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    options.LoginPath = "/Identity/Account/Login";
    // ReturnUrlParameter requires 
    //using Microsoft.AspNetCore.Authentication.Cookies;
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});


//register here 
builder.Services.AddScoped<IUtility, Utility>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IEmailSender, SystemSendMail>();
builder.Services.AddScoped<IOrganizationDeatais, OrganizationDeatailRepository>();
builder.Services.AddScoped<ICommon, CommonRepository>();
builder.Services.AddScoped<IUserControl, UserControlRepository>();
builder.Services.AddScoped<IVehicleDarta, VehicleDartaRepository>();
builder.Services.AddScoped<IRoute, RouteRepository>();
builder.Services.AddScoped<IMember, MemberRepository>();
builder.Services.AddScoped<IReport, ReportRepository>();
builder.Services.AddScoped<IDashboard, DashboardRepository>();
builder.Services.AddScoped<IDriverDetail, DriverDetailRepository>();

builder.Services.AddScoped<LoggingService>();
builder.Services.AddHostedService<NotificationService>();

//end

builder.Services.AddAuthorization();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});


builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger());



var app = builder.Build();
app.Logger.LogInformation("Initilize Project");

//roles and user
using (var scope = app.Services.CreateScope())
{
    CreateRolesAndAdminUser(scope.ServiceProvider);
    AddRequiredData(scope.ServiceProvider);
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

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "Areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();

#region Create Role and initial User Rergistered
void CreateRolesAndAdminUser(IServiceProvider serviceProvider)
{
    var roleNames = new List<string>
            {
               UserRoles.Administrator,
                UserRoles.SuperAdmin,
                UserRoles.Admin,
                UserRoles.SubAdmin,
                UserRoles.OrgUser,
                UserRoles.User,
            };
    // Creating roles
    foreach (var role in roleNames) { CreateRole(serviceProvider, role); }

    // Administrator user Setup
    const string administratorUserEmail = "softech@gmail.com";
    const string administratorPwd = "Softech@123!";
    AddUserToRole(serviceProvider, new ApplicationUser()
    {
        FullName = UserRoles.Administrator,
        Email = administratorUserEmail,
        UserName = administratorUserEmail,
        EmailConfirmed = true,
        LockoutEnabled = false,
    }, administratorPwd, UserRoles.Administrator);

    // For Super admin 
    const string superAdminUserEmail = "superadmin@gmail.com";
    const string superAdminPwd = "Softech@123";
    AddUserToRole(serviceProvider, new ApplicationUser()
    {
        FullName = UserRoles.SuperAdmin,
        Email = superAdminUserEmail,
        UserName = superAdminUserEmail,
        EmailConfirmed = true,
    }, superAdminPwd, UserRoles.SuperAdmin);

}

/// <summary>
/// Create a role if not exists.
/// </summary>
/// <param name="serviceProvider">Service Provider</param>
/// <param name="roleName">Role Name</param>
void CreateRole(IServiceProvider serviceProvider, string roleName)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    var roleExists = roleManager.RoleExistsAsync(roleName);
    roleExists.Wait();

    if (roleExists.Result) return;
    var roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
    roleResult.Wait();
}

/// <summary>
/// Add user to a role if the user exists, otherwise, create the user and adds him to the role.
/// </summary>
/// <param name="serviceProvider">Service Provider</param>
/// <param name="user"></param>
/// <param name="userPwd">User Password. Used to create the user if not exists.</param>
/// <param name="roleName">Role Name</param>
void AddUserToRole(IServiceProvider serviceProvider, ApplicationUser user, string userPwd, string roleName)
{
    var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    Task<ApplicationUser> checkAppUser = userManager.FindByEmailAsync(user.Email);
    checkAppUser.Wait();

    ApplicationUser appUser = checkAppUser.Result;

    if (checkAppUser.Result == null)
    {
        Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(user, userPwd);
        taskCreateAppUser.Wait();

        if (taskCreateAppUser.Result.Succeeded)
        {
            appUser = user;
        }
    }

    Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
    newUserRole.Wait();
}
#endregion

#region AddRequiredData
void AddRequiredData(IServiceProvider serviceProvider)
{
    using var context = serviceProvider.GetService<TransportDbContext>();
    #region VehicleCategory
    var listOfVehicleCategory = new List<VehicleCategory>
        {
            new VehicleCategory() { Id = 1, Name= "Two Wheeler"},
            new VehicleCategory() { Id = 2, Name= "Three Wheeler"},
            new VehicleCategory() { Id = 3, Name= "Four Wheeler"}
        };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[VehicleCategory] On");
            foreach (var item in listOfVehicleCategory)
            {
                if (context.VehicleCategory.Any(x => x.Id == item.Id)) continue;
                context.VehicleCategory.Add(item);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[VehicleCategory] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"VehicleCategoryNot Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region VehicleType
    var listOfVehicleType = new List<VehicleType>
        {
            new VehicleType() { Id = 1, Name= "Bus", VehicleCategoryId = 3}
        };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[VehicleType] On");
            foreach (var item in listOfVehicleType)
            {
                if (context.VehicleType.Any(x => x.Id == item.Id)) continue;
                context.VehicleType.Add(item);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[VehicleType] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"VehicleTypeNot Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region ManufactureCompany
    var listOfManufactureCompany = new List<ManufactureCompany>
        {
            new ManufactureCompany() { Id = 1, Name= "Honda"},
            new ManufactureCompany() { Id = 2, Name= "Tata"},
            new ManufactureCompany() { Id = 3, Name= "BMW"}
        };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ManufactureCompany] On");
            foreach (var item in listOfManufactureCompany)
            {
                if (context.ManufactureCompany.Any(x => x.Id == item.Id)) continue;
                context.ManufactureCompany.Add(item);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[ManufactureCompany] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"ManufactureCompanyNot Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region EngineType
    var listOfEngineType = new List<EngineType>
        {
            new EngineType() { Id = 1, Name= "Petrol"},
            new EngineType() { Id = 2, Name= "Diseal"},
            new EngineType() { Id = 3, Name= "Electric"}
        };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[EngineType] On");
            foreach (var item in listOfEngineType)
            {
                if (context.EngineType.Any(x => x.Id == item.Id)) continue;
                context.EngineType.Add(item);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[EngineType] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"EngineTypeNot Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region StandardType
    var listOfStandardType = new List<StandardType>
        {
            new StandardType() { Id = 1, Name= "Delux"},
            new StandardType() { Id = 2, Name= "A/C"}
        };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[StandardType] On");
            foreach (var item in listOfStandardType)
            {
                if (context.StandardType.Any(x => x.Id == item.Id)) continue;
                context.StandardType.Add(item);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[StandardType] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"StandardTypeNot Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region Gender
    var listOfGender = new List<Gender>
        {
            new Gender() { Id = 1, Name_en= "Male", Name= "पुरूष" },
            new Gender() { Id = 2, Name_en= "Female", Name= "महिला"},
            new Gender() { Id = 3, Name_en= "Others", Name= "अन्य"}
        };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Gender] On");
            foreach (var item in listOfGender)
            {
                if (context.Gender.Any(x => x.Id == item.Id)) continue;
                context.Gender.Add(item);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Gender] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"GenderNot Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
    #region DrivingLicense
    var listOfDrivingLicense = new List<DrivingLicense>
        {
            new DrivingLicense() { Id = 1, Name= "A", CatName= "Motorcycle, Scooter, Moped" },
            new DrivingLicense() { Id = 2, Name= "B", CatName="Car, Jeep, Delivery Van"},
            new DrivingLicense() { Id = 3, Name= "C", CatName = "Tempo, Auto Rickshaw"},
            new DrivingLicense() { Id = 4, Name= "C1", CatName = "E-Rickshaw"},
            new DrivingLicense() { Id = 5, Name= "D", CatName = "Power Tiller"},
            new DrivingLicense() { Id = 6, Name= "E", CatName = "Tractor"},
            new DrivingLicense() { Id = 7, Name= "F", CatName = "Minibus, Minitruck"},
            new DrivingLicense() { Id = 8, Name= "G", CatName = "Truck, Bus, Lorry"},
            new DrivingLicense() { Id = 9, Name= "H", CatName = "Road Roller, Dozer"},
            new DrivingLicense() { Id = 10, Name= "H1", CatName = "Dozer"},
            new DrivingLicense() { Id = 11, Name= "H2", CatName = "Road Roller"},
            new DrivingLicense() { Id = 12, Name= "I", CatName = "Crane, Fire Brigade, Loader"},
            new DrivingLicense() { Id = 13, Name= "I1", CatName = "Crane"},
            new DrivingLicense() { Id = 14, Name= "I2", CatName = "Fire Brigade"},
            new DrivingLicense() { Id = 15, Name= "I3", CatName = "Loader"},
            new DrivingLicense() { Id = 16, Name= "J1", CatName = "Excavator"},
            new DrivingLicense() { Id = 17, Name= "J2", CatName = "Backhoe Loader"},
            new DrivingLicense() { Id = 18, Name= "J3", CatName = "Grader"},
            new DrivingLicense() { Id = 19, Name= "J4", CatName = "Forklift"},
            new DrivingLicense() { Id = 20, Name= "J5", CatName = "Other"},
            new DrivingLicense() { Id = 21, Name= "K", CatName = "Scooter, Moped"}
        };
    using (var transaction = context.Database.BeginTransaction())
    {
        try
        {
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[DrivingLicense] On");
            foreach (var item in listOfDrivingLicense)
            {
                if (context.DrivingLicense.Any(x => x.Id == item.Id)) continue;
                context.DrivingLicense.Add(item);
                context.SaveChanges();
            }
            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[DrivingLicense] Off");
            transaction.Commit();
        }
        catch (Exception ex)
        {
            app.Logger.LogInformation($"DrivingLicenseNot Created, Error : {ex}");
            transaction.Rollback();
        }
    }
    #endregion
}
#endregion