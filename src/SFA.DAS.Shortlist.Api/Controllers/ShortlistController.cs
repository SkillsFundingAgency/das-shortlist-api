using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.Shortlist.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortlistController : ControllerBase
    {
        private readonly ILogger<ShortlistController> _logger;

        public ShortlistController(ILogger<ShortlistController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetAllForUser(Guid userId)
        {
            _logger.LogInformation("Request received to get all shortlist items for user {userId}", userId);
            return Ok("Not implemnented yet");
        }
    }
}