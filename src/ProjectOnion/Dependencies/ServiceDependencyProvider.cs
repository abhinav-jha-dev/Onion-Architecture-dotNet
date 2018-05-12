using Microsoft.Extensions.DependencyInjection;
using ProjectOnion.Model.Models;
using ProjectOnion.MongoDB.Interface;
using ProjectOnion.MongoDB.Provider;
using ProjectOnion.Repository.Interface;
using ProjectOnion.Repository.Repository;
using ProjectOnion.Service.Interface;
using ProjectOnion.Service.Service;

namespace ProjectOnion.Http.Dependencies
{
    public static class ServiceDependencyProvider
    {
        public static void RegisterDependencies(this IServiceCollection serviceCollection, string connectionString, string databaseName)
        {
            // Middleware Dependency Injection
            serviceCollection.AddSingleton<IEntityProvider<Test>>(x =>
                new MongoDbProvider<Test>(connectionString, databaseName));

            // Repository Dependency Injection
            serviceCollection.AddSingleton<ITestRepository, TestRepository>();

            // Service Dependency Injection
            serviceCollection.AddSingleton<ITestService, TestService>();
        }
    }
}
