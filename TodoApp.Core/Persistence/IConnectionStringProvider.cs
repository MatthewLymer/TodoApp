namespace TodoApp.Core.Persistence
{
    public interface IConnectionStringProvider
    {
        string ConnectionString { get; }
    }
}
