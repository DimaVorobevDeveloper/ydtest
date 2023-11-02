//using System.Web.Http;
//using Abdt.DigitalProfile.DataProvider.Abstractions;
//using Abdt.DigitalProfile.DataProvider.IntegrationTests.Api.Mocks;
//using Abdt.DigitalProfile.DataProvider.Storage.PostgreSql;
//using Abdt.DigitalProfile.DataProvider.UnitTests.Mocks;
//using Abdt.Infrastructure.Queue;
//using AbdtDigitalProfile.DataProvider.EsiaDataServices.Abstractions;
//using Hangfire;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using YDTest.Data;

namespace YDTest.IntegrationsTests.Api;

public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    //public StorageProviderMock StorageProviderMock { get; set; }
    //public EsiaPersonDataProviderMock EsiaPersonDataProviderMock { get; set; }
    //public EsiaOrgDataProviderMock EsiaOrgDataProviderMock { get; set; }
    //public FederalProfileDataProviderMock FederalProfileDataProviderMock { get; set; }

    //public MdmProviderMock MdmProviderMock { get; set; }

    //public CrmFindClientHttpRepositoryMock CrmFindClientHttpRepositoryMock { get; set; }

    //public CrmUpsertClientHttpRepositoryMock CrmUpsertClientHttpRepositoryMock { get; set; }
    public YDTestContext DbContext { get; set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseTestServer().ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<YDTestContext>));
            var descriptor2 = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(YDTestContext));

            services.Remove(descriptor);
            services.Remove(descriptor2);

            //var federalProfileDataProvider = services.SingleOrDefault(
            //    d => d.ServiceType ==
            //         typeof(IFederalProfileDataProvider));
            //services.Remove(federalProfileDataProvider);
            //var storageProvider = services.SingleOrDefault(
            //    d => d.ServiceType ==
            //         typeof(IStorageProvider));
            //services.Remove(storageProvider);

            //var mdmProvider = services.SingleOrDefault(
            //    d => d.ServiceType ==
            //         typeof(IMdmProvider));
            //services.Remove(mdmProvider);

            //var crmFindProvider = services.SingleOrDefault(
            //    d => d.ServiceType ==
            //         typeof(ICrmFindClientHttpRepository));
            //services.Remove(crmFindProvider);

            //var crmUpsertProvider = services.SingleOrDefault(
            //    d => d.ServiceType ==
            //         typeof(ICrmUpsertClientHttpRepository));
            //services.Remove(crmUpsertProvider);

            //StorageProviderMock = new StorageProviderMock();
            //FederalProfileDataProviderMock = new FederalProfileDataProviderMock();
            //EsiaPersonDataProviderMock = new EsiaPersonDataProviderMock();
            //EsiaOrgDataProviderMock = new EsiaOrgDataProviderMock();
            //MdmProviderMock = new MdmProviderMock();
            //CrmFindClientHttpRepositoryMock = new CrmFindClientHttpRepositoryMock();
            //CrmUpsertClientHttpRepositoryMock = new CrmUpsertClientHttpRepositoryMock();
            //services.AddScoped<IStorageProvider, StorageProviderMock>(x => StorageProviderMock);
            //services.AddScoped<IEsiaPersonDataProvider, EsiaPersonDataProviderMock>(x => EsiaPersonDataProviderMock);
            //services.AddScoped<IEsiaOrgDataProvider, EsiaOrgDataProviderMock>(x => EsiaOrgDataProviderMock);
            //services.AddScoped<ICrmFindClientHttpRepository, CrmFindClientHttpRepositoryMock>();
            //services.AddScoped<ICrmUpsertClientHttpRepository, CrmUpsertClientHttpRepositoryMock>();
            //services.AddScoped<IMdmProvider, MdmProviderMock>();
            //services.AddScoped<IEventQueue, EventQueueMock>();
            //services.AddScoped<IFederalProfileDataProvider, FederalProfileDataProviderMock>(x => FederalProfileDataProviderMock);

            //var inMemory = GlobalConfiguration.Configuration.UseInMemoryStorage();
            //services.AddHangfire(x => x.UseStorage(inMemory.Entry));

            services.AddDbContextPool<YDTestContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            var sp = services.BuildServiceProvider();
            var scope = sp.CreateScope();
            var scopedServices = scope.ServiceProvider;
            var db = scopedServices.GetRequiredService<YDTestContext>();
            db.Database.EnsureCreated();
            DbContext = db;

            services.AddHttpClient();
        });
    }
}