namespace Peacious.Framework.ORM.Interfaces;

public interface IMigrationJob
{
    Task MigrateAsync();
}