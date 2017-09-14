using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MacroBot;

namespace MacroBot.Controllers
{
    [Route("api/[controller]")]
    public class MacroController : Controller
    {
        // GET api/values
        // [HttpGet]
        // public IEnumerable<string> Get()
        // {
        //     return new string[] { "value1", "value2" };
        // }

        // GET api/values/5
        //[HttpGet("{tokenId}")]
        [HttpGet]
        public async Task<string> Get(string tokenId, string message)
        {
            var ts = new TokenDataService(new LiteDbDatabaseFactory());
            var token  = await ts.GetToken(tokenId);
            await GlobalState.CurrentBot.SendMessageAsync(token.ChannelId, message);
            return "Worked?!!";
        }

        [HttpPost]
        public async Task Post(string tokenId,[FromBody]string message)
        {
            var ts = new TokenDataService(new LiteDbDatabaseFactory());
            var token  = await ts.GetToken(tokenId);
            await GlobalState.CurrentBot.SendMessageAsync(token.ChannelId, message);
        }

        // // PUT api/values/5
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody]string value)
        // {
        // }

        // // DELETE api/values/5
        // [HttpDelete("{id}")]
        // public void Delete(int id)
        // {
        // }
    }
}
