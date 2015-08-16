using System;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using TodoApp.Core.Persistence.Mapping;
using Environment = NHibernate.Cfg.Environment;

namespace TodoApp.Core.Persistence
{
    public sealed class MsSqlSessionFactory : IDisposable
    {
        private readonly ISessionFactory _sessionFactory;

        public MsSqlSessionFactory(string connectionString)
        {
            _sessionFactory = CreateConfiguration(connectionString).BuildSessionFactory();
        }

        public ISession Create()
        {
            return _sessionFactory.OpenSession();
        }

        public void Dispose()
        {
            _sessionFactory.Dispose();
        }

        private static Configuration CreateConfiguration(string connectionString)
        {
            var configuration = new Configuration();

            ConfigureConnection(configuration, connectionString);

            ConfigureMappings(configuration);

            return configuration;
        }

        private static void ConfigureConnection(Configuration configuration, string connectionString)
        {
            configuration.SetProperty(Environment.Dialect, "NHibernate.Dialect.MsSql2005Dialect");
            configuration.SetProperty(Environment.ConnectionDriver, "NHibernate.Driver.SqlClientDriver");
            configuration.SetProperty(Environment.ConnectionProvider, "NHibernate.Connection.DriverConnectionProvider");
            configuration.SetProperty(Environment.ConnectionString, connectionString);
        }

        private static void ConfigureMappings(Configuration configuration)
        {
            var mapper = new ModelMapper();

            mapper.AddMappings(Assembly.GetAssembly(typeof(UserMapping)).GetTypes());

            configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
        }
    }
}
