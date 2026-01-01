namespace MIRS.Core.BaseModels;

public abstract class BaseEntity
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }
}

public abstract class  BaseEntity<TKey>
{

    public int Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

}