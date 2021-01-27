using ConsulUnit.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsulUnit.Interface
{
    public interface IResolveServiceInstances
    {
        Task<IList<RegistryInfo>> FindServiceInstancesAsync();

        Task<IList<RegistryInfo>> FindServiceInstancesAsync(string name);

        Task<IList<RegistryInfo>> FindServiceInstancesWithVersionAsync(string name, string version);

        Task<IList<RegistryInfo>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> nameTagsPredicate,
            Predicate<RegistryInfo> registryInformationPredicate);

        Task<IList<RegistryInfo>> FindServiceInstancesAsync(Predicate<KeyValuePair<string, string[]>> predicate);

        Task<IList<RegistryInfo>> FindServiceInstancesAsync(Predicate<RegistryInfo> predicate);

        Task<IList<RegistryInfo>> FindAllServicesAsync();
    }
}
