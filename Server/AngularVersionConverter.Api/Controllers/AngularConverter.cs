using AngularVersionConverter.Application.Extensions;
using AngularVersionConverter.Application.Interfaces;
using AngularVersionConverter.Domain.Entities;
using AngularVersionConverter.Domain.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AngularVersionConverter.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AngularConverter : ControllerBase
    {

        private readonly ILogger<AngularConverter> _logger;
        private readonly IConverterService converterService;

        public AngularConverter(ILogger<AngularConverter> logger, IConverterService converterService)
        {
            _logger = logger;
            this.converterService = converterService;
        }

        [HttpGet()]
        [Route("Versions")]
        public ActionResult GetEnumValues()
        {
            var values = converterService.GetAngularVersionEnums();
            return Ok(values);
        }

        [HttpGet()]
        [Route("StaticChanges")]
        public ActionResult GetStaticChanges([FromQuery] AngularVersionEnum versionFrom, [FromQuery] AngularVersionEnum versionTo)
        {
            var staticChanges = converterService.GetAllOneTimeReports(versionFrom, versionTo);
            return Ok(staticChanges);
        }

        [HttpPost(Name = "ConvertFile")]
        public ActionResult ConvertFile(ConvertRequest request)
        {
            _logger.LogInformation("Inicio conversão {0}", DateTime.Now.ToString("MM/dd/yy H:mm:ss.ffff zzz"));
            var codeReport = converterService.ConvertAngularFile(request.CodeToConvert.ToStream(), request.VersionFrom, request.VersionTo);
            _logger.LogInformation("Final conversão {00}", DateTime.Now.ToString("MM/dd/yy H:mm:ss.ffff zzz"));
            return Ok(codeReport);
        }
    }
}