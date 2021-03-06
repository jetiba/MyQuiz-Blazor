using quiz.shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace quiz.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AccountsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterModel model)
        {
            if(!model.Username.Contains("admin")){
                var newUser = new IdentityUser { UserName = model.Username };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description);

                    return Ok(new RegisterResult { Successful = false, Errors = errors });
                }

                return Ok(new RegisterResult { Successful = true });
            }
            else
            {
                var errors = new List<string>(){ {"Non puoi utilizzare la parola 'admin' nel tuo username"}};
                return Ok(new RegisterResult { Successful = false, Errors = errors });
            }
            
        }

    }
}
