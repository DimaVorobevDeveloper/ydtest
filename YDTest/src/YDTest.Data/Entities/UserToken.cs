using YDTest.Data.Abstractions;
using YDTest.Model.Enums;

namespace YDTest.Data.Entities;

public class UserToken : EntityBase
{
    public Guid UserId { get; set; }

    public string Token { get; set; }

    public TokenType Type { get; set; }

    public DateTime Expired { get; set; }

    public User User { get; set; }
}
