﻿using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Models;

namespace AngularVersionConverter.Application.Interfaces
{
    public interface IConverterService
    {
        public string ConvertAngular(Stream streamToConvert, AngularVersionEnum versionFrom, AngularVersionEnum versionTo);
        public string ConvertAngularLine(string stringToConvert, IEnumerable<VersionChange> versionChangeList);
    }
}
