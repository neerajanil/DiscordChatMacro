using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MacroBot;

namespace MacroBot.Controllers
{

    public class MacroRequest
    {
        public string tokenid {get;set;}
        public string message {get;set;}
    }

    [Route("api/[controller]")]
    public class MacroController : Controller
    {
        [HttpPost]
        public async Task Post([FromBody]MacroRequest request)
        {
            var ts = new TokenDataService(new LiteDbDatabaseFactory());
            var token  = await ts.GetToken(request.tokenid);
            await GlobalState.CurrentBot.SendMessageAsync(token.ChannelId, request.message);
        }
    }
}
