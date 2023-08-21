using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Models;

namespace AngularVersionConverterApplication.Interfaces.Repository
{
    public interface IVersionChangeRepository
    {
        public IEnumerable<VersionChange> GetAll();
        public IEnumerable<VersionChange> GetVersionsFromTo(AngularVersionEnum versionFrom, AngularVersionEnum versionTo);
    }
}
