namespace server.Models
{
    /// <summary>
    /// Аналогичная сущность, просто для отображения инфы о Структурных единицах.
    /// </summary>
    public class StructureUnitsDTO
    {
        //Использую новый класс, потому что не рекомендуется использовать класс-модели для работ, не связанных с БД.
        public byte IdUnit { get; set; }

        public string NameStruct { get; set; } = null!;
    }
}
