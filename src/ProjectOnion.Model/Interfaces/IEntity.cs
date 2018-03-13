using System;

namespace ProjectOnion.Model.Interfaces
{
    public interface IEntity : IModel
    {
        Guid Id { get; set; }
    }
}
