using Microsoft.AspNetCore.Mvc;

namespace TextParser.API.Controllers
{
    using Utils.Parsers.TextParser;
    using Utils.Serializers;

    [Route("api/[controller]")]
    public class TextController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [Route("[action]")]
        public IActionResult ToXml([FromBody] string text)
        {
            return this.Ok(new TextObjectToXml().Serialize(TextParser.Parse(text)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [Route("[action]")]
        public IActionResult ToCsv([FromBody] string text)
        {
            return this.Ok(new TextObjectToCsv().Serialize(TextParser.Parse(text)));
        }
    }
}
