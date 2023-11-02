//using System.Net;
//using System.Net.Http.Headers;
//using System.Security.Claims;
////using Abdt.DigitalProfile.DataProvider.Client.Services;
////using Abdt.DigitalProfile.DataProvider.IntegrationTests.Api.Mocks;
////using Abdt.DigitalProfile.DataProvider.Web;
//using AutoFixture;
//using FluentAssertions;
//using Microsoft.EntityFrameworkCore;
//using Xunit;

//namespace YDTest.IntegrationsTests.Api;

//public class YDTestApiClientTests : IClassFixture<TestWebApplicationFactory<Program>>
//{
//    private readonly TestWebApplicationFactory<Startup> _factory;
//    protected readonly HttpClient DpDataProviderHttpClient;

//    private Fixture Fixture { get; } = new();

//    public YDTestApiClientTests(TestWebApplicationFactory<Startup> factory)
//    {
//        _factory = factory;
//        DpDataProviderHttpClient = factory.CreateClient();
//    }

//    //[Fact]
//    //public async Task FederalProfileController_FillProfile_ShouldReturnSuccess()
//    //{
//    //    var token = MockJwtTokens.GenerateJwtToken(new List<Claim>
//    //    {
//    //        new("urn:esia:sbj_id", "2")
//    //    });

//    //    DpDataProviderHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//    //    var result = await DpDataProviderHttpClient.GetAsync($"api/v1/FederalProfile/fillProfile?state=123321&code=567345");

//    //    var request = await _factory.DbContext.ProfileRequests.FirstOrDefaultAsync();
//    //    if (request != null) _factory.DbContext.ProfileRequests.Remove(request);
//    //    await _factory.DbContext.SaveChangesAsync();
//    //    Assert.Equal(HttpStatusCode.OK, result.StatusCode);
//    //}

//    //[Fact]
//    //public async Task ExternalController_CallBack_StorageProviderMockShouldBeCalled()
//    //{
//    //    var token = MockJwtTokens.GenerateJwtToken(new List<Claim>
//    //    {
//    //        new("urn:esia:sbj_id", "2")
//    //    });

//    //    DpDataProviderHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//    //    await DpDataProviderHttpClient.GetAsync($"api/v1/FederalProfile/fillProfile?state=123321&code=567345");
//    //    var request = await _factory.DbContext.ProfileRequests.FirstOrDefaultAsync();
//    //    if (request != null) _factory.DbContext.ProfileRequests.Remove(request);
//    //    await _factory.DbContext.SaveChangesAsync();
//    //    _factory.StorageProviderMock.IsCalled.Should().BeTrue();
//    //}

//    //[Fact]
//    //public async Task ExternalController_CallBack_FederalProfileDataProviderMockShouldBeCalled()
//    //{
//    //    var token = MockJwtTokens.GenerateJwtToken(new List<Claim>
//    //    {
//    //        new("urn:esia:sbj_id", "2")
//    //    });

//    //    DpDataProviderHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//    //    await DpDataProviderHttpClient.GetAsync($"api/v1/FederalProfile/fillProfile?state=123321&code=567345");
//    //    var request = await _factory.DbContext.ProfileRequests.FirstOrDefaultAsync();
//    //    if (request != null) _factory.DbContext.ProfileRequests.Remove(request);
//    //    await _factory.DbContext.SaveChangesAsync();
//    //    _factory.FederalProfileDataProviderMock.IsCalled.Should().BeTrue();
//    //}

//    //[Fact]
//    //public async Task ExternalController_CallBack_ProfileRequestShouldBeCreated()
//    //{
//    //    var token = MockJwtTokens.GenerateJwtToken(new List<Claim>
//    //    {
//    //        new("urn:esia:sbj_id", "2")
//    //    });

//    //    DpDataProviderHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
//    //    await DpDataProviderHttpClient.GetAsync($"api/v1/FederalProfile/fillProfile?state=123321&code=567345");
//    //    var requests = await _factory.DbContext.ProfileRequests.ToListAsync();
//    //    requests.Count.Should().Be(1);
//    //}

//    //[Fact]
//    //public async Task FillProfile_FillProfileFromFederal_ApiResponseShouldBeSuccess()
//    //{
//    //    // Arrange
//    //    var httpClient = _factory.CreateClient();
//    //    var client = new DataProviderClient(httpClient);
//    //    var token = MockJwtTokens.GenerateJwtToken(new List<Claim>
//    //    {
//    //        new("urn:esia:sbj_id", "2")
//    //    });

//    //    client.SetToken(token);

//    //    // Act
//    //    var response = await client.FillProfileFromFederal("123", "123", "123");
//    //    var request = await _factory.DbContext.ProfileRequests.FirstOrDefaultAsync();
//    //    if (request != null) _factory.DbContext.ProfileRequests.Remove(request);
//    //    await _factory.DbContext.SaveChangesAsync();

//    //    // Assert
//    //    response.Success.Should().BeTrue();
//    //    response.ErrorCode.Should().Be(0);
//    //    response.Error.Should().BeNullOrEmpty();
//    //    response.Result.Should().NotBeNull();
//    //}

//    //[Fact]
//    //public async Task FillProfile_FillProfileFromEsia_ApiResponseShouldBeSuccess()
//    //{
//    //    // Arrange
//    //    var httpClient = _factory.CreateClient();
//    //    var client = new DataProviderClient(httpClient);
//    //    var token = MockJwtTokens.GenerateJwtToken(new List<Claim>
//    //    {
//    //        new("urn:esia:sbj_id", "2")
//    //    });

//    //    client.SetToken(token);

//    //    // Act
//    //    var response = await client.FillProfileFromEsia("123", "123");

//    //    // Assert
//    //    response.Success.Should().BeTrue();
//    //    response.ErrorCode.Should().Be(0);
//    //    response.Error.Should().BeNullOrEmpty();
//    //    response.Result.Should().NotBeNull();
//    //}

//    //[Fact]
//    //public async Task FillProfileWithoutToken_FillProfileFromEsia_ApiResponseShouldBeFailed()
//    //{
//    //    // Arrange
//    //    var httpClient = _factory.CreateClient();
//    //    var client = new DataProviderClient(httpClient);

//    //    // Act
//    //    var exception = await Assert.ThrowsAsync<Abdt.DigitalProfile.DataProvider.Contracts.Exceptions.ClientException>(() => client.FillProfileFromEsia("123", "123"));

//    //    // Assert
//    //    Assert.Contains("Status Code=401", exception.Message);
//    //}
//}
