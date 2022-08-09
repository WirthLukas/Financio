using Financio.WebApi.DataTranferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Financio.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        public AccountController() { }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok();
        }

        [HttpPost]
        public void CreateAccount(CreateAccountDto accountData)
        {

        }

        [HttpDelete]
        public void DeleteAccount()
        {

        }
    }
}
