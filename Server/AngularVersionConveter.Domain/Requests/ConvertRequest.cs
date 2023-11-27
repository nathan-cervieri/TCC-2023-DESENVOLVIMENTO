using AngularVersionConverter.Domain.Entities;

namespace AngularVersionConverter.Domain.Requests
{
    public class ConvertRequest
    {
        public string CodeToConvert { get; set; } = "";
        public AngularVersionEnum VersionFrom { get; set; } = AngularVersionEnum.Angular15;
        public AngularVersionEnum VersionTo { get; set; } = AngularVersionEnum.Angular16;
    }
}
