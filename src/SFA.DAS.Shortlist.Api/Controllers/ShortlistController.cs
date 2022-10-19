using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Shortlist.Api.Models;
using SFA.DAS.Shortlist.Application.Services;
using System.Net;

namespace SFA.DAS.Shortlist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortlistController : ControllerBase
    {
        private readonly ILogger<ShortlistController> _logger;
        private readonly IShortlistService _shortlistService;

        public ShortlistController(ILogger<ShortlistController> logger, IShortlistService shortlistService)
        {
            _logger = logger;
            _shortlistService = shortlistService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateShortlistItem(ShortlistAddModel shortlist)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid request to create shortlist item received");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating shortlist item for userId: {userId}", shortlist.ShortlistUserId);
            await _shortlistService.AddItem(shortlist);
            return CreatedAtAction("GetAllForUser", new { userId = shortlist.ShortlistUserId }, null);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetAllForUser(Guid userId)
        {
            _logger.LogInformation("Request received to get all shortlist items for user {userId}", userId);
            var shortlists = await _shortlistService.GetAllUserShortlist(userId);
            return Ok(shortlists);
        }

        [HttpDelete]
        [Route("users/{userId}")]
        public async Task<IActionResult> DeleteShortlistForUser(Guid userId)
        {
            _logger.LogInformation("Request received to delete shortlist items for user {userId}", userId);

            await _shortlistService.DeleteAllShortlistForUser(userId);

            return NoContent();
        }
    }
}