using AngularVersionConverter.Domain.Entities.VersionChange.ChangeReplace;
using AngularVersionConverter.Domain.Models.VersionChange.ChangeReplace;

namespace AngularVersionConverter.Domain.Entities.VersionChange
{
    public class VersionChange
    {
        public Guid Id { get; set; }

        /// <summary
        /// Defines the target version to which the change is relevant eg. from version AngularVersionEnum.14 to AngularVersionEnum.15 Version will be AngularVersionEnum.15
        /// </summary>
        public AngularVersionEnum Version { get; set; }
        public ChangeTypeEnum ChangeType { get; set; }
        public string ChangeFinderRegexString { get; set; } = "";
        public string Description { get; set; } = "";
        public string InformationUrl { get; set; } = "";
        public bool ApplyOnce { get; set; } = false;
        public IEnumerable<FindReplace> FindReplaceList { get; set; } = new List<FindReplace>();
    }
}
