﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TicTacToe.Application.Common.Interfaces;
using TicTacToe.Domain.Common;
using TicTacToe.Infrastructure.Config;

namespace TicTacToe.Infrastructure.Persistence
{
    public class Repository<T> : IRepository<T> where T : Document
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(IMongoClient client, MongoSettings settings)
        {
            var collectionName = typeof(T).Name;
            collectionName = collectionName[0].ToString().ToLowerInvariant() + collectionName[1..];
            _collection = client.GetDatabase(settings.DatabaseName).GetCollection<T>(collectionName);
        }
        
        public async Task<T> CreateAsync(T document)
        {
            document.CreatedAt = DateTime.Now;
            document.UpdatedAt = DateTime.Now;
            await _collection.InsertOneAsync(document);
            return document;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _collection.AsQueryable().FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<T> ReplaceAsync(T document)
        {
            await _collection.ReplaceOneAsync(x => x.Id.Equals(document.Id), document);
            return document;
        }

        public async Task<(List<T> items, long count)> GetPaginatedAsync(int pageNumber, int pageSize)
        {
            var countFacet = AggregateFacet.Create("count",
                PipelineDefinition<T, AggregateCountResult>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Count<T>()
                }));
            var dataFacet = AggregateFacet.Create("data",
                PipelineDefinition<T, T>.Create(new[]
                {
                    PipelineStageDefinitionBuilder.Sort(Builders<T>.Sort.Ascending(x => x.CreatedAt)),
                    PipelineStageDefinitionBuilder.Skip<T>((pageNumber - 1) * pageSize),
                    PipelineStageDefinitionBuilder.Limit<T>(pageSize)
                }));

            var filter = Builders<T>.Filter.Empty;
            var aggregation = await _collection.Aggregate()
                .Match(filter)
                .Facet(countFacet, dataFacet)
                .ToListAsync();

            var count = aggregation.First()
                .Facets.First(x => x.Name == "count")
                .Output<AggregateCountResult>()
                ?.FirstOrDefault()
                ?.Count ?? 0;

            var data = aggregation.First()
                .Facets.First(x => x.Name == "data")
                .Output<T>();

            return (data.ToList(), count);
        }
    }
}