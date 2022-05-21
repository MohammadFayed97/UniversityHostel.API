namespace CommonLibrary.Server;

public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        
        builder.Property<Guid>(e => e.Id).HasValueGenerator<GuidValueGenerator>();
        builder.Property(e => e.CreationDate).HasDefaultValueSql("getDate()");
        builder.Property(e => e.ConcurrencyStamp).IsRowVersion();
    }
}
