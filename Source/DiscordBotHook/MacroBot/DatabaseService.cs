using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using System.Linq.Expressions;

namespace MacroBot
{
    
    public interface IDatabaseServiceFactory
    {
        Task<IDatabaseService> GetDatabaseService();
    }

    public interface IDatabaseService : IDisposable
    {
        Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> predicate);
        Task<T> Find<T>(object id);
        Task Delete<T>(Expression<Func<T, bool>> predicate);
        Task Delete<T>(object id);
        Task Insert<T>(T token);
    }

    public class LiteDbDatabaseFactory : IDatabaseServiceFactory
    {
        public Task<IDatabaseService> GetDatabaseService()
        {
            return Task.FromResult<IDatabaseService>(new LiteDbDatabase(@"tokens.db"));
        }
    }


    public class LiteDbDatabase : IDatabaseService
    {
        //private readonly string _databaseFileName = null;
        private readonly LiteDatabase _liteDb = null;
        public LiteDbDatabase(string databaseFileName)
        {
            _liteDb = new LiteDatabase(databaseFileName);
        }

        private LiteCollection<T> GetCollection<T>()
        {
            return _liteDb.GetCollection<T>(typeof(T).Name);
        }


        public Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult<IEnumerable<T>>(GetCollection<T>().Find(predicate));
        }
        public Task<T> Find<T>(object id)
        {
            return Task.FromResult<T>(GetCollection<T>().FindById(new BsonValue(id)));
        }
        public Task Delete<T>(Expression<Func<T, bool>> predicate)
        {
            GetCollection<T>().Delete(predicate);
            return Task.CompletedTask;
        }
        public Task Delete<T>(object id)
        {
            GetCollection<T>().Delete(new BsonValue(id));
            return Task.CompletedTask;
        }
        public Task Insert<T>(T token)
        {
            GetCollection<T>().Insert(token);
            return Task.CompletedTask;
        }


        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _liteDb.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~belh() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
    

}