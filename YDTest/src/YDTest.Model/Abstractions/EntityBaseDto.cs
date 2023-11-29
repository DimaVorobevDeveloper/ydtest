namespace YDTest.Model.Abstractions;

public class EntityBaseDto
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime Modified { get; set; }
    
    public DateTime? Deleted { get; set; }

    public bool? IsDeleted { get; set; }
}
