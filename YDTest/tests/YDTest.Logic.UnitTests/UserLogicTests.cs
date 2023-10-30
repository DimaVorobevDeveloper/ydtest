using FluentAssertions;

namespace YDTest.Logic.UnitTests;

public class UserLogicTests
{
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
        var userLogic = new UserLogic(null);
        var users = userLogic.GetUsers();
        var count = users.Count;
        var expectedUsersCount = 5;
        Assert.That(count, Is.EqualTo(expected: expectedUsersCount));
        count.Should().Be(expectedUsersCount);
        Assert.Pass();
    }
}