namespace TextParser.API.Controllers
{
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Utils.Parsers.TextParser;
    using Utils.Serializers;

    [EnableCors(CorsPolicies.AllowAllOrigins)]
    [Route("api/[controller]")]
    public class TextController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [Route("[action]")]
        public IActionResult ToXml([FromBody] Request request)
        {
            return this.Ok(new TextObjectToXml().Serialize(TextParser.Parse(request.Text)));
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [Route("[action]")]
        public IActionResult ToCsv([FromBody] Request request)
        {
            return this.Ok(new TextObjectToCsv().Serialize(TextParser.Parse(request.Text)));
        }
    }
}