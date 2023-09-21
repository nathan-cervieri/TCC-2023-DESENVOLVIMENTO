namespace AngularVersionConverter.Domain.Entities.VersionChange.ChangeReplace
{
    public enum ChangeTypeEnum
    {
        None = 0,
        SingleImportOriginChange = 1,
        MultipleImportOriginChange = 2,
        NoChangeOnlyWarn = 3,
        NoChangeWarnWithLink = 4,
    }
}
