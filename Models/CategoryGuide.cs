using System;
using System.Collections.Generic;

namespace server.Models;

public partial class CategoryGuide
{
    public byte IdCategory { get; set; }

    public string NameCategory { get; set; } = null!;

    public virtual ICollection<Guide> Guides { get; set; } = new List<Guide>();
}
