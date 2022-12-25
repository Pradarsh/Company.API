using Company.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Company.Data.Interface
{
    public interface IDbService
    {
        Task<List<TDto>> GetAsync<TEntity, TDto>() where TEntity : class, IEntity
                where TDto : class;

        Task<TDto> SingleAsync<TEntity, TDto>(Expression<Func<TEntity, bool>>
                    expression) where TEntity : class, IEntity where TDto : class;

        Task<bool> SaveChangesAsync();

        Task<TEntity> AddAsync<TEntity, TDto>(TDto dto) where TEntity : class where
                TDto : class;

        void Update<TEntity, TDto>(int id, TDto dto) where TEntity : class, IEntity
                    where TDto : class;

        Task<bool> DeleteAsync<TEntity>(int id) where TEntity : class, IEntity;

    }
}
