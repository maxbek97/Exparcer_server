using System;
using System.Collections.Generic;

namespace server.Models;

public partial class Guide
{
    public uint IdRec { get; set; }

    public string NameGuid { get; set; } = null!;

    public DateOnly AcceptanceDate { get; set; }

    public string IdDocumnet { get; set; } = null!;

    public DateOnly CheckDate { get; set; }

    public byte? IdCategory { get; set; }

    public virtual CategoryGuide? IdCategoryNavigation { get; set; }
}
