namespace server.Models
{
    /// <summary>
    /// Класс, описывающий сущность Инструкции. Используется для создания Инструкции.
    /// </summary>
    public class CreateRecordDTO
    {
        public string name_guid { get; set; }
        public string struct_name { get; set; }
        public string id_guid { get; set; }
        public DateOnly acceptance_day { get; set; }
        public DateOnly check_day { get; set; }
    }
    /// <summary>
    /// Класс, описывающий сущность Инструкции. Используется для показа Инструкций с добавленным полем, для подсчета оставшегося кол-ва дней
    /// </summary>
    public class GetRecordDTO
    {
        public string name_guid { get; set; }
        public string struct_name { get; set; }
        public string id_guid { get; set; }
        public DateOnly acceptance_day { get; set; }
        public DateOnly check_day { get; set; }
        public int days_remaining { get; set; }
    }
}
