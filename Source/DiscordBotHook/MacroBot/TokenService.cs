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


    public class GuidTokenService : ITokenService
    {
        ITokenDataService _tokenDataService;
        public GuidTokenService(ITokenDataService tokenDataService)
        {
            _tokenDataService = tokenDataService ?? throw new ArgumentNullException("tokenDataService");
        }

        public Task<Token> GetToken(ulong userId, ulong channelId)
        {
            _tokenDataService.DeleteToken(userId);
            Token token = new Token() {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                ChannelId = channelId,
                CreatedDateTime = DateTime.UtcNow
            };
            _tokenDataService.InsertToken(token);
            return Task.FromResult<Token>(token);
        }
    }

}