using CampusPlacementManagement.Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampusPlacementManagement.Api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    [Authorize(Roles = "SuperAdmin,PlacementOfficer")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPut("{userId}/activate")]
        public async Task<IActionResult> ActivateUser(string userId)
        {
            var result = await _accountService.ActivateUserAsync(userId);

            if (!result)
            {
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            return Ok(new
            {
                message = "Account activated successfully."
            });
        }

        [HttpPut("{userId}/reject")]
        public async Task<IActionResult> RejectUser(string userId)
        {
            var result = await _accountService.RejectUserAsync(userId);

            if (!result)
            {
                return NotFound(new
                {
                    message = "User not found."
                });
            }

            return Ok(new
            {
                message = "Account rejected successfully."
            });
        }
    }
}