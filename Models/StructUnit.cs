using System;
using System.Collections.Generic;

namespace server.Models;
/// <summary>
/// Модель описывающая сущность Структурной единицы из БД. Создан Entity Framework
/// </summary>
public partial class StructUnit
{
    public byte IdUnit { get; set; }

    public string NameStruct { get; set; } = null!;

    public virtual ICollection<Guide> Guides { get; set; } = new List<Guide>();
}
