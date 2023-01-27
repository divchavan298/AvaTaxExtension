using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NB.AvaTaxExtension.Core;

namespace NB.AvaTaxExtension.Web.Controllers.Api
{
    [Route("api/ava-tax-extension")]
    public class AvaTaxExtensionController : Controller
    {
        // GET: api/ava-tax-extension
        /// <summary>
        /// Get message
        /// </summary>
        /// <remarks>Return "Hello world!" message</remarks>
        [HttpGet]
        [Route("")]
        [Authorize(ModuleConstants.Security.Permissions.Read)]
        public ActionResult<string> Get()
        {
            return Ok(new { result = "Hello world!" });
        }
    }
}
