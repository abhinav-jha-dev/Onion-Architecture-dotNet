using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectOnion.Model.Interfaces;

namespace ProjectOnion.Http.WebApi.V1.ViewModel.Entity
{
    public class EntityConverter<TE> where TE : IEntity
    {
        protected EntityConverter()
        {
        }

        protected EntityConverter(TE entity)
        {
            if (entity != null)
            {
                foreach (var propertyInfo in entity.GetType().GetProperties().ToArray())
                {
                    if (GetType().GetProperty(propertyInfo.Name) != null)
                    {
                        GetType().GetProperty(propertyInfo.Name).SetValue(this, propertyInfo.GetValue(entity, null));
                    }
                }
            }
        }

        public TE Hydrate(TE entity)
        {
            if (entity != null)
            {
                foreach (var propertyInfo in GetType().GetProperties().ToArray())
                {
                    if (entity.GetType().GetProperty(propertyInfo.Name) != null)
                    {
                        entity.GetType().GetProperty(propertyInfo.Name).SetValue(entity, propertyInfo.GetValue(this, null));
                    }
                }
            }

            return entity;
        }
    }
}
