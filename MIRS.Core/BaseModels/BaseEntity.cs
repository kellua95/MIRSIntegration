using MIRS.Core.Intefaces;

namespace MIRS.Core.BaseModels;

public abstract class BaseEntity:IBaseEntity
{
    public int Id { get; set; }
}