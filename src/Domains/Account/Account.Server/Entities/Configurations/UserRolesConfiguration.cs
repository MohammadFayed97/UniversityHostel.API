namespace Account.Server.Entities.Configurations;

public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        var superAdmin = new IdentityUserRole<string>()
        {
            UserId = new Guid("e757f10f-c177-42f1-9242-0656163a43db").ToString(),
            RoleId = new Guid("48c665f5-a9e5-4ebe-a9d3-d60af4006fe5").ToString(),
        };

        builder.HasData(superAdmin);
    }
}
