using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectOnion.Model.Interfaces;

namespace ProjectOnion.Http.WebApi.V1.Controller.Entity
{
    public abstract class EntityController<TEntity, TService> : Microsoft.AspNetCore.Mvc.Controller
        where TService : IEntityService<TEntity>
        where TEntity : IEntity
    {
        protected readonly TService _service;
        protected IHttpContextAccessor _accessor;

        protected EntityController(TService service)
        {
            _service = service;
        }

        protected EntityController(TService service, IHttpContextAccessor accessor)
            : this(service)
        {
            _accessor = accessor;
        }

        protected void SetClientIp(TEntity entityObject)
        {
            entityObject.IPAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }
    }
}
