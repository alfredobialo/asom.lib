namespace asom.lib.core
{
    /// <summary>
    /// Stores List of Injected Service Instances and provides a way of Filtering out the required Service Instance by Client
    /// </summary>
    /// <example>If we have multiple implemented Instances of a given service, you can choose the implemented instance at runtime </example>
    /// <typeparam name="TService">Service Interface</typeparam>
    public interface IServiceListFilter<TService> where TService : IServiceQuery
    {
        TService GetService(string serviceId);
        string DefaultServiceName { get; set; }
    }

    public interface IServiceQuery
    {
        string Id { get; }
    }
}
