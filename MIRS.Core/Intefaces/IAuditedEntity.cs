namespace MIRS.Core.Intefaces;

public interface IAuditedEntity
{
    DateTime? CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}