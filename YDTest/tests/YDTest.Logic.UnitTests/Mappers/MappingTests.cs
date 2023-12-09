using AutoFixture;
using AutoMapper;
using FluentAssertions;
using YDTest.Api.Configuration;
using YDTest.Data.Entities;
using YDTest.Model.Api;

namespace YDTest.Logic.UnitTests.Mappers;

public class MappingTests : BaseUnitTest
{
    private readonly IMapper _mapper;

    public MappingTests()
    {
        var mapperConfiguration = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new UserProfile());
            mc.AddProfile(new TeamProfile());
            mc.AddProfile(new UserTeamProfile());
        });
        _mapper = new Mapper(mapperConfiguration);
    }

    [Test]
    public void Map_CreateUserRequest2Dto_DtoShouldBeEqualEntity()
    {
        // Arrange
        var source = Fixture.Create<CreateUserRequest>();

        // Act
        var destination = _mapper.Map<User>(source);

        // Assert
        destination.Should().NotBeNull();
        destination.City.Should().Be(source.City);
        destination.Email.Should().Be(source.Email);
        destination.Name.Should().Be(source.Name);
        destination.LastName.Should().Be(source.LastName);
        destination.Birth.Should().Be(source.Birth);
    }
}
