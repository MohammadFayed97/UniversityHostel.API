namespace Account.Server.Entities.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(e => e.FullName).HasMaxLength(100);

        var superAdmin = new AppUser
        {
            Id = new Guid("e757f10f-c177-42f1-9242-0656163a43db").ToString(),
            UserName = "Admin",
            FullName = "SuperAdmin",
            NormalizedUserName = "Admin".ToUpper(),
            Email = "Admin@TantaUniversity.edu.eg",
            EmailConfirmed = true,
        };
        superAdmin.PasswordHash = new PasswordHasher<AppUser>().HashPassword(superAdmin, "12345");
        
        builder.HasData(superAdmin);
    }
}
