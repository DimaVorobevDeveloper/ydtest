namespace YDTest.Model.Abstractions;

public class UserBaseDto
{
    public Guid Id { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Deleted { get; set; }

    public bool? IsDeleted { get; set; }
}
