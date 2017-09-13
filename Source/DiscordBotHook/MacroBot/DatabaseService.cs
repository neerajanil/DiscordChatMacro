using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacroBot
{
    public class Token
    {
        public string Id { get; set; }
        public ulong UserId { get; set; }
        public ulong ChannelId { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }


    public interface IDatabaseService
    {
        Task<Token> GetToken(string Token);
        Task DeleteToken(string Token);
        Task DeleteToken(ulong UserId);
        Task InsertToken(Token token);
    }


    public class LiteDbDatabaseService : IDatabaseService
    {
        public LiteDbDatabaseService(){}
        

        public Task<Token> GetToken(string Token)
        {
            return Task.FromResult<Token>(new Token());
        }
        
        public Task DeleteToken(string Token)
        {
            return Task.CompletedTask;
        }
        
        public Task DeleteToken(ulong UserId)
        {
            return Task.CompletedTask;
        }

        public Task InsertToken(Token token)
        {
            return Task.CompletedTask;
        }

    }

}