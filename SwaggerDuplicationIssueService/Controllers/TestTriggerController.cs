using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SwaggerDuplicationIssueService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTriggerController : ControllerBase
    {

        [HttpGet("execute")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 404)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<ActionResult<string>> ExecuteTriggerManually()
        {
            //[ERROR] if I delete the ErrorResponseModels it won't generate the errors
            return Ok();
        }
    }
}