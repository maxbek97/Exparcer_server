using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Guide
{
    public string IdDocument { get; set; } = null!;

    public string NameGuid { get; set; } = null!;

    public DateOnly AcceptanceDate { get; set; }

    public DateOnly CheckDate { get; set; }

    public byte IdStructUnit { get; set; }

    public virtual StructUnit IdStructUnitNavigation { get; set; } = null!;
}
