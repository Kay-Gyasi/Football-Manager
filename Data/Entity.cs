namespace Data;

public abstract class Entity<TKey>
{
    public TKey Id { get; private set; }
    public Audit Audit { get; private set; }

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
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string CreatedBy { get; set; }
    public string UpdatedBy { get; set; }
    public EntityStatus Status { get; set; }

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