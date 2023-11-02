using FluentAssertions;
using Moq;
using Xunit.Abstractions;
using YDTest.Data;
using YDTest.Data.Entities;
using YDTest.Logic.UnitTests.Mocks;
using YDTest.Model;

namespace YDTest.Logic.UnitTests;

public class UserLogicTests : BaseUnitTests
{
    private static readonly string[] Names = new[]
    {
        "Василий", "Василий", "Иван", "Виктор", "Василий", "Виктор", "Balmy", "Владислав", "Виктор", "Сергей"
    };

    private static readonly string[] Emails = new[]
    {
        "video.1kito@gmail.com", "photo.1kito@gmail.com", "ramil.1kito@gmail.com", "sushi.1kito@gmail.com", "video.1kito@gmail.com"
    };

    private static readonly string[] Cities = new[]
    {
        "Зеленодольск", "Казань", "Саратов", "Набережные челны", "Саратов", "Саратов", "Саратов", "Саратов", "Саратов", "Саратов"
    };

    //public UserLogicTests(ITestOutputHelper output) : base(output)
    //{
    //}

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetUsers_Count_Success()
    {
        var userLogic = new UserLogic(null);
        var users = userLogic.GetUsers();
        var count = users.Count;
        var expectedUsersCount = 3;
        Assert.That(count, Is.EqualTo(expected: expectedUsersCount));
        count.Should().Be(expectedUsersCount);
        Assert.Pass();
    }

    [Test]
    public void GetUsers_Count_Fail()
    {
        var t1 = Enumerable.Range(1, 3).Select(index => new User
        {
            Name = Names[Random.Shared.Next(Names.Length)],
            Birth = DateTime.Now.AddYears(-Random.Shared.Next(10, 55)),
            Email = Emails[Random.Shared.Next(Emails.Length)],
            City = Cities[Random.Shared.Next(Cities.Length)],
        }).ToList();

        var t = CreateContextMock(t1.ToArray());

        var userLogic = new UserLogic(t);
        var users = userLogic.GetUsers();
        var count = users.Count;
        var expectedUsersCount = 3;
        Assert.That(count, Is.EqualTo(expected: expectedUsersCount));
        count.Should().Be(expectedUsersCount);
        // Assert.Pass();
    }


    protected YDTestContext CreateContextMock(User[] set)
    {
        var output = Mock.Of<ITestOutputHelper>();
        var fakeDbContext = new FakeDbContext(output);
        fakeDbContext.UseSet(set);
        fakeDbContext.Object.ChangeTracker.Clear();
        return fakeDbContext.Object;
    }
}