namespace Account.Server.Entities.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(new IdentityRole
        {
            Id = new Guid("48c665f5-a9e5-4ebe-a9d3-d60af4006fe5").ToString(),
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
        new IdentityRole
        {
            Id = new Guid("bc1521d7-3949-46d1-9c61-8f07ab249bea").ToString(),
            Name = "User",
            NormalizedName = "USER"
        });
    }
}
