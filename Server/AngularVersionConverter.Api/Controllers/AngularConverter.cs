using AngularVersionConverter.Application.Interfaces;
using AngularVersionConverter.Models;
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

        [HttpGet(Name = "GetConversionVersionsEnum")]
        public IEnumerable<AngularVersionEnum> GetEnumValues()
        {
            return Enum.GetValues(typeof(AngularVersionEnum)).Cast<AngularVersionEnum>();
        }
    }
}