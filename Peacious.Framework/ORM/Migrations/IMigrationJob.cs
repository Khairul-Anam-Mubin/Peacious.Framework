namespace Peacious.Framework.ORM.Migrations;

public interface IMigrationJob
{
    Task MigrateAsync();
}