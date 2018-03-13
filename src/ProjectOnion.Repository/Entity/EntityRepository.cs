using ProjectOnion.Model.Interfaces;
using ProjectOnion.MongoDB.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectOnion.Repository.Entity
{
    public abstract class EntityRepository<TE,MDE> : IEntityRepository<TE>
        where TE : IEntity
        where MDE : IEntityProvider<TE>
    {
        protected MDE _provider;

        protected EntityRepository(MDE provider)
        {
            this._provider = provider;
        }

        public async Task<long> Count(TE instance)
        {
           return await this._provider.Count();
        }

        public async Task<TE> Create(TE instance)
        {
            return await this._provider.Insert(instance);
        }

        public void Delete(TE instance)
        {
           this._provider.Remove(instance.Id);
        }

        public async Task<TE> Get(Guid id)
        {
            return await this._provider.Get(id);
        }

        public async Task<IEnumerable<TE>> GetAll()
        {
            return await this._provider.GetAll();
        }

        public async Task<TE> Update(TE instance)
        {
            return await this._provider.Update(instance);
        }
    }
}
