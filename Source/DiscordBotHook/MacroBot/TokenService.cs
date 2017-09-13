using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacroBot
{

    public interface ITokenService
    {
        Task<Token> GetToken(ulong userId, ulong channelId);
    }


    public class TokenService : ITokenService
    {
        public TokenService()
        {

        }

        public Task<Token> GetToken(ulong userId, ulong channelId)
        {
            return Task.FromResult<Token>(new Token());
        }

    }

}