using ABPUnit;
using CommonUnit.StringUnit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace ABPEFCoreMySqlUnit
{
    public class BaseRepository<TDbContext, TEntity, TPrimaryKey> : EfCoreRepository<TDbContext, TEntity, TPrimaryKey>, IBaseRepository<TEntity, TPrimaryKey>
        where TEntity : BaseAggregateRoot<TPrimaryKey>, IEntity<TPrimaryKey>
        where TDbContext : IEfCoreDbContext
    {
        private readonly IMediator _mediator;

        public BaseRepository(IDbContextProvider<TDbContext> dbContextProvider, IMediator mediator) : base(dbContextProvider)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="bAdd"></param>
        /// <returns></returns>
        public async Task<int> Save(TEntity entity, bool bAdd = true)
        {
            var entityEntry = bAdd ? DbContext.Add(entity) : DbContext.Update<TEntity>(entity);
            var events = entity.GetNotificationEventsHandler();
            if (events.Count > 0)
            {
                foreach (var item in events)
                {
                    _mediator.Publish(item);
                }
            }
            return await DbContext.SaveChangesAsync();
        }
    }
}
