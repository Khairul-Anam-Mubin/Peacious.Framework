using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peacious.Framework.ORM.Migrations;

public class MigrationJob
{
    public required string Name { get; set; }
    public int Order { get; set; }
    public bool Enabled { get; set; }
}
