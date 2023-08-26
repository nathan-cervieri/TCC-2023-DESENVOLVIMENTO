using AngularVersionConverter.Domain.Entities.VersionChange;

namespace AngularVersionConverter.Domain.Models.VersionChange.ChangeReplace
{
    public class FindReplace
    {
        public FindReplaceTypeEnum Type { get; set; } = FindReplaceTypeEnum.None;
        public string FindChangeString { get; set; } = "";
        public string WhatReplaceRegex { get; set; } = "";
        public string ReplaceText { get; set; } = "";
        public string NewLine { get; set; } = "";
    }
}
