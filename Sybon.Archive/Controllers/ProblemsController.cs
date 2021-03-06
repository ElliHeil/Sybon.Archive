﻿using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using Sybon.Archive.Services.ProblemsService;
using Problem = Sybon.Archive.Services.ProblemsService.Models.Problem;

namespace Sybon.Archive.Controllers
{
    [Route("api/[controller]")]
    public class ProblemsController : Controller, ILogged
    {
        [HttpGet("{id}")]
        [SwaggerOperation("GetById")]
        [SwaggerResponse((int) HttpStatusCode.OK, Type = typeof(Problem))]
        [SwaggerOperationFilter(typeof(SwaggerApiKeySecurityFilter))]
        [AuthorizeFilter(true)]
        public async Task<IActionResult> Get([FromServices] IProblemsService problemsService, long id)
        {
            var problem = await problemsService.GetAsync(id);
            return Ok(problem);
        }

        [HttpGet("{id}/statement")]
        [SwaggerOperationFilter(typeof(SwaggerApiKeySecurityFilter))]
        [AuthorizeFilter(true)]
        public async Task<IActionResult>  Statement([FromServices] IProblemsService problemsService, long id)
        {
            var url = await problemsService.GetStatementUrlAsync(id);
            return Ok(url);
        }

        public long UserId { get; set; }
        public string ApiKey { get; set; }
    }
}