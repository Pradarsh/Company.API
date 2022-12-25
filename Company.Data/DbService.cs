using AutoMapper;
using Company.Data.Context;
using Company.Data.Interface;
using Company.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data
{
    public class DbService : IDbService
    {
        private readonly CompnayContext _db;
        private readonly IMapper _mapper;
        public DbService(CompnayContext db, IMapper mapper) =>
        (_db, _mapper) = (db, mapper);

        public async Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class,
                                IEntity where TDto : class
        {
            var entities = await _db.Set<TEntity>().ToListAsync();
            return _mapper.Map<List<TDto>>(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() >= 0;
        }

        public async Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>> expression) where TEntity : class,
                                IEntity where TDto : class
        {
            var entity = await SingleAsync(expression);
            return _mapper.Map<TDto>(entity);
        }

        public async Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where
                                TDto : class
        {
            var entity = _mapper.Map<TEntity>(dto); 
            await _db.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public void Update<TEntity, TDto>(int id, TDto dto) where TEntity : class, IEntity
                where TDto : class
        {
           var entity =  _mapper.Map<TEntity>(dto);
            _db.Set<TEntity>().Update(entity);
        }

        public Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity
        {
            var entity = SingleAsync<TEntity>(e => e.Id.Equals(id)).Result;
            if (entity == null)
                return Task.FromResult(false);
            else
            {
                _db.Remove(entity);
                return Task.FromResult(true);
            }
        }

        private async Task<TEntity?> SingleAsync<TEntity>(Expression<Func<TEntity,
                    bool>> expression) where TEntity : class, IEntity =>
            await _db.Set<TEntity>().SingleOrDefaultAsync(expression);

      
    }
}
