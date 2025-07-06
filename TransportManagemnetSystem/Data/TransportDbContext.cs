using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TransportManagemnetSystem.Data
{
    public class TransportDbContext : IdentityDbContext<ApplicationUser>
    {
        public TransportDbContext(DbContextOptions<TransportDbContext> options)
           : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<FiscalYear> FiscalYear { get; set; }
        public DbSet<State> State { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Palika> Palika { get; set; }
        public DbSet<OrganizationDetail> OrganizationDetail { get; set; }
        public DbSet<OuterUserRegistration> OuterUserRegistration { get; set; }
        public DbSet<VehicleCategory> VehicleCategory { get; set; }
        public DbSet<VehicleType> VehicleType { get; set; }
        public DbSet<ManufactureCompany> ManufactureCompany { get; set; }
        public DbSet<EngineType> EngineType { get; set; }
        public DbSet<StandardType> StandardType { get; set; }
        public DbSet<VehicleDarta> VehicleDarta { get; set; }
        public DbSet<VehicleAssignedData> VehicleAssignedData { get; set; }
        public DbSet<VehicleDartaApprove> VehicleDartaApprove { get; set; }
        public DbSet<bluebookRouteBima> bluebookRouteBima { get; set; }
        public DbSet<BusRoute> BusRoute { get; set; }
        public DbSet<StopSetup> StopSetup { get; set; }
        public DbSet<BusOutRoute> BusOutRoute { get; set; }
        public DbSet<OutStopSetup> OutStopSetup { get; set; }
        public DbSet<Member> Member { get; set; }
        public DbSet<VehicleDartaAddImage> VehicleDartaAddImage { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<BookedSeat> BookedSeat { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<DriverDetails> DriverDetails { get; set; }
        public DbSet<DriverLicenseCategory> DriverLicenseCategory { get; set; }
        public DbSet<DrivingLicense> DrivingLicense  { get; set; }
        public DbSet<BusconductorDetails> BusconductorDetails { get; set; }

        public DbSet<UserLog> UserLog { get; set; }
        public DbSet<OrganizationUserDetails> OrganizationUserDetails { get; set; }
        public DbSet<FAQ> FAQ { get; set; }
    }
}
