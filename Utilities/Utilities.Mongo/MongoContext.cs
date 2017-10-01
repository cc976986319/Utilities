using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities.Mongo.Extensions.Microsoft;
using Utilities.Mongo.Extensions.Mongo;
using Utilities.Mongo.Models;

namespace Utilities.Mongo
{
    /// <summary>
    /// 数据库实例
    /// </summary>   
    public partial class MongoContext // 基本信息
    {
        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="connectionName">数据库连接名</param>
        public MongoContext(string connectionName)
        {
            this.ConnectionString = connectionName.ReadingConnectionStrings();

            this.MongoUrl = new MongoUrl(this.ConnectionString);

            this.MongoClient = new MongoClient(this.ConnectionString);
        }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// 
        /// </summary>
        public MongoClient MongoClient { get; }

        /// <summary>
        /// Mongo访问路径
        /// </summary>
        public MongoUrl MongoUrl { get; }

        /// <summary>
        /// 数据库
        /// </summary>
        public IMongoDatabase MongoDatabase { get { return this.MongoClient.GetDatabase(this.MongoUrl.DatabaseName); } }
    }

    // 新增
    public partial class MongoContext
    {
        /// <summary>
        /// 添加一条数据
        /// </summary>
        /// <param name="entity">要添加的数据</param>
        public void Add<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            this.GetCollection<TEntity>().InsertOne(entity);
        }

        /// <summary>
        /// 添加多条数据
        /// </summary>
        /// <param name="entitys">要添加的数据集</param>
        public void AddRange<TEntity>(IEnumerable<TEntity> entitys) where TEntity : EntityModel
        {
            this.GetCollection<TEntity>().InsertMany(entitys);
        }

        /// <summary>
        /// 添加一条数据(异步)
        /// </summary>
        /// <param name="entity">要添加的数据</param>
        /// <returns></returns>
        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            await this.GetCollection<TEntity>().InsertOneAsync(entity).ConfigureAwait(false);
        }

        /// <summary>
        /// 添加多条数据(异步)
        /// </summary>
        /// <param name="entitys">要添加的数据集</param>
        /// <returns></returns>
        public async Task AddRangeAsync<TEntity>(IEnumerable<TEntity> entitys) where TEntity : EntityModel
        {
            await this.GetCollection<TEntity>().InsertManyAsync(entitys).ConfigureAwait(false);
        }
    }

    // 修改
    public partial class MongoContext
    {
        /// <summary>
        /// 修改数据(单条)(根据编号:_id)
        /// </summary>
        /// <typeparam name="TEntity">文档类型</typeparam>
        /// <param name="entity">新的文档值</param>
        /// <returns></returns>
        public long Update<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            return this.Update<TEntity>(e => e._id == entity._id, entity);
        }

        /// <summary>
        /// 修改数据(单条)
        /// </summary>
        /// <typeparam name="TEntity">文档类型</typeparam>
        /// <param name="filter">修改条件</param>
        /// <param name="entity">新的文档值</param>
        /// <returns></returns>
        public long Update<TEntity>(Expression<Func<TEntity, bool>> filter, TEntity entity) where TEntity : EntityModel
        {
            UpdateDefinition<TEntity> updateDefinition = entity.MarkUpdateDefinition<TEntity>();
            UpdateDefinition<TEntity> updateDefinitionBuilder = new UpdateDefinitionBuilder<TEntity>().Combine(updateDefinition);
            return this.GetCollection<TEntity>().UpdateOne(filter, updateDefinitionBuilder).ModifiedCount;
        }

        /// <summary>
        /// 修改数据(单条)
        /// </summary>
        /// <typeparam name="TEntity">文档类型</typeparam>
        /// <param name="filter">修改条件</param>
        /// <param name="entity">新的文档值</param>
        /// <returns></returns>
        public async Task<long> UpdateAsync<TEntity>(Expression<Func<TEntity, bool>> filter, TEntity entity) where TEntity : EntityModel
        {
            UpdateDefinition<TEntity> updateDefinition = entity.MarkUpdateDefinition<TEntity>();
            UpdateDefinition<TEntity> updateDefinitionBuilder = new UpdateDefinitionBuilder<TEntity>().Combine(updateDefinition);
            var result = await this.GetCollection<TEntity>().UpdateOneAsync(filter, updateDefinitionBuilder);
            return result.ModifiedCount;
        }

        /// <summary>
        /// 修改数据(多条)
        /// </summary>
        /// <typeparam name="TEntity">文档类型</typeparam>
        /// <param name="filter">修改条件</param>
        /// <param name="entity">新的文档值</param>
        /// <returns></returns>
        public long UpdateMany<TEntity>(Expression<Func<TEntity, bool>> filter, TEntity entity) where TEntity : EntityModel
        {
            UpdateDefinition<TEntity> updateDefinition = entity.MarkUpdateDefinition<TEntity>();
            UpdateDefinition<TEntity> updateDefinitionBuilder = new UpdateDefinitionBuilder<TEntity>().Combine(updateDefinition);
            return this.GetCollection<TEntity>().UpdateMany(filter, updateDefinitionBuilder).ModifiedCount;
        }

        /// <summary>
        /// 修改数据(多条)
        /// </summary>
        /// <typeparam name="TEntity">文档类型</typeparam>
        /// <param name="filter">修改条件</param>
        /// <param name="entity">新的文档值</param>
        /// <returns></returns>
        public async Task<long> UpdateManyAsync<TEntity>(Expression<Func<TEntity, bool>> filter, TEntity entity) where TEntity : EntityModel
        {
            UpdateDefinition<TEntity> updateDefinition = entity.MarkUpdateDefinition<TEntity>();
            UpdateDefinition<TEntity> updateDefinitionBuilder = new UpdateDefinitionBuilder<TEntity>().Combine(updateDefinition);
            var result = await this.GetCollection<TEntity>().UpdateManyAsync(filter, updateDefinitionBuilder);
            return result.ModifiedCount;
        }
    }

    // 删除
    public partial class MongoContext
    {
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="filter">删除条件</param>
        /// <returns></returns>
        public long DeleteOne<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : EntityModel
        {
            return this.GetCollection<TEntity>().DeleteOne(filter).DeletedCount;
        }

        /// <summary>
        /// 删除一条数据(异步)
        /// </summary>
        /// <param name="filter">删除条件</param>
        /// <returns></returns>
        public async Task<long> DeleteOneAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : EntityModel
        {
            var result = await this.GetCollection<TEntity>().DeleteOneAsync(filter);
            return result.DeletedCount;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="entity">要删除的数据</param>
        /// <returns></returns>
        public long DeleteOne<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            return this.DeleteOne(entity);
        }

        /// <summary>
        /// 删除一条数据(异步)
        /// </summary>
        /// <param name="entity">要删除的数据</param>
        /// <returns></returns>
        public async Task<long> DeleteOneAsync<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            return await this.DeleteOneAsync(entity);
        }

        /// <summary>
        /// 删除指定条件的数据
        /// </summary>
        /// <param name="collectionName">数据集名称</param>
        /// <param name="filter">删除条件</param>
        /// <returns></returns>
        public long DeleteRange<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : EntityModel
        {
            return this.GetCollection<TEntity>().DeleteMany(filter).DeletedCount;
        }

        /// <summary>
        /// 删除指定条件的数据
        /// </summary>
        /// <param name="filter">删除条件</param>
        /// <returns></returns>
        public async Task<long> DeleteRangeAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : EntityModel
        {
            var result = await this.GetCollection<TEntity>().DeleteManyAsync(filter);
            return result.DeletedCount;

        }
    }

    // 替换
    public partial class MongoContext
    {
        /// <summary>
        /// 替换数据
        /// </summary>
        /// <param name="entity">要替换的数据</param>
        /// <returns></returns>
        public long Replace<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            return this.GetCollection<TEntity>().ReplaceOne(e => e._id == entity._id, entity, new UpdateOptions() { IsUpsert = true }).ModifiedCount;
        }

        /// <summary>
        /// 替换数据(异步)
        /// </summary>
        /// <param name="entity">要替换的数据</param>
        /// <returns></returns>
        public async Task<long> ReplaceAsync<TEntity>(TEntity entity) where TEntity : EntityModel
        {
            ReplaceOneResult resut = await this.GetCollection<TEntity>().ReplaceOneAsync(e => e._id == entity._id, entity, new UpdateOptions() { IsUpsert = true });
            return resut.ModifiedCount;
        }
    }

    // 查询
    public partial class MongoContext
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Find<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : EntityModel
        {
            return this.GetCollection<TEntity>().Find(filter).ToEnumerable();
        }

        /// <summary>
        /// 查询(异步)
        /// </summary>
        /// <param name="collectionName">数据集名称</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : EntityModel
        {
            var result = await this.GetCollection<TEntity>().FindAsync(filter).ConfigureAwait(false);
            return result.ToEnumerable();
        }

        /// <summary>
        /// 查询(lambda)
        /// </summary>
        /// <typeparam name="TEntity">数据集类型:必须继承于<see cref="MongoEntity"/></typeparam>
        /// <returns></returns>
        public IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : EntityModel
        {
            return this.GetCollection<TEntity>().AsQueryable().AsQueryable();
        }
    }

    // 其它
    public partial class MongoContext
    {
        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <typeparam name="TEntity">数据集类型</typeparam>
        /// <returns></returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : EntityModel
        {
            return this.MongoDatabase.GetCollection<TEntity>(EntityModel.GetCollectionName<TEntity>());
        }

        /// <summary>
        /// MapReduce(原始)
        /// </summary>
        /// <typeparam name="TEntity">数据集类型</typeparam>
        /// <typeparam name="TResult">返回结果</typeparam>
        /// <param name="map">Map语句</param>
        /// <param name="reduce">Reduce语句</param>
        /// <param name="options">选项配置</param>
        /// <returns></returns>
        public IEnumerable<ReduceResult<TResult>> MapReduce<TEntity, TResult>(string map, string reduce, MapReduceOptions<TEntity, ReduceResult<TResult>> options = null) where TEntity : EntityModel
        {
            var result = this.GetCollection<TEntity>().MapReduce(map, reduce, options);
            result.MoveNext();
            return result.Current;
        }

        /// <summary>
        /// MapReduce(原始)
        /// </summary>
        /// <typeparam name="TEntity">数据集类型</typeparam>
        /// <typeparam name="TResult">返回结果</typeparam>
        /// <param name="map">Map语句</param>
        /// <param name="reduce">Reduce语句</param>
        /// <param name="options">选项配置</param>
        /// <returns></returns>
        public async Task<IEnumerable<ReduceResult<TResult>>> MapReduceAsync<TEntity, TResult>(string map, string reduce, MapReduceOptions<TEntity, ReduceResult<TResult>> options = null) where TEntity : EntityModel
        {
            var result = await this.GetCollection<TEntity>().MapReduceAsync(map, reduce, options);
            await result.MoveNextAsync();
            return result.Current;
        }
    }
}
