using ConsulUnit.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsulUnit.Interface
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public interface IRegistryService : IManageHealthChecks, IResolveServiceInstances
    {
        /// <summary>
        /// 注册服务实例
        /// </summary>
        /// <param name="serviceName">服务名称</param>
        /// <param name="version">版本号</param>
        /// <param name="uri">服务地址</param>
        /// <param name="healthCheckUri">健康检查url</param>
        /// <param name="tags">标签</param>
        /// <param name="checkSeconds">健康检查间隔</param>
        /// <returns></returns>
        Task<RegistryInfo> RegisterServiceAsync(string serviceName, string version, Uri uri, Uri healthCheckUri = null, IEnumerable<string> tags = null, int checkSeconds = 30);

        /// <summary>
        /// 注销服务实例
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        Task<bool> DeregisterServiceAsync(string serviceId);
    }
}
