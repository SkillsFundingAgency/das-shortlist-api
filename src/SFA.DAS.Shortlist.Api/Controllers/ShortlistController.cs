using Microsoft.AspNetCore.Mvc;
using SFA.DAS.Shortlist.Api.Models;
using SFA.DAS.Shortlist.Application.Services;

namespace SFA.DAS.Shortlist.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShortlistItem(ShortlistAddModel shortlist)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogInformation("Invalid request to create shortlist item received");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating shortlist item for userId: {userId}", shortlist.ShortlistUserId);
            await _shortlistService.AddItem(shortlist);
            return CreatedAtAction(nameof(GetAllShortlistForUser), new { userId = shortlist.ShortlistUserId }, null);
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<List<Application.Domain.Entities.Shortlist>>> GetAllShortlistForUser(Guid userId)
        {
            _logger.LogInformation("Request received to get all shortlist items for user {userid}", userId);
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
        
        [HttpGet]
        [Route("users/{userId}/count")]
        public async Task<ActionResult<int>> GetShortlistCountForUser(Guid userId)
        {
            _logger.LogInformation("Request received to get count of shortlist items for user {userid}", userId);
            var count = await _shortlistService.GetShortlistCountForUser(userId);
            return Ok(count);
        }

        [HttpGet]
        [Route("users/expired")]
        public async Task<IActionResult> GetExpiredShortlistUserIds([FromQuery] int expiryInDays)
        {
            _logger.LogInformation("Request received to get expired user ids");
            var userIds = await _shortlistService.GetExpiredShortlistUserIds(expiryInDays);
            return Ok(new { userIds = userIds });
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