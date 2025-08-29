using System;
using System.Collections.Generic;

namespace server.Models;

public partial class StructUnit
{
    public byte IdUnit { get; set; }

    public string NameStruct { get; set; } = null!;

    public virtual ICollection<Guide> Guides { get; set; } = new List<Guide>();
}
