using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacious.Framework.ORM.Migrations;

public class MigrationConfig
{
    public bool Enabled { get; set; }
    public List<MigrationJob> MigrationJobs { get; set; }

    public MigrationConfig()
    {
        MigrationJobs = new List<MigrationJob>();
    }
}
