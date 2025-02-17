using Microsoft.AspNetCore.Mvc;
using Plaid.MSACommerce.AuthServer.Services;

namespace Plaid.MSACommerce.AuthServer.Controllers
{

    [Route("api/token")]
    [ApiController]
    public class TokenController(IIdentityService identityService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string username, string password)
        {
            var result = await identityService.GetAccessTokenAsync(username, password);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(new { errors = result.Errors });
        }
    }
}
