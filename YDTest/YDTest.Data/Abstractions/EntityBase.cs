namespace YDTest.Data.Abstractions;

public class EntityBase
{
    public string Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Deleted { get; set; }

    public string IsDeleted { get; set; }
}
