using MIRS.Core.Intefaces;

namespace MIRS.Core.BaseModels;

public abstract class BaseEntity<TKey> : IBaseEntity<TKey>, IAuditedEntity
{
    public TKey Id { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
