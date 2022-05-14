using System.Collections.Generic;
using System.Linq;

namespace asom.lib.core
{
    /// <summary>
    /// Stores List of Injected Service Instances and provides a way of Filtering out the required Service Instance by Client
    /// </summary>
    /// <example>If we have multiple implemented Instances of a given service, you can choose the implemented instance at runtime </example>
    /// <typeparam name="TService">Service Interface</typeparam>
    public class ServiceListFilter<TService> : IServiceListFilter<TService> where TService : IServiceQuery
    {
        private readonly IEnumerable<TService> _injectedServices;

        public ServiceListFilter(IEnumerable<TService> injectedServices)
        {
            _injectedServices = injectedServices;
        }
        public TService GetService(string serviceId)
        {
            if (!string.IsNullOrEmpty(serviceId))
            {
                var serviceInstance = _injectedServices.FirstOrDefault(x => x.Id == serviceId);

                return serviceInstance;
            }
            return _injectedServices.FirstOrDefault(x =>x.Id == DefaultServiceName);

        }

        public string DefaultServiceName { get; set; }
    }
}
