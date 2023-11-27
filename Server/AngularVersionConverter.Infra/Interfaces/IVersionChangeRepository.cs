using AngularVersionConverter.Domain.Entities;
using AngularVersionConverter.Domain.Entities.VersionChange;

namespace AngularVersionConverter.Infra.Interfaces
{
    public interface IVersionChangeRepository
    {
        IEnumerable<VersionChange> GetAll();
        IEnumerable<VersionChange> GetStaticChangesFrom(AngularVersionEnum versionFrom, AngularVersionEnum versionTo);
        IEnumerable<VersionChange> GetDynamicChangesFromTo(AngularVersionEnum versionFrom, AngularVersionEnum versionTo);
    }
}
