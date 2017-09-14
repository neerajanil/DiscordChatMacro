using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;

namespace MacroBot
{
    public class Token
    {
        [BsonId]
        public string Id { get; set; }
        public ulong UserId { get; set; }
        public ulong ChannelId { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }


    public interface ITokenDataService
    {
        Task<Token> GetToken(string tokenId);
        Task DeleteToken(string tokenId);
        Task DeleteToken(ulong userId);
        Task InsertToken(Token token);
    }


    public class TokenDataService : ITokenDataService
    {
        private readonly IDatabaseServiceFactory _databaseServiceFactory;
        public TokenDataService(IDatabaseServiceFactory databaseServiceFactory)
        {
            _databaseServiceFactory = databaseServiceFactory ?? throw new ArgumentNullException("databaseService");
        }
        
        public async Task<Token> GetToken(string tokenId)
        {
            using(var db = await _databaseServiceFactory.GetDatabaseService())
            {
                var tokens = await db.Find<Token>(x => x.Id == tokenId);
                return tokens.FirstOrDefault();
            }
        }
        
        public async Task DeleteToken(string tokenId)
        {
            using(var db = await _databaseServiceFactory.GetDatabaseService())
            {
                await db.Delete<Token>(tokenId);
            }
        }
        
        public async Task DeleteToken(ulong userId)
        {
            using(var db = await _databaseServiceFactory.GetDatabaseService())
            {
                await db.Delete<Token>(x => x.UserId == userId);
            }
        }

        public async Task InsertToken(Token token)
        {
            using(var db = await _databaseServiceFactory.GetDatabaseService())
            {
                await db.Insert<Token>(token);
            }
        }

    }

}