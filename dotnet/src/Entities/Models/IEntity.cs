using System;

namespace Entities.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}