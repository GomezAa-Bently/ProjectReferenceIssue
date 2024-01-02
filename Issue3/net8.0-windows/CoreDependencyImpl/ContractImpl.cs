using CoreDependencyLibContract;
using SatelliteDependency;

namespace CoreDependencyImpl
{
    public class ContractImpl : IContract
    {
        private Satellite _satellite = new Satellite();
    }
}
