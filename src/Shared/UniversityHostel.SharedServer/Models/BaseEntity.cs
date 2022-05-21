namespace CommonLibrary.Server;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreationDate { get; set; }
    public byte[] ConcurrencyStamp { get; set; }
    public int DisplayOrder { get; set; }
}
