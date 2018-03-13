using MongoDB.Driver;
using ProjectOnion.Model.Interfaces;
using ProjectOnion.MongoDB.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectOnion.MongoDB.Provider
{
    public class MongoDbProvider<TEntity> : IEntityProvider<TEntity> where TEntity : IEntity
    {
        private IMongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<TEntity> _collection;

        public MongoDbProvider(string databaseUrl, string databaseName)
        {
            this._client = new MongoClient(databaseUrl);
            this._database = _client.GetDatabase(databaseName);
            this._collection = _database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task<TEntity> Get(Guid objectId)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            var list = await this._collection.FindAsync(filter);
            return list.SingleOrDefault();
        }

        public async Task<IEnumerable<TEntity>> Get(FilterDefinition<TEntity> filters)
        {
            var list = await this._collection.FindAsync(filters);
            return list.ToList();
        }

        public async Task<IEnumerable<TEntity>> Get(FilterDefinition<TEntity> filters, FindOptions<TEntity> options)
        {
            var list = await this._collection.FindAsync(filters, options);
            return list.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            var filter = Builders<TEntity>.Filter.Empty;
            var list = await this._collection.FindAsync(filter);
            return list.ToList();
        }

        /// <summary>
        /// Guid needs to be generated before calling insert.
        /// </summary>
        /// <param name="entityObject"></param>
        /// <returns></returns>
        public async Task<TEntity> Insert(TEntity entityObject)
        {
            await this._collection.InsertOneAsync(entityObject);
            return entityObject;
        }

        public async Task<DeleteResult> Remove(Guid objectId)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", objectId);
            var list = await this._collection.DeleteOneAsync(filter);
            return list;
        }

        public async Task<long> Count()
        {
            var filter = Builders<TEntity>.Filter.Empty;
            var count = await this._collection.CountAsync(filter);
            return count;
        }

        public void RemoveCollection()
        {
            this._database.DropCollection(typeof(TEntity).FullName);
        }

        public async Task<TEntity> Update(TEntity entityObject)
        {
            var filter = Builders<TEntity>.Filter.And(
                Builders<TEntity>.Filter.Eq("_id", entityObject.Id));

            // Concurrency check
            var previousInstance = await this._collection.FindOneAndReplaceAsync(filter, entityObject);
            if (previousInstance == null)
            {
                throw new Exception();
            }
            return entityObject;
        }

        public async Task<TEntity> Upsert(TEntity entityObject)
        {
            await this._collection.ReplaceOneAsync(doc => doc.Id.Equals(entityObject.Id), entityObject,
                new UpdateOptions {IsUpsert = true});
            return entityObject;
        }
    }
}
