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


    public interface IDatabaseService
    {
        Task<Token> GetToken(string tokenId);
        Task DeleteToken(string tokenId);
        Task DeleteToken(ulong userId);
        Task InsertToken(Token token);
    }


    public class LiteDbDatabaseService : IDatabaseService
    {
        private const string _tokenCollectionName = "tokens";
        private const string _tokenDatabaseName = @"tokens.db";

        public LiteDbDatabaseService()
        {
            using(var db = new LiteDatabase(_tokenDatabaseName))
            {
                // Get customer collection
                var tokens = db.GetCollection<Token>(_tokenCollectionName);
                tokens.EnsureIndex(t => t.Id);
                tokens.EnsureIndex(t => t.UserId);
            }
        }
        
        public Task<Token> GetToken(string tokenId)
        {
            Token token = null;
            using(var db = new LiteDatabase(_tokenDatabaseName))
            {
                // Get customer collection
                var tokens = db.GetCollection<Token>(_tokenCollectionName);
                token = tokens.Find(x => x.Id == tokenId).FirstOrDefault();
            }
            return Task.FromResult<Token>(token);
        }
        
        public Task DeleteToken(string tokenId)
        {
            using(var db = new LiteDatabase(_tokenDatabaseName))
            {
                // Get customer collection
                var tokens = db.GetCollection<Token>(_tokenCollectionName);
                tokens.Delete(tokenId);
            }
            return Task.CompletedTask;
        }
        
        public Task DeleteToken(ulong userId)
        {
            using(var db = new LiteDatabase(_tokenDatabaseName))
            {
                // Get customer collection
                var tokens = db.GetCollection<Token>(_tokenCollectionName);
                tokens.Delete(x => x.UserId == userId);
            }
            return Task.CompletedTask;
        }

        public Task InsertToken(Token token)
        {
            using(var db = new LiteDatabase(_tokenDatabaseName))
            {
                // Get customer collection
                var tokens = db.GetCollection<Token>(_tokenCollectionName);
                tokens.Insert(token);
            }
            return Task.CompletedTask;
        }

    }

}