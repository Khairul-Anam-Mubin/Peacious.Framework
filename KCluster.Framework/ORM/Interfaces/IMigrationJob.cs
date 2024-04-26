namespace KCluster.Framework.ORM.Interfaces;

public interface IMigrationJob
{
    Task MigrateAsync();
}