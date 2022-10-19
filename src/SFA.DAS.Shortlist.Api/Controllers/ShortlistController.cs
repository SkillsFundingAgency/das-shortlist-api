using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Shortlist.Api.Models;
using SFA.DAS.Shortlist.Application.Services;

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
        [Route("users/{userId}/items/{id}")]
        public async Task<IActionResult> DeleteShortlistItemForUser(Guid userId, Guid id)
        {
            _logger.LogInformation("Request received to delete all shortlist items for user {userId} and {id}", userId, id);
            await _shortlistService.DeleteShortlistUserItem(id, userId);
            return NoContent();
        }
    }
}