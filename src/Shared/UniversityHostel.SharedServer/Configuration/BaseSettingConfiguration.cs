namespace CommonLibrary.Server
{
    public class BaseSettingConfiguration<TEntity> : BaseConfiguration<TEntity>
        where TEntity : BaseSettingEntity
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.NameSecondLanguage).IsRequired();
        }
    }
}

