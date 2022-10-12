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
                return BadRequest(ModelState);
            }
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
    }
}