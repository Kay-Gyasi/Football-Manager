namespace Data.Base;

public abstract class Entity<TKey>
{
    public TKey Id { get; private set; }
    public Audit? Audit { get; set; }

    public Entity<TKey> HasId(TKey id)
    {
        Id = id;
        return this;
    }

    public Entity<TKey> AuditAs(Audit audit)
    {
        Audit = audit;
        return this;
    }
}

public class Audit
{
    private Audit()
    {
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        CreatedBy = "admin";
        UpdatedBy = "admin";
        Status = EntityStatus.Normal;
    }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public string CreatedBy { get; private set; }
    public string UpdatedBy { get; private set; }
    public EntityStatus Status { get; private set; }

    public static Audit Create() => new();

    public Audit WasCreatedBy(string name)
    {
        CreatedBy = name;
        return this;
    }
    
    public Audit WasUpdatedBy(string name)
    {
        UpdatedBy = name;
        return this;
    }

    public Audit Delete()
    {
        Status = EntityStatus.Deleted;
        return this;
    }
    
    public Audit Archive()
    {
        Status = EntityStatus.Archived;
        return this;
    }
}

public enum EntityStatus
{
    Normal = 1,
    Archived,
    Deleted
}