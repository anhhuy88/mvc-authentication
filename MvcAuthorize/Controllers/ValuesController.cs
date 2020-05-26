using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MvcAuthorize.PermissionAccess;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MvcAuthorize.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [HasPermission(Permissions.CanEdit, Permissions.CanView)]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }
        [HttpGet("token")]
        public ActionResult<string> Token()
        {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, "UserName"),
                    new Claim(ClaimTypes.NameIdentifier, $"{Guid.NewGuid()}"),
                    new Claim(PermissionConstants.PackedPermissionClaimType, $"{(int)Permissions.CanEdit}"),
                    new Claim(PermissionConstants.PackedPermissionClaimType, $"{(int)Permissions.CanView}")
                };
            var signingkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey));
            var token = new JwtSecurityToken(
                   issuer: JwtOptions.Issuer,
                   audience: JwtOptions.Audience,
                   expires: DateTime.UtcNow.AddDays(1),
                   claims: claims,
                   signingCredentials: new SigningCredentials(signingkey, SecurityAlgorithms.HmacSha256)
                   );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
