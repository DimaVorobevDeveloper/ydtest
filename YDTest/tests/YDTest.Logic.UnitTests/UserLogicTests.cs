using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit.Abstractions;
using YDTest.Api.Configuration;
using YDTest.Data;
using YDTest.Data.Entities;
using YDTest.Logic.UnitTests.Mocks;

namespace YDTest.Logic.UnitTests;

public class UserLogicTests : BaseUnitTest
{
    private readonly IMapper _mapper;

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

    public UserLogicTests()
    {
        var mapperConfiguration = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new UserProfile());
            // mc.AddProfile(new DomainMappingProfile());
        });
        _mapper = new Mapper(mapperConfiguration);
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void GetUsers_Count_Success()
    {
        var users = CreateFakeUsers();
        var dbContext = CreateContextMock(users);

        var userLogic = new UserLogic(dbContext, null, _mapper);
        var usersDto = userLogic.GetUsers();
        var count = usersDto.Count;
        var expectedUsersCount = 3;
        Assert.That(count, Is.EqualTo(expected: expectedUsersCount));
        count.Should().Be(expectedUsersCount);
        Assert.Pass();
    }

    [Test]
    public void GetUsers_Count_Fail()
    {
        var users = CreateFakeUsers();
        var dbContext = CreateContextMock(users);

        var userLogic = new UserLogic(dbContext, null, _mapper);
        var usersDto = userLogic.GetUsers();
        var count = usersDto.Count;
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

    protected User[] CreateFakeUsers()
    {
        var users = Enumerable.Range(1, 3).Select(index => new User
        {
            Name = Names[Random.Shared.Next(Names.Length)],
            Birth = DateTime.Now.AddYears(-Random.Shared.Next(10, 55)),
            Email = Emails[Random.Shared.Next(Emails.Length)],
            City = Cities[Random.Shared.Next(Cities.Length)],
        });

        return users.ToArray();
    }
}