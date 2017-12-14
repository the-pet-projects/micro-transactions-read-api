namespace PetProjects.MicroTransactionsApi.Presentation.Api.V1.Controllers
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    [ApiVersion("1")]
    [Route("v{api-version:apiVersion}/samples")]
    public class SamplesController : ControllerBase
    {
        [HttpGet("{key}")]
        public IActionResult GetAsync([FromRoute] string key)
        {
            return this.Ok(new Dictionary<string, Guid>
            {
                {
                    key, Guid.NewGuid()
                }
            });
        }
    }
}