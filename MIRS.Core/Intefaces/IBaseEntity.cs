namespace MIRS.Core.Intefaces;

public interface IBaseEntity
{
    public int Id { get; set; }
}

public interface IBaseEntity<TKey>
{
    public TKey Id { get; set; }
}