using MIRS.Core.Intefaces;

namespace MIRS.Core.BaseModels;

public abstract class BaseEntity:IBaseEntity , IAuditedEntity 
{
    public int Id { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }
}