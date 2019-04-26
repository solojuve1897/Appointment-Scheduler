using Lime.Business;
using Lime.Common;
using Microsoft.AspNetCore.Mvc;

namespace Lime.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuggestionsController : ControllerBase
    {
        private readonly ISuggestionLogic _suggestionLogic;

        public SuggestionsController(ISuggestionLogic suggestionLogic)
        {
            _suggestionLogic = suggestionLogic;
        }

        // GET api/suggestions?employees=X&employees=X&fromDate=X&toDate=X&officeHoursStart=X&officeHoursEnd=X&meetingLength=X
        [HttpGet]
        public IActionResult Get([FromQuery]SuggestionsQueryParams parameters)
        {
            if (ModelState.IsValid)
            {
                return Ok(new { query = parameters, suggestions = _suggestionLogic.GetSuggestions(parameters) });
            }
            return BadRequest();
        }
    }
}
