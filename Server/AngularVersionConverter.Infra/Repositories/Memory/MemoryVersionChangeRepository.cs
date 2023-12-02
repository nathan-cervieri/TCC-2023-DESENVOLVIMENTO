using AngularVersionConverter.Domain.Entities;
using AngularVersionConverter.Domain.Entities.VersionChange;
using AngularVersionConverter.Domain.Entities.VersionChange.ChangeReplace;
using AngularVersionConverter.Domain.Models.VersionChange.ChangeReplace;
using AngularVersionConverter.Infra.Interfaces;

namespace AngularVersionConverter.Infra.Repositories.Memory
{
    public class MemoryVersionChangeRepository : IVersionChangeRepository
    {
        private static readonly IEnumerable<VersionChange> versionChangeList = new List<VersionChange>
        {
            new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType = ChangeTypeEnum.SingleImportOriginChange,
                ChangeFinderRegexString = @"\s*XhrFactory\s*",
                ApplyOnce= false,
                FindReplaceList = new List<FindReplace>
                {
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.Replace,
                        FindChangeString = @"{\s*XhrFactory\s*}",
                        WhatReplaceRegex = @"@angular/common/http",
                        ReplaceText = @"@angular/common",
                    },
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.ReplaceAndNewLine,
                        FindChangeString = @"{.*\s*,\s*XhrFactory\s*.*}",
                        WhatReplaceRegex = @",\s*XhrFactory",
                        ReplaceText = @"",
                        NewLine = @"import { XhrFactory } from '@angular/common;"
                    },
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.ReplaceAndNewLine,
                        FindChangeString = @"{\s*XhrFactory\s*,.*}",
                        WhatReplaceRegex = @"XhrFactory\s*,\s*",
                        ReplaceText = @"",
                        NewLine = @"import { XhrFactory } from '@angular/common;"
                    }
                },
                Description = "Simple import change"
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType = ChangeTypeEnum.MultipleImportOriginChange,
                ChangeFinderRegexString = @"^import\s*{.*(makeStateKey|StateKey|TransferState)+.*}\s*from\s*'@angular\/platform-browser';+$",
                ApplyOnce= false,
                FindReplaceList = new List<FindReplace>
                {
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.Replace,
                        FindChangeString = @"{\s*(((StateKey|makeStateKey|TransferState),?)+\s*)+\s*}",
                        WhatReplaceRegex = @"@angular/platform-browser",
                        ReplaceText = @"@angular/core",
                    },
                    new FindReplace
                    {
                        Type = FindReplaceTypeEnum.MultipleReplaceAndNewLine,
                        FindChangeString = @"{(.*,\s*)?(makeStateKey|StateKey|TransferState)+\s*(,.*)?}",
                        WhatReplaceRegex = @"makeStateKey|StateKey|TransferState",
                        ReplaceText = @"",
                        NewLine = @"import { {replaced} } from '@angular/core';"
                    }
                },
                Description = "complex import change"
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                Description = "Make sure that you are using a supported version of node.js before you upgrade your application. Angular v16 supports node.js versions: v16 and v18."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                Description = "Make sure that you are using a supported version of TypeScript before you upgrade your application. Angular v16 supports TypeScript version 4.9.3 or later."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                Description = "Make sure that you are using a supported version of Zone.js before you upgrade your application. Angular v16 supports Zone.js version 0.13.x or later."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                Description = "Due to the removal of the Angular Compatibility Compiler (ngcc) in v16, projects on v16 and later no longer support View Engine libraries."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= ": event",
                Description = "The Event union no longer contains RouterEvent, which means that if you're using the Event type you may have to change the type definition from (e: Event) to (e: Event|RouterEvent)"
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= "RendererType2",
                Description = "Pass only flat arrays to RendererType2.styles because it no longer accepts nested arrays."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= "BrowserPlatformLocation",
                InformationUrl= "https://github.com/angular/angular/blob/main/CHANGELOG.md#common-9",
                Description = "ou may have to update tests that use BrowserPlatformLocation because MockPlatformLocation is now provided by default in tests."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= "ActiveRoute",
                InformationUrl= "https://github.com/angular/angular/blob/main/CHANGELOG.md#1600-next1-2023-03-01",
                Description = "After bug fixes in Router.createUrlTree you may have to readjust tests which mock ActiveRoute."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= "renderModuleFactory",
                Description = "Revise your code to use renderModule instead of renderModuleFactory because it has been deleted."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= ".withServerTransition\\({%s*appId",
                InformationUrl= "https://github.com/angular/angular/blob/main/CHANGELOG.md#platform-browser-4",
                Description = "If you're running multiple Angular apps on the same page and you're using BrowserModule.withServerTransition({ appId: 'serverApp' }) make sure you set the APP_ID instead since withServerTransition is now deprecated."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ChangeFinderRegexString= ".runInContext",
                Description = "Change EnvironmentInjector.runInContext to runInInjectionContext and pass the environment injector as the first parameter."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= "ComponentFactoryResolver",
                Description = "Update your code to use ViewContainerRef.createComponent without the factory resolver. ComponentFactoryResolver has been removed from Router APIs."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                Description = "If you bootstrap multiple apps on the same page, make sure you set unique APP_IDs."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= "renderApplication",
                Description = "Update your code to revise renderApplication method as it no longer accepts a root component as first argument, but instead a callback that should bootstrap your app."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= "PlatformConfig",
                Description = "Update your code to remove any reference to PlatformConfig.baseUrl and PlatformConfig.useAbsoluteUrl platform-server config options as it has been deprecated."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= "setInput",
                Description = "If you rely on ComponentRef.setInput to set the component input even if it's the same based on Object.is equality check, make sure you copy its value."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ChangeFinderRegexString= "ANALYZE_FOR_ENTRY_COMPONENTS",
                Description = "Update your code to remove any reference to ANALYZE_FOR_ENTRY_COMPONENTS injection token as it has been deleted."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ChangeFinderRegexString= "entryComponents",
                Description = "entryComponents is no longer available and any reference to it can be removed from the @NgModule and @Component public APIs."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ChangeFinderRegexString= "ngTemplateOutletContext",
                InformationUrl = "https://github.com/angular/angular/blob/main/CHANGELOG.md#common-1",
                Description = "ngTemplateOutletContext has stricter type checking which requires you to declare all the properties in the corresponding object."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ApplyOnce= true,
                ChangeFinderRegexString= "FESM2015",
                Description = "Angular packages no longer include FESM2015 and the distributed ECMScript has been updated from 2020 to 2022."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ChangeFinderRegexString= "addGlobalEventListener",
                Description = "The deprecated EventManager method addGlobalEventListener has been removed as it is not used by Ivy."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ChangeFinderRegexString= "BrowserTransferStateModule",
                Description = "BrowserTransferStateModule is no longer available and any reference to it can be removed from your applications."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ChangeFinderRegexString= "ReflectiveInjector",
                Description = "Update your code to use Injector.create rather than ReflectiveInjector since ReflectiveInjector is removed."
            }, new VersionChange
            {
                Id = new Guid(),
                Version = AngularVersionEnum.Angular16,
                ChangeType= ChangeTypeEnum.NoChangeOnlyWarn,
                ChangeFinderRegexString= "QueryList",
                Description = "QueryList.filter now supports type guard functions. Since the type will be narrowed, you may have to update your application code that relies on the old behavior."
            }
        };

        public IEnumerable<VersionChange> GetAll()
        {
            return versionChangeList;
        }

        public IEnumerable<VersionChange> GetAllChangesFromTo(AngularVersionEnum versionFrom, AngularVersionEnum versionTo)
        {
            return versionChangeList.Where(vcl => vcl.Version > versionFrom && vcl.Version <= versionTo);
        }

        public IEnumerable<VersionChange> GetStaticChangesFrom(AngularVersionEnum versionFrom, AngularVersionEnum versionTo)
        {
            return versionChangeList
                .Where(vcl => vcl.Version > versionFrom
                        && vcl.Version <= versionTo
                        && vcl.ApplyOnce
                        && string.IsNullOrWhiteSpace(vcl.ChangeFinderRegexString)
                );
        }

        public IEnumerable<VersionChange> GetDynamicChangesFromTo(AngularVersionEnum versionFrom, AngularVersionEnum versionTo)
        {
            return versionChangeList
                .Where(vcl => vcl.Version > versionFrom
                        && vcl.Version <= versionTo
                        && (!vcl.ApplyOnce || !string.IsNullOrEmpty(vcl.ChangeFinderRegexString))
                );
        }
    }
}
