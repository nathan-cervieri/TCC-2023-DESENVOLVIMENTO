using AngularVersionConverter.Models;

namespace AngularVersionConverter.Domain.Requests
{
    public class ConvertRequest
    {
        public string CodeToConvert { get; set; } = "";
        public AngularVersionEnum VersionFrom { get; set; } = AngularVersionEnum.Angular14;
        public AngularVersionEnum VersionTo { get; set; } = AngularVersionEnum.Angular15;
    }
}
