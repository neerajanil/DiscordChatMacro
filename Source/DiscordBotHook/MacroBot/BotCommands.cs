using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;


namespace MacroBot
{

// Create a module with no prefix
    public class TestModule : ModuleBase<SocketCommandContext>
    {
        // ~say hello -> hello
        [Command("say"), Summary("Echos a message.")]
        public async Task Say([Remainder, Summary("The text to echo")] string echo)
        {
            // ReplyAsync is a method on ModuleBase
            await ReplyAsync(echo);
        }
    }

    // Create a module with no prefix
    public class LinkCommand : ModuleBase
    {
        private readonly ITokenService _tokenService;
        public LinkCommand(ITokenService tokenService)
        {
            _tokenService = tokenService ?? throw new ArgumentNullException("tokenService");
        }


        // ~say hello -> hello
        [Command("link"), Summary("Links user and channel to bot and messages token to user.")]
        public async Task Link()
        {
            var token = await _tokenService.GetToken(Context.User.Id, Context.Channel.Id);
            await ReplyAsync(token.Id);
        }
    }
}