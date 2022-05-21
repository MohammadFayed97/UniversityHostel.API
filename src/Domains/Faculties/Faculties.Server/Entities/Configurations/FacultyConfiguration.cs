namespace Faculties.Server.Entities.Configurations;

public class FacultyConfiguration : BaseSettingConfiguration<Faculty>
{
    public override void Configure(EntityTypeBuilder<Faculty> builder) => base.Configure(builder);
}
